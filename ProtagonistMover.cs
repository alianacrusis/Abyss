using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtagonistMover : MonoBehaviour
{
    //public variables
    [Range(0.0f, 10.0f)] public float moveSpeed;
    public float jumpForce;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 3f;
    public GameObject respawnPoint;


    //private variables
    Rigidbody2D _rb2d;
    Transform _transform;
    PolygonCollider2D _playerCollider;
    SpriteRenderer _myRenderer;
    Animator _animator;
    CharacterManager charManager;
    UIManager uiManager;
 
    float _horizontal;
    bool _isFacingRight;
    bool _ignore;

    // Start is called before the first frame update
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _playerCollider = GetComponent<PolygonCollider2D>();
        _myRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        charManager = FindObjectOfType<CharacterManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }

    private void LateUpdate()
    {
        FlipSprite();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(horizontal * moveSpeed, _rb2d.velocity.y);
        _rb2d.velocity = moveVelocity;
    }

    private void Jump()
    {

        if (Input.GetButtonDown("Jump") && _playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Shadow", "Pushable Block", "Moving Platform", "Jelly Bell", "Bubble")))
        {
            _rb2d.velocity = Vector2.up * jumpForce;
        }

        if (_rb2d.velocity.y < 0)
        {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (_rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void FlipSprite()
    {
        bool playerIsMovingHorizontally = Mathf.Abs(_rb2d.velocity.x) > Mathf.Epsilon;
        if (playerIsMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb2d.velocity.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)  
    {
        if (otherCollider.gameObject.tag == "Dandelion L")
        {
            _animator.SetTrigger("Damage");
            Destroy(otherCollider.gameObject);
            StartCoroutine(Respawn());
        }

        if (otherCollider.gameObject.tag == "Dandelion R")
        {
            _animator.SetTrigger("Damage");
            Destroy(otherCollider.gameObject);
            StartCoroutine(Respawn());
        }

        if (otherCollider.gameObject.tag == "Jelly")
        {
            _animator.SetTrigger("Damage");
        }

        if (otherCollider.gameObject.tag == "Toggle Text Volume")
        {
            StartCoroutine(uiManager.ManageToggleText());
            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.gameObject.tag == "Discovery Trigger")
        {
            StartCoroutine(uiManager.ManageDiscoveryText());
            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.gameObject.tag == "Shadow Meet Trigger")
        {
           StartCoroutine(uiManager.ManageShadowMeetText());
           Destroy(otherCollider.gameObject);
        }


        if (otherCollider.gameObject.tag == "Aquarium Reminder Trigger")
        {
            StartCoroutine(uiManager.ManageAquariumText());
            Destroy(otherCollider.gameObject);
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        this.transform.position = respawnPoint.transform.position;
    }

}

    


