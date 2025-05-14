using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    [SerializeField] private float speed;
    public Animator anim;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] public AudioClip deathSound;
    [SerializeField] private AudioClip cookieSound;
    [SerializeField] private AudioClip winSound;
    public CookieManager cManag;
    public GameManager gManag;
    public bool isDead;

    private void Awake()
    {
        // Gets References from PlayerObject
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        //first setup
        anim.SetBool("grounded",true);
        anim.SetBool("jump",false);
        anim.SetBool("run",false);
        isDead=false;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            horizontalKeyboard();
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput > 0) {
            LeftRight(horizontalInput);
        }

        if(horizontalInput == 0) {
            anim.SetBool("grounded",true);
            anim.SetBool("jump",false);
            anim.SetBool("run",false);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isGrounded())
            Jump();

        if(!isGrounded()) {
            anim.SetBool("grounded",false);
            anim.SetBool("run",false);
            anim.SetBool("jump",true);
        }
        
    }

// Keyboard input
    private void horizontalKeyboard() {
        if (!isDead) {
            //set animator params
            anim.SetBool("run", true);
            anim.SetBool("grounded", isGrounded());

            float horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontalInput*speed,body.velocity.y);

            if(horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1,1,1);
        }
    }

// Touchscreen input
    public void LeftRight(float horizontalVelocity) {
        Debug.Log("Touch"+horizontalVelocity);
        if(Input.touchCount > 1) {
            Debug.Log(Input.GetTouch(0).phase.ToString());
        }
        if (!isDead) {
        //set animator params
        anim.SetBool("run", true);

        body.velocity = new Vector2(horizontalVelocity*speed,body.velocity.y);

        if(horizontalVelocity > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalVelocity < -0.01f)
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    // new touchscreen input
    public void OnWalk(InputValue value) 
{
    Vector2 input = value.Get<Vector2>();
    rigidbody.velocity = new Vector2(input.x * speed, rigidbody.velocity.y);
}
    public void OnJump(InputValue value) 
    {
        Jump();
    }

    public void Jump()
    {
        if (!isDead) {
        anim.SetBool("grounded",false);
        anim.SetBool("jump",true);
        body.velocity = new Vector2(body.velocity.x, speed*1.5f);
        SoundManager.instance.playSound(jumpSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectible")) {
            Destroy(collision.gameObject);
            SoundManager.instance.playSound(cookieSound);
            gManag.pointIncrease();
            Debug.Log("cookies: "+gManag.cookieCount);
        }
        
        if(collision.gameObject.CompareTag("Death") && !isDead) {
            death();
        }
    }

    public void death() {
        Time.timeScale=0; //pause game
        isDead=true;
        SoundManager.instance.playSound(deathSound);
        gManag.gameOver();
        Debug.Log("Dead");
        anim.SetBool("idle",false);
        
        anim.SetTrigger("hurt");
        anim.SetBool("hurt",true);
        new WaitForSeconds(2f);
        anim.SetBool("hurt",false);
    }

// if player touches AI
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) {
            Debug.Log("Death by AI");
            death();
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, Vector2.down,0.2f,groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //make sure wall is not ground
        if(collision.gameObject.CompareTag("Ground")) {
            anim.SetBool("grounded",true);
            anim.SetBool("jump",false);
            anim.SetBool("run",false);
        } else if(collision.gameObject.CompareTag("Win")) {
            SoundManager.instance.playSound(winSound);
            anim.SetBool("active",true);
            new WaitForSeconds(2f);
        } else if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") ) {
            SoundManager.instance.playSound(deathSound);
            anim.SetBool("active",false);
            anim.SetBool("hurt",true);
            death();
            new WaitForSeconds(2f);
            anim.SetBool("hurt",false);
        }
    }
}
