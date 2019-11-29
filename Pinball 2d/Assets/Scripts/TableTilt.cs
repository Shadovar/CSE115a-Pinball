using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTilt : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float minInterval = 0.5f;
    private float yRange = 1f;
    private float forceMin = 100f;
    private float forceMax = 1000f;
    private float timeLastTilt;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timeLastTilt = Time.realtimeSinceStartup;
    }

    bool userCanTilt()
    {
        return Time.realtimeSinceStartup - timeLastTilt >= minInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (userCanTilt())
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                rb2d.AddForce(new Vector2(-1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
                timeLastTilt = Time.realtimeSinceStartup;
                Debug.Log("Right tilt.");
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                rb2d.AddForce(new Vector2(1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
                timeLastTilt = Time.realtimeSinceStartup;
                Debug.Log("Right tilt.");
            }
        }
    }
}
