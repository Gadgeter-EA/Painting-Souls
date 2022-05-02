 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint; // FirePoint that is an emptyObject
    public GameObject bulletPrefab; // Prefab of the bullet

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
