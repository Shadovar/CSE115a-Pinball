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
        if (rigidBall.velocity.magnitude > 10)
        {
            hitWallSource.Play();
            Debug.Log("played");
        }
        else
        {
            Debug.Log(rigidBall.velocity.magnitude);
        }
        if (collision.transform.name == "Flipper")
        {
            Debug.Log("Hit Flipper");
        }
        // hit bumpers
        if (collision.transform.name == "Triangle")
        {
            ScoreControl.scorevalue += 10;
        }
        // lose the game
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            transform.position = startPos;
        }
        // bonus points
        if (collision.transform.name == "Bonus1")
        {
            bonustracker += 1;
            int bonus1 = 0;
            bonus1++;
            Debug.Log(bonus1);
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
            int bonus2 = 0;
            bonus2++;
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
            int bonus3 = 0;
            bonus3++;
            if (bonustracker >= 3 && bonus3 == 1)
            {
                ScoreControl.scorevalue += 100;
                bonustracker = 0;
                bonus3 = 0;
            }
        }
    }
}
