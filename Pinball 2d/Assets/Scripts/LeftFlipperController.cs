using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFlipperController : MonoBehaviour
{
    float leftMaxRot = 35; //default value: 35
    float leftRestingRot = 330; //default value: 330
    float upSpeed = 18; //default 18
    float downSpeed = -8; //default -8

    //Sets inputs for the left flipper
    bool leftFlipperInput()
    {
        return (Input.GetKey(KeyCode.LeftShift)/* || Input.GetKey(KeyCode.Z)*/);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (leftFlipperInput())
        {
            //if it hasn't reached its maximum
            if (transform.eulerAngles.z <= leftMaxRot || transform.eulerAngles.z >= 300)
            {
                transform.Rotate(new Vector3(0, 0, upSpeed));
            }
        }
        else
        {
            //if it hasn't reached its base
            //a range is being checked !(322, 330) because the left flipper has to go between 30 and 330 degrees
            if (transform.eulerAngles.z <= (leftRestingRot + downSpeed - 1)
                 || transform.eulerAngles.z >= leftRestingRot)
            {
                Debug.Log(transform.eulerAngles.z + " < " + (leftRestingRot + downSpeed - 1) + " || > " + leftRestingRot);
                transform.Rotate(new Vector3(0, 0, downSpeed));
            }
            else
            {
                Debug.Log("Left at base");
            }
        }
    }
}
