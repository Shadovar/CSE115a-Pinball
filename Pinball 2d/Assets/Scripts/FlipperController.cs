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
    void Update()
    {
        if(transform.rotation.z > 0)
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
        }
    }
}
