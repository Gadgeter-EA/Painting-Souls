using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    // public GameObject impactEffect; // Impact effect
    
    void Update()
    {
        rb.velocity = transform.right * speed; // Moving the bullet to the right
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>(); // Trying to get an enemy
        if (enemy != null) // If it's an enemy, we deal damage.
        {
            enemy.TakeDamage(damage); 
        }
        // Instantiate(impactEffect, transform.position,transform.rotation); Reproducing impact effect
        Destroy(gameObject); // Destroy the bullet
    }
}
