using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowMover : MonoBehaviour
{
    //movement variables

    [Range(0.0f, 10.0f)] public float moveSpeed;
    public float jumpForce;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 3f;
    public GameObject respawnPoint;
    public bool isImmune;
    public float immunityTime = 10f;
    public bool canFloat;


    Rigidbody2D _rb2d;
    Transform _transform;
    PolygonCollider2D _playerCollider;
    Animator _animator;
    UIManager uiManager;

    float _horizontal;
    bool _isFacingRight;


    //skill variables
    SpriteRenderer _myRenderer;

    bool ignore;
    bool canPassThru;

    // Start is called before the first frame update
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _playerCollider = GetComponent<PolygonCollider2D>();

        _animator = GetComponent<Animator>();
        _myRenderer = GetComponent<SpriteRenderer>();
        ignore = false;
        canFloat = false;

        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        CheckIfFloat();
        /*if (Input.GetKeyDown(KeyCode.I)) // && the current scene is the upper abyss)
        {
            SetImmune();
            StartCoroutine(Immune());
        }*/
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

        if (Input.GetButtonDown("Jump") && _playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Protagonist", "Pushable Block", "Moving Platform", "Jelly Bell", "Bubble")))
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

    private void OnCollisionEnter2D(Collision2D otherCollider)
    { 
        if (otherCollider.gameObject.tag == "Jelly")
        {
            _animator.SetTrigger("Damage");
            StartCoroutine(Respawn());
        }

        if (otherCollider.gameObject.tag == "Moving Platform")
        {
            this.transform.parent = otherCollider.transform;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Moving Platform")
        {
            this.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Dandelion L")
        {
            if (isImmune == true)
            {
                Physics2D.IgnoreLayerCollision(10, 12, ignore = true);
            }

            else if (isImmune == false)
            {
                _animator.SetTrigger("Damage");
                Destroy(otherCollider.gameObject);
                StartCoroutine(Respawn());
            }
        }

        if (otherCollider.gameObject.tag == "Dandelion R")
        {
            if (isImmune == true)
            {
                Physics2D.IgnoreLayerCollision(10, 12, ignore = true);
            }

            else if (isImmune == false)
            {
                _animator.SetTrigger("Damage");
                Destroy(otherCollider.gameObject);
                StartCoroutine(Respawn());
            }
        }

        if (otherCollider.gameObject.tag == "Torch")
        {
                otherCollider.GetComponentInChildren<Light2D>().enabled = true;
        }

        if (otherCollider.gameObject.tag == "Float Sequence Trigger")
        {
            StartCoroutine(uiManager.ManageFloatDangerSequence());
            Destroy(otherCollider.gameObject);
        }

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        this.transform.position = respawnPoint.transform.position;
        isImmune = false;
    }

    IEnumerator Immune()
    {
        yield return new WaitForSeconds(immunityTime);
        SetDamageable();
    }

    private void CheckIfFloat()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canFloat == true)
            {
                _rb2d.gravityScale = .1f;
            }
        }
    }

    public void SetImmune()
    {
        immunityTime = 10f;
        isImmune = true;
        //UITimer.enabled = true;
        Debug.Log("Shadow is immune to attack!");
        //mainCam.GetComponent<AudioSource>().pitch = 0.75f;
        Color temp = _myRenderer.color;
        temp.a = 0.35f;
        _myRenderer.color = temp;
    }

    public void SetDamageable()
    {
        isImmune = false;
        //UITimer.enabled = false;
        Physics2D.IgnoreLayerCollision(8, 12, ignore = false);
        Debug.Log("Shadow is no longer immune!");
        //mainCam.GetComponent<AudioSource>().pitch = 1f;
        Color temp = _myRenderer.color;
        temp.a = 1f;
        _myRenderer.color = temp;

    }


}
