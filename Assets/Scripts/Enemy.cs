using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 20;
    public Player player;
    public string deathSound;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //public GameObject deathEffect;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_GrayscaleAmount", 1);
    }
    
    void Update()
    {
        animator.SetFloat("Speed", Math.Abs(rb.velocity.x));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }else
        {
            spriteRenderer.material.SetFloat("_GrayscaleAmount", health/100.0f);
        }
        
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play(deathSound);
        //Instantiate(deathEffect, transform.position, Quaternion.identity); // Reproduce death animation
        Destroy(gameObject); // Destroy the Enemy Object
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
            StartCoroutine(player.KnockBack(1f, 30f, transform));
            Debug.Log("Kock");
        }
    }
}
