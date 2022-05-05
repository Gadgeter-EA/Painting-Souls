using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Rigidbody2D rb;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
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

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
