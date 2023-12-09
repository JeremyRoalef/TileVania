using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float fltBounceAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, fltBounceAmount);
        }
    }

}
