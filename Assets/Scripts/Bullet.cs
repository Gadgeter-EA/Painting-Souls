using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    public Animator animator;
    
    void Update()
    {
        rb.velocity = transform.right * speed; // Moving the bullet to the right
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.gameObject.tag == "Enemy")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            FindObjectOfType<AudioManager>().Play("paintImpact");
            animator.SetTrigger("HasImpact");
            StartCoroutine(CheckAnimationCompleted("Impact", () =>
            {
                Destroy(gameObject); // Destroy the bullet
            }));
            
        }
        else if (hitInfo.gameObject.tag == "Player")
        {
            
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("paintImpact");
            animator.SetTrigger("HasImpact");
            StartCoroutine(CheckAnimationCompleted("Impact", () =>
            {
                Destroy(gameObject); // Destroy the bullet
            }));
        }
    }

    public IEnumerator CheckAnimationCompleted(string currentAnim, Action Oncomplete)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnim))
            yield return null;
        if (Oncomplete != null)
            Oncomplete();
    }
    
}
