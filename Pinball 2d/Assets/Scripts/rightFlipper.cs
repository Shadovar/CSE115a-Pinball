using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightFlipper : MonoBehaviour
{
    // Reference to gameObject fields
    public AudioClip rightFlipperSound;
    public AudioSource rightFlipperSource;   
    public GameObject childFlipper;

    // Reference to constant fields
    float rightMaxRot = 145;
    float rightRestingRot = 210;
    float upSpeed = 18;
    float downSpeed = -8;
    public KeyCode rightFlipperKey = KeyCode.M;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        rightFlipperSource.clip = rightFlipperSound;
    }



    // Update is called once per frame
    private void Update()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(rightFlipperKey))
            {
                rightFlipperSource.Play();
            }
        }
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        if (!paused)
        {
            if (Input.GetKey(rightFlipperKey))
            {
                //if it hasn't reached its maximum
                //uses subtraction for upward rotation because rotating clockwise
                if (transform.eulerAngles.z >= rightMaxRot)
                {
                    transform.Rotate(new Vector3(0, 0, -upSpeed));

                    //Tell flipper to be able to push ball up
                    childFlipper.gameObject.SendMessage("ChangeColliderState", true);
                }
                else
                {
                    //Tell flipper to no longer be able to push ball up
                    childFlipper.gameObject.SendMessage("ChangeColliderState", false);
                }
            }
            else
            {
                //if it hasn't reached its base
                if (transform.eulerAngles.z <= rightRestingRot)
                {
                    transform.Rotate(new Vector3(0, 0, -downSpeed));

                }

                //Tell flipper to no longer be able to push ball up
                childFlipper.gameObject.SendMessage("ChangeColliderState", false);
            }
        }
    }

    //Disable flipper when paused
    public void Pause()
    {
        paused = true;
    }

    //Enable flipper when unpaused
    public void Resume()
    {
        paused = false;
    }

    // When it detects a collision, tell it that it collided with a flipper
    private void OnCollisionEnter2D(Collision2D other)
    {
                other.gameObject.SendMessage("FlipperCollision");

    }
}
