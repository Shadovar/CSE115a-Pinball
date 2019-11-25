﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftFlipper : MonoBehaviour
{
    float leftMaxRot = 35; //default value: 35
    float leftRestingRot = 330; //default value: 330
    float upSpeed = 18; //default 18
    float downSpeed = -8; //default -8
    public AudioClip leftFlipperSound;
    public AudioSource leftFlipperSource;
    public KeyCode LeftFlipperKey = KeyCode.Z;
    public GameObject childFlipper;
    public bool paused = false;
    //Sets inputs for the left flipper
   
    // Start is called before the first frame update
    void Start()
    {
        leftFlipperSource.clip = leftFlipperSound;   
    }

    public void leftChangePauseState()
    {
        paused = !paused;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(LeftFlipperKey))
            {
                leftFlipperSource.Play();
            }
            if (Input.GetKey(LeftFlipperKey))
            {
                //if it hasn't reached its maximum
                if (transform.eulerAngles.z <= leftMaxRot || transform.eulerAngles.z >= 300)
                {
                    transform.Rotate(new Vector3(0, 0, upSpeed));
                    //Tell child flipper to be able to push ball up
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
                //a range is being checked !(322, 330) because the left flipper has to go between 30 and 330 degrees
                if (transform.eulerAngles.z <= (leftRestingRot + downSpeed - 1)
                     || transform.eulerAngles.z >= leftRestingRot)
                {
                    //Debug.Log(transform.eulerAngles.z + " < " + (leftRestingRot + downSpeed - 1) + " || > " + leftRestingRot);
                    transform.Rotate(new Vector3(0, 0, downSpeed));
                    //Tell child flipper to no longer provide upward momentum to ball
                    childFlipper.gameObject.SendMessage("ChangeColliderState", false);
                }
                //else
                //{
                //    Debug.Log("Left at base");
                //}
            }
        }
    }
}
