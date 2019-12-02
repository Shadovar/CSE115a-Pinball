using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFlipperCollision : MonoBehaviour
{
    //References to gameObject fields
    public Rigidbody2D rb2d;

    //Constants
    private float thrust = 50f;

    //Upon entering the collider of another object
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if it collided with a flipper
        if(other.gameObject.CompareTag("Flipper"))
        {
            //add an upward force
            rb2d.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
        }
    }
}
