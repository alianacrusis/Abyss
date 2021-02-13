using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool protagonistIsActive;
    public bool shadowIsActive;

    public Transform targetA;
    public Transform targetB;


    ProtagonistMover _protagonistController;
    ShadowMover _shadowController;
    Rigidbody2D _protagonistRgbd;
    Rigidbody2D _shadowRgbd;
    PolygonCollider2D _protagonistCollider;
    PolygonCollider2D _shadowCollider;
    GameObject _protagonistCam;
    GameObject _shadowCam;

    GameManager gm;
    UIManager uiManager;



    // Start is called before the first frame update
    void Start()
    {
        protagonistIsActive = true;
        shadowIsActive = false;

        _protagonistController = GameObject.Find("Protagonist").GetComponent<ProtagonistMover>();
        _protagonistRgbd = GameObject.Find("Protagonist").GetComponent<Rigidbody2D>();
        _protagonistCollider = GameObject.Find("Protagonist").GetComponent<PolygonCollider2D>();

        _shadowController = GameObject.Find("Shadow").GetComponent<ShadowMover>();
        _shadowRgbd = GameObject.Find("Shadow").GetComponent<Rigidbody2D>();
        _shadowCollider = GameObject.Find("Shadow").GetComponent<PolygonCollider2D>();

        _protagonistCam = GameObject.Find("CM_P vcam");
        _shadowCam = GameObject.Find("CM_S vcam");

        gm = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && protagonistIsActive && _protagonistCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Shadow", "Pushable Block")))
        {
            SetShadowActive();
            EnableShadowActiveConstraints();
        }

        else if (Input.GetKeyDown(KeyCode.O) && shadowIsActive && _shadowCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Protagonist", "Pushable Block")))
        {
            SetProtagonistActive();
            EnableProtagonistActiveConstraints();
        }

        CheckForTogetherness();
    }

    private void SetProtagonistActive()
    {
        _protagonistCam.SetActive(true);
        shadowIsActive = false;
        _shadowController.enabled = false;
        _protagonistController.enabled = true;
        protagonistIsActive = true;
    }

    private void SetShadowActive()
    {
        _protagonistCam.SetActive(false);
        protagonistIsActive = false;
        _protagonistController.enabled = false;
        _shadowController.enabled = true;
        shadowIsActive = true;
    }

    private void EnableProtagonistActiveConstraints()
    {
        _shadowRgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        _protagonistRgbd.constraints = RigidbodyConstraints2D.None;
        _protagonistRgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void EnableShadowActiveConstraints()
    {
        _protagonistRgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        _shadowRgbd.constraints = RigidbodyConstraints2D.None;
        _shadowRgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void CheckForTogetherness()
    {
        if (_protagonistCollider.IsTouchingLayers(LayerMask.GetMask("Checkpoint")) && _shadowCollider.IsTouchingLayers(LayerMask.GetMask("Checkpoint")))
        {
            gm.StartCoroutine(gm.LoadNextScene());
        }

        else if (_protagonistCollider.IsTouchingLayers(LayerMask.GetMask("Checkpoint")))
        {
            StartCoroutine(uiManager.ManageForgettingSomeoneText());
        }

        else if(_shadowCollider.IsTouchingLayers(LayerMask.GetMask("Checkpoint")))
        {
            StartCoroutine(uiManager.ManageForgettingSomeoneText());
        }
    }

}
