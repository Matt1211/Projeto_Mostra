using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.25f; // Movement speed
    public float jumpSpeed = 10f;   // Jump force
    private bool facingRight = true;
    private Rigidbody2D rb;         // Reference to the Rigidbody2D
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal input (left/right arrow keys or A/D)
        float inputX = Input.GetAxis("Horizontal");

        // Move the player horizontally
        Vector2 playerVelocity = rb.velocity;
        playerVelocity.x = inputX * moveSpeed;
        rb.velocity = playerVelocity;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (playerVelocity.x < 0 && facingRight)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        else if (playerVelocity.x > 0 && !facingRight)
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }

        animator.SetFloat("player_speed", Mathf.Abs(playerVelocity.x));

        // Check for jump input (spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply upward force for jumping
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
}
