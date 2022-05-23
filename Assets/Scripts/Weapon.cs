 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    public Transform firePoint; // FirePoint that is an emptyObject
    public GameObject bulletPrefab; // Prefab of the bullet
    
    Animator animator;
    CharacterController2D controller;
    private float cooldownTimer = Mathf.Infinity;
    
    public int maxPaint = 200;
    public int currentPaint;
    public PaintBar paintBar;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        currentPaint = maxPaint;
        paintBar.SetMaxPaint(maxPaint);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && cooldownTimer > attackCooldown && !controller.IsCrouching())
        {
            if (currentPaint > 0)
            {
                Shoot(10);
            } else FindObjectOfType<AudioManager>().Play("noPaint");
            
        }

        cooldownTimer += Time.deltaTime;
    }

    void Shoot(int paint)
    {
        animator.SetTrigger("IsAttacking");
        FindObjectOfType<AudioManager>().Play("AttackSound");
        currentPaint -= paint;
        paintBar.SetPaint(currentPaint);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        cooldownTimer = 0;
    }
    
    public void AddPaint(int paint)
    {
        currentPaint = Mathf.Clamp(currentPaint + paint, 0, maxPaint);
        paintBar.SetPaint(currentPaint);
    }
}
