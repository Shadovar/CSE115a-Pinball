using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    public LaunchBarrier barrier;
    public static KeyCode launchCode = KeyCode.Space;
    public static Vector3 launchPos = new Vector3(8.075f, -0.5f, 0);
    public BallTesting newBall;
    private int numFramesHeld = 0;
    int MaxNumFramesHold = 30 * 3;
    // Start is called before the first frame update
    void Start()
    {
        newBall.startPos = launchPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(launchCode))
        {
            numFramesHeld = 0;
        }
        else if (Input.GetKey(launchCode))
        {
            numFramesHeld = (numFramesHeld < MaxNumFramesHold) ? (numFramesHeld + 1) : MaxNumFramesHold;
            newBall.rigidBall.position = new Vector3(launchPos.x, launchPos.y - (1.0f + (numFramesHeld / (float)MaxNumFramesHold)));
        }
        else if (Input.GetKeyUp(launchCode))
        {
            float applyForce = (numFramesHeld * 300) / (float)MaxNumFramesHold;
            newBall.rigidBall.position = new Vector3(launchPos.x, launchPos.y - (1.0f + (numFramesHeld / (float)MaxNumFramesHold)));
            newBall.rigidBall.AddForce(new Vector2(0.0f, applyForce), ForceMode2D.Impulse);
            Debug.Log("Launch Intensity: " + applyForce);
            numFramesHeld = 0;
            barrier.collider.isTrigger = true;
        }
        transform.localScale = new Vector3(1, 1.0f - (numFramesHeld / (float)MaxNumFramesHold), 0);
        transform.localPosition = new Vector3(launchPos.x, launchPos.y - (2.0f + (numFramesHeld / (float)MaxNumFramesHold)));
    }
}
