using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCollectible : MonoBehaviour
{
    [SerializeField] private int paintValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Weapon>().AddPaint(paintValue);
            FindObjectOfType<AudioManager>().Play("grabPaint");
            Destroy(gameObject);
        }
    }
}
