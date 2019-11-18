using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torqueFlipper : MonoBehaviour
{
    public Rigidbody2D rb;
    float rightMaxRot = 145; //default value: 145
    float rightRestingRot = 210; //default value: 210
    float upTorque = -360; //default 18
    float downTorque = 100; //default -8
    public AudioClip rightFlipperSound;
    public AudioSource rightFlipperSource;
    public KeyCode rightFlipperKey = KeyCode.RightShift;
    // Start is called before the first frame update
    void Start()
    {
        rightFlipperSource.clip = rightFlipperSound;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Right flipper functionality
        if (Input.GetKeyDown(rightFlipperKey))
        {
            rightFlipperSource.Play();
        }

        if (Input.GetKey(rightFlipperKey))
        {
            //if it hasn't reached its maximum
            //uses subtraction for upward rotation because rotating clockwise
            if (transform.eulerAngles.z >= rightMaxRot)
            {
                rb.freezeRotation = false;
                rb.AddTorque(upTorque);
            }
            else
            {
                rb.freezeRotation = true;
            }

        }
        else
        {
            //if it hasn't reached its base
            if (transform.eulerAngles.z <= rightRestingRot)
            {
                rb.freezeRotation = false;
                rb.AddTorque(downTorque);
            }
            else
            {
                rb.freezeRotation = true;
            }
        }
    }
}
