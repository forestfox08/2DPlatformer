using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed = 7f;
    public float jumpForce = 15f;
    [SerializeField] private int health = 3;

    [Header("References")]
    private Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    private SpriteRenderer Sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        SetAnimation(moveInput);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("run");
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("jump");
            }
            else
            {
                animator.Play("fall");
            }
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            health--;
            Debug.Log("Player Health: " + health);
            StartCoroutine(FlashRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }
    private IEnumerator FlashRed()
    {
        Sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Sr.color = Color.white;
    }
    private void Die()
    {
        Debug.Log("Player Died");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}