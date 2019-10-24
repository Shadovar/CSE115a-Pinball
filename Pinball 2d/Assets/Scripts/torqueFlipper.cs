using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torqueFlipper : MonoBehaviour
{
    public Rigidbody2D rb;
    public float torqueUp;
    public float torqueDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Right Flipper Functionality

        //if right shift is held/pressed
        if (Input.GetKey(KeyCode.RightShift))
        {
            //if it hasn't reached its maximum
            //if (transform.eulerAngles.z >= 145)
            //{
                //Rotate
                rb.AddTorque(torqueUp);
            //}
        }
        else
        {
            //if it hasn't reached its base
            //if (transform.eulerAngles.z <= 210)
            //{
            //Rotate
            rb.AddTorque(torqueDown);
            //}

        }
    }
}
