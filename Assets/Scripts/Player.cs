using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    private Rigidbody2D rb;
    public HealthBar healthBar;

    [Header("iFrames")] 
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberFlashes;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
        
;        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator KnockBack(float KnockTime, float KnockPower, Transform enemy)
    {
        float timer = 0;

        while (KnockTime > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            rb.AddForce(-direction * KnockPower);
        }

        yield return 0;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8,9,true); // Telling to ignore
        //Wait certain time before returning Collisions
        for (int i = 0; i < numberFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f); // Changing the Sprite to red
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2)); // Waiting 1 second
            spriteRenderer.color = Color.white; // Returning to white
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2)); // Waiting 1 second
        }
        Physics2D.IgnoreLayerCollision(8,9,false); // Enabling collisions again
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
