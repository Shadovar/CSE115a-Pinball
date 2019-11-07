using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    public LaunchBarrier barrier;
    public KeyCode launchCode = KeyCode.Space;
    public Vector3 launchPos; //= new Vector3(8.075f, -0.5f, 0);
    public pinballScript newBall, cb;
    public float springHeight;
    private int numFramesHeld = 0;
    int MaxNumFramesHold = 30 * 3;
    // Start is called before the first frame update
    void Start()
    {
        newBall.startPos = launchPos;
        launchPos = transform.position;
        launchPos.y += transform.localScale.y * 1;
        springHeight = transform.localScale.y * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //newBall.rigidBall.velocity = new Vector2(0, 0);
        if (Input.GetKeyDown(launchCode))
        {
            numFramesHeld = 0;
            if (cb != null)
                DestroyImmediate(cb.gameObject);
            cb = Instantiate(newBall, new Vector3(launchPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight), Quaternion.identity);

        }
        else if (Input.GetKey(launchCode))
        {
            numFramesHeld = (numFramesHeld < MaxNumFramesHold) ? (numFramesHeld + 1) : MaxNumFramesHold;
            cb.rigidBall.position = new Vector3(launchPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight);
        }
        else if (Input.GetKeyUp(launchCode))
        {
            float applyForce = 150 + (numFramesHeld * 300) / (float)MaxNumFramesHold;
            cb.rigidBall.position = new Vector3(launchPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight);
            cb.rigidBall.AddForce(new Vector2(0.0f, applyForce), ForceMode2D.Impulse);
            Debug.Log("Launch Intensity: " + applyForce);
            numFramesHeld = 0;
            barrier.collider.isTrigger = true;
        }
        transform.localScale = new Vector3(1, 1.0f - (numFramesHeld / (float)MaxNumFramesHold), 0);
        transform.localPosition = new Vector3(launchPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight);
    }
}
