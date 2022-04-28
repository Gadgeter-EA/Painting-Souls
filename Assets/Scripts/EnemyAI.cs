using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target; //Player

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0; // Current Point
    bool reachedEndOfPath = false; // To check if the path has ended

    Seeker seeker;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>(); //The Script that manage all Pathfinder AI System
        rb = GetComponent<Rigidbody2D>();
        
        // Method to continuosly creating a new Path, no waiting for it, and every 0.5 seconds
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //The path will consist of the current position of the enemy, the end
        // that is the target position, and the function to call when the path its calculated.
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone()) // Checking if there isn´t a path being calculated
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }


    void OnPathComplete(Path p) // Recieves the path
    {
        if (!p.error)
        {
            path = p;// Checking if the path has nos errors before de assign
            currentWaypoint = 0; // Needes to start at the beginning of a new path
        } 
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) // Checking if we have a path
            return;

        if (currentWaypoint >= path.vectorPath.Count) // Checking if we have reached end of path
        {
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; // Getting our next Waypoint
        Vector2 force = direction * speed * Time.deltaTime; // Force to applied to the enemy no move

        rb.AddForce(force); // Adding the force to the enemy
        
        // Getting distance to the next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) // Checking if we have reached the current Waypoint 
        {
            currentWaypoint++; 
        }
        
        // Flipping the Sprite of the enemy, using force and velocity for better aspect
        if(rb.velocity.x >= 0.01f && force.x > 0f) // Going to right
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x <= -0.01 && force.x < 0f) // Going to left
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
