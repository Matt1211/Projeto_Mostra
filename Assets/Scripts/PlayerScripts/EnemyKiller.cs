using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip enemyDeathSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyNPC"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = enemyDeathSfx;
            audioSource.Play();
            enemy.KillEnemy();
        }
    }
}
