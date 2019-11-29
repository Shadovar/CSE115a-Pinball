using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballScript : MonoBehaviour
{

    public Rigidbody2D rigidBall;
    public Vector3 startPos;
    public AudioClip hitWallSound;
    public AudioSource hitWallSource;
    public int bonustracker = 0;
    private float forceFromFlipper = 50f;
    public int bonus1 = 0;
    public int bonus2 = 0;
    public int bonus3 = 0;
    // Start is called before the first frame update
    void Start()
    {
        hitWallSource.clip = hitWallSound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (rigidBall.velocity.magnitude > 2)
        {
            hitWallSource.Play();
            Debug.Log("played");
        }
        else
        {
            Debug.Log(rigidBall.velocity.magnitude);
        }
        if (collision.gameObject.CompareTag("Flipper"))
        {
            Debug.Log("PinballScript: Hit Flipper");
            //rigidBall.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);

        }
        // hit bumpers
        /*if (collision.transform.name == "Triangle")
        {
            ScoreControl.scorevalue += 10;
        }*/
        
        // lose the game
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            bonustracker = 0;
            bonus1 = 0;
            transform.position = startPos;
            rigidBall.velocity = new Vector2(0, 0);
            LauncherSpring.userCanLaunch = true;
        }
        // bonus points
        if (collision.transform.name == "Bonus1")
        {
            bonustracker += 1;
            //int bonus1 = 0;
            bonus1++;
            Debug.Log("bonus1: "+ bonus1);
            if (bonustracker >= 3 && bonus1 == 1)
            {
                ScoreControl.scorevalue += 100;
                bonustracker = 0;
                bonus1 = 0;
            }
        }
        if (collision.transform.name == "Bonus2")
        {
            bonustracker += 1;
            //int bonus2 = 0;
            bonus2++;
            Debug.Log("bonus2: " + bonus2);
            if (bonustracker >= 3 && bonus2 == 1)
            {
                ScoreControl.scorevalue += 100;
                bonustracker = 0;
                bonus2 = 0;
            }
        }
        if (collision.transform.name == "Bonus3")
        {
            bonustracker += 1;
            //int bonus3 = 0;
            bonus3++;
            Debug.Log("bonus3: " + bonus3);
            if (bonustracker >= 3 && bonus3 == 1)
            {
                ScoreControl.scorevalue += 100;
                bonustracker = 0;
                bonus3 = 0;
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        //Debug.Log("Pinball Script: OnCollisionEnter2D: " + collision.gameObject.name + ", " + collision.gameObject.tag);

        if (collision.gameObject.tag.Equals("Bumper"))
        {
            //Debug.Log("pinballScript: Hit bumper");
            ScoreControl.scorevalue += 10;
            Vector2 colliderLoc = collision.transform.position;
            float outwardForce = 100f;
            rigidBall.AddForce(outwardForce * (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - colliderLoc));
            //rigidBall.AddForce(-rigidBall.velocity * 50);
        }
    }

    public void FlipperCollision()
    {
        Debug.Log("pinballScript: recived message for Flipper Collision");
        rigidBall.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);
    }
}
