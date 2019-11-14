using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightFlipper : MonoBehaviour
{
    float rightMaxRot = 145; //default value: 145
    float rightRestingRot = 210; //default value: 210
    float upSpeed = 18; //default 18
    float downSpeed = -8; //default -8
    public AudioClip rightFlipperSound;
    public AudioSource rightFlipperSource;
    public KeyCode rightFlipperKey = KeyCode.Period;

    //Sets inputs for the right flipper

    // Start is called before the first frame update
    void Start()
    {
        rightFlipperSource.clip = rightFlipperSound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(rightFlipperKey))
        {
            rightFlipperSource.Play();
        }
        if (Input.GetKey(rightFlipperKey))
        {
            Debug.Log("righthit?");
            //if it hasn't reached its maximum
            //uses subtraction for upward rotation because rotating clockwise
            if (transform.eulerAngles.z >= rightMaxRot)
            {
                transform.Rotate(new Vector3(0, 0, -upSpeed));
            }
    
        }
        else
        {
            //if it hasn't reached its base
            if (transform.eulerAngles.z <= rightRestingRot)
            {
                transform.Rotate(new Vector3(0, 0, -downSpeed));
            }
        }
    }
}
