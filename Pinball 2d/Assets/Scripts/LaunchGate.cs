using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGate : MonoBehaviour
{
    //Reference to gameObject fields
    public LaunchTrigger gateTrigger; // A collider which triggers when the ball passes through the gate
    public Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    public void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

}
