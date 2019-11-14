using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballScript : MonoBehaviour
{

    public Rigidbody2D rigidBall;
    public Vector3 startPos;
    public AudioClip hitWallSound;
    public AudioSource hitWallSource;
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
            ScoreControl.scorevalue += 10;
        }
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            transform.position = startPos;
        }

    }
}
