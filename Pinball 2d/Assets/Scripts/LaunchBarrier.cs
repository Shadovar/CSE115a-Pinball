using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBarrier : MonoBehaviour
{
    // References to gameObject fields
    public Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    public void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();  
    }
}
