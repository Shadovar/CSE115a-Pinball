using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftFlipper : MonoBehaviour
{
    // References to gameObject fields
    public AudioClip leftFlipperSound;
    public AudioSource leftFlipperSource;
    public GameObject childFlipper;
    
    // References for constant fields
    float leftMaxRot = 35;
    float leftRestingRot = 330;
    float upSpeed = 18;
    float downSpeed = -8;
    public bool paused = false;
    public KeyCode LeftFlipperKey = KeyCode.C;
 
    // Start is called before the first frame update
    void Start()
    {
        leftFlipperSource.clip = leftFlipperSound;   
    }

    // Update us called once per frame
    private void Update()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(LeftFlipperKey))
            {
                leftFlipperSource.Play();
            }
        }
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        if (!paused)
        {
            if (Input.GetKey(LeftFlipperKey))
            {
                //if it hasn't reached its maximum
                if (LessThanMaxRotation())
                {
                    transform.Rotate(new Vector3(0, 0, upSpeed));

                    //Tell child flipper to be able to provide upward momentum to ball
                    childFlipper.gameObject.SendMessage("ChangeColliderState", true);
                }
                else
                {
                    //Tell child flipper to no longer provide upward momentum to ball
                    childFlipper.gameObject.SendMessage("ChangeColliderState", false);
                }
            }
            else
            {
                //if it hasn't reached its base
                if (NotRestingRotation())
                {
                    transform.Rotate(new Vector3(0, 0, downSpeed));

                    //Tell child flipper to no longer provide upward momentum to ball
                    childFlipper.gameObject.SendMessage("ChangeColliderState", false);
                }
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

    //Check if the flipper is at less than maximum rotation
    bool LessThanMaxRotation()
    {
        // A range is being checked because it goes from 359 degrees to 1 degree when rotating
        return transform.eulerAngles.z <= leftMaxRot || transform.eulerAngles.z >= (leftRestingRot + 2 * downSpeed);
    }

    // Check if the flipper has fully returned to its resting position
    bool NotRestingRotation()
    {
        // A range is being checked because it goes from 1 degree to 359 degrees when rotating
        return transform.eulerAngles.z <= (leftRestingRot + downSpeed - 1) || transform.eulerAngles.z >= leftRestingRot;
    }
}
