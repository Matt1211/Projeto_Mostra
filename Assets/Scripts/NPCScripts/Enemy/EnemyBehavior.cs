using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 2.7f; // Movement speed
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;
    public float rightLimit;
    public float leftLimit;
    public Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy
        float move = (facingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Check for boundaries and switch direction if needed
        if ((facingRight && transform.position.x >= rightLimit) || (!facingRight && transform.position.x <= leftLimit))
        {
            SwitchDirection();
        }
    }

    private void SwitchDirection()
    {
        facingRight = !facingRight;

        // Flip the sprite renderer
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void KillEnemy()
    {
        animator.SetBool("enemy_jumped", true);
        StartCoroutine(DelayDeath(0.3f));
    }


    private IEnumerator DelayDeath(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }
}
