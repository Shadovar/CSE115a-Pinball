using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTilt : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private float yRange = 1f;
    private float forceMin = 100f;
    private float forceMax = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            rb2d.AddForce(new Vector2(-1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb2d.AddForce(new Vector2(1, Random.Range(-yRange, yRange)) * Random.Range(forceMin, forceMax));
        }
    }
}
