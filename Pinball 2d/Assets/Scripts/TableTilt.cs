using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTilt : MonoBehaviour
{
    // References to gameObject fields
    public Rigidbody2D rigidbody2d;
    public GameObject camera;

    // References to constant fields
    public float minInterval = 0.5f;
    private float yRange = 1f;
    private float forceMin = 100f;
    private float forceMax = 1000f;
    private float timeLastTilt;
    private KeyCode LeftTiltInput = KeyCode.LeftShift;
    private KeyCode RightTiltInput = KeyCode.RightShift;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timeLastTilt = Time.realtimeSinceStartup;
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        if (UserCanTilt())
        {
            if (Input.GetKeyDown(LeftTiltInput))
            {
                rigidbody2d.AddForce(new Vector2(-1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
                timeLastTilt = Time.realtimeSinceStartup;
                camera.SendMessage("ShakeForDuration", .5f);
            }
            if (Input.GetKeyDown(RightTiltInput))
            {
                rigidbody2d.AddForce(new Vector2(1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
                timeLastTilt = Time.realtimeSinceStartup;
                camera.SendMessage("ShakeForDuration", .5f);
            }
        }
    }
    
    // Checks if enough time has passed since the last tilt and that game isn't paused
    bool UserCanTilt()
    {
        return (Time.realtimeSinceStartup - timeLastTilt >= minInterval) && !paused;
    }

    // Method to pause functionality
    public void Pause()
    {
        paused = true;
    }

    // Method to unpause functionality
    public void Resume()
    {
        paused = false;
    }
}
