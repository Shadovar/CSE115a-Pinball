using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    public LaunchGate gate;                     // Gate to prevent ball from re-entering launch area
    public LaunchBarrier barrier;               // Barrier preventing ball from going "inside" the spring
    public KeyCode launchCode = KeyCode.Space;  // Key the user uses to launch the labb
    public Vector3 launchPos;                   // Position which the ball is launched from
    public pinballScript newBall;               // The "static" refrence to the ball
    public AudioClip springSound;               // Sound of the spring
    public AudioSource springSource;            // AudioSource for the spring sound
    private Vector2 currentSpringPos;           // Position of the launchPad of the spring.
    private int numFramesHeld = 0;              // Number of fromes which launch key is held down
    int MaxNumFramesHold = 30 * 3;              // Max number of fromes which launch key is held down
    float springHeight;                         // Calculated height of the spring
    public static bool userIsLaunching = false; // Is the user currently holding the launch key?

    // Start is called before the first frame update
    void Start()
    {
        launchPos = transform.position;
        launchPos.y += transform.localScale.y * 1;
        currentSpringPos = new Vector2(launchPos.x, launchPos.y);
        springHeight = transform.localScale.y * 0.5f;
        springSource.clip = springSound;
        userIsLaunching = false;
    }

    // Update is called once per frame
    void Update()
    {
        newBall.startPos = launchPos;
        userIsLaunching = Input.GetKeyDown(launchCode) || Input.GetKey(launchCode) || Input.GetKeyUp(launchCode);
        if (userIsLaunching)
        {
            // The user is holding (or just released) the launch key
            newBall.rigidBall.position = new Vector3(currentSpringPos.x, currentSpringPos.y - 1.5f);
            newBall.rigidBall.velocity = new Vector2(0, 0);
            newBall.rigidBall.inertia = 0.0f;
            newBall.rigidBall.angularVelocity = 0;
            newBall.rigidBall.SetRotation(0);
            gate.GetComponent<Rigidbody2D>().simulated = false;
        }

        if (Input.GetKeyDown(launchCode))
        {
            // The user just pressed the launch key
            barrier.rigidbody.simulated = false;
            currentSpringPos = launchPos;
            numFramesHeld = 0;
        }
        else if (Input.GetKey(launchCode))
        {
            // The user is still holding the launch key
            numFramesHeld = (numFramesHeld < MaxNumFramesHold) ? (numFramesHeld + 1) : MaxNumFramesHold;
        }
        else if (Input.GetKeyUp(launchCode))
        {
            // The user just released the launch key
            float applyForce = 10 + (numFramesHeld * 100) / (float)MaxNumFramesHold;
            newBall.rigidBall.position = new Vector3(launchPos.x, launchPos.y);
            newBall.rigidBall.AddForce(new Vector2(0.0f, applyForce), ForceMode2D.Impulse);
            Debug.Log("Launch Intensity: " + applyForce);
            numFramesHeld = 0;
            barrier.rigidbody.simulated = true;
            springSource.Play();
        }

        currentSpringPos = new Vector2(currentSpringPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight);
        transform.localScale = new Vector3(1, 1.0f - (numFramesHeld / (float)MaxNumFramesHold), 0);
        transform.position = new Vector3(currentSpringPos.x, currentSpringPos.y - 2.0f);
    }

    public void restartGame()
    {
        newBall.rigidBall.position = new Vector3(launchPos.x, launchPos.y + 5);
    }
}
