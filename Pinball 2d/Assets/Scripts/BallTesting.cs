﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTesting : MonoBehaviour
{

    public Rigidbody2D rigidBall;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidBall.AddForce(transform.up * 3, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = startPos;
    }
}
