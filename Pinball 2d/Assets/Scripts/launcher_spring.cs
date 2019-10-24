using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher_spring : MonoBehaviour
{
    public BallTesting NewBall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(NewBall, new Vector3(0,0,0), Quaternion.identity);
            Debug.Log("I pressed spase.");
        }
    }
}
