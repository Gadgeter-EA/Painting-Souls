using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 20;
    //public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Die();
    }

    void Die()
    {
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
        }
    }
}
