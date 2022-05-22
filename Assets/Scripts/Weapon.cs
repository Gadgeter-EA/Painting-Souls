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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && cooldownTimer > attackCooldown && !controller.IsCrouching())
        {
            Shoot();
        }

        cooldownTimer += Time.deltaTime;
    }

    void Shoot()
    {
        animator.SetTrigger("IsAttacking");
        FindObjectOfType<AudioManager>().Play("AttackSound");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        cooldownTimer = 0;
    }
}
