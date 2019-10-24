using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(transform.rotation.z > 0)
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
        else if(transform.rotation.z < 0)
        {
            transform.Rotate(new Vector3(0, 0, 5));
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z < 100)
        {
            transform.Rotate(new Vector3(0, 0, 10));
        }*/

        //Right Flipper Functionality

        //if right shift is held/pressed
        if (Input.GetKey(KeyCode.RightShift))
        {
            //if it hasn't reached its maximum
            if(transform.eulerAngles.z >= 145)
            {
                //Rotate
                transform.Rotate(new Vector3(0, 0, -35));
            }
        }
        else
        {
            //if it hasn't reached its base
            if(transform.eulerAngles.z <= 210)
            {
                //Rotate
                transform.Rotate(new Vector3(0, 0, 10));
            }

        }
    }
}
