using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public float offs;
    public GameObject Guns;
    [SerializeField] private float speed;
    [SerializeField] private int maxJumps = 2; // Maximum number of jumps (including initial jump)
    [SerializeField] private int jumpsLeft; // Number of jumps remaining
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private float jforce;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    
    private bool isGrounded;

    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        float moveH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveH * speed, rb.velocity.y); 

        if (isGrounded)
        {
            jumpsLeft = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsLeft > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity before jump
            rb.AddForce(Vector2.up * jforce, ForceMode2D.Impulse);
            jumpsLeft--;
            isGrounded = false; // Character is no longer grounded after jumping
        }

        WallSlide(moveH); 
        Flip(moveH);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps; // Reset jumps when grounded
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("JumpWall"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps; // Reset jumps when grounded
        }
        if (collision.gameObject.CompareTag("a"))
        {
            maxJumps = 1;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("a"))
        {
            maxJumps = 2;
        }
    }
    
    private void Flip(float moveH)
    {

        if (isFacingRight && moveH < 0f || !isFacingRight && moveH > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            GlobalManager.ofs = GlobalManager.ofs + offs;
        }
    }
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide(float moveH)
    {
        if (isWalled() && isGrounded && moveH != 0)
        {
            speed = 0.1f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else if (!isWalled())
        {
            speed = 5f;
        }
    }
}