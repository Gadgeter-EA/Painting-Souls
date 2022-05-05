using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    private Rigidbody2D rb;

    float horizontalMove = 0f; // Storing for passing to FixedUpdate
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VelocidadY", rb.velocity.y);
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if (Input.GetButtonUp("Crouch")) crouch = false;
        
    }

    public void Crouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    private void FixedUpdate() // Function dedicated to physics
    {
        // Move character
        // Multplying by TimeDeltaTime makes sure the function executes ALWAYS the same, independent of the PC
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
