using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    // References to gameObject fields
    public LaunchGate gate;                     // Gate to prevent ball from re-entering launch area
    public LaunchBarrier barrier;               // Barrier preventing ball from going "inside" the spring
    public pinballScript newBall;               // The "static" refrence to the ball
    public AudioClip springSound;               // Sound of the spring
    public AudioSource springSource;            // AudioSource for the spring sound

    // References to constants
    public KeyCode launchCode = KeyCode.Space;  // Key the user uses to launch the labb
    public Vector3 launchPos;                   // Position which the ball is launched from
    private Vector2 currentSpringPos;           // Position of the launchPad of the spring.
    private int numFramesHeld = 0;              // Number of fromes which launch key is held down
    int MaxNumFramesHold = 30 * 3;              // Max number of fromes which launch key is held down
    float springHeight;                         // Calculated height of the spring
    public static bool userIsLaunching = false; // Is the user currently holding the launch key?
    public static bool userCanLaunch = true;    // Can the user launch the ball?

    // Start is called before the first frame update
    void Start()
    {
        // Initialize fields
        launchPos = transform.position;

        launchPos.y += transform.localScale.y * 1;
        currentSpringPos = new Vector2(launchPos.x, launchPos.y);
        springHeight = transform.localScale.y * 0.5f;
        springSource.clip = springSound;
        newBall.startPos = launchPos;
    }


    // Update is called once per frame
    void Update()
    {
        // If the user can't launch, dont need to do anything
        if (!userCanLaunch)
        {
            return;
        }

        userIsLaunching = Input.GetKeyDown(launchCode) || Input.GetKey(launchCode) || Input.GetKeyUp(launchCode);
        if (userIsLaunching)
        {
            // The user is holding (or just released) the launch key
            newBall.rigidBall.position = new Vector3(currentSpringPos.x, currentSpringPos.y - 1.5f);
            newBall.rigidBall.velocity = new Vector2(0, 0);
            newBall.rigidBall.inertia = 0.0f;
            newBall.rigidBall.angularVelocity = 0;
            newBall.rigidBall.SetRotation(0);
            barrier.rigidbody2D.simulated = true;
        }

        if (Input.GetKeyDown(launchCode))
        {
            // The user just pressed the launch key
            barrier.rigidbody2D.simulated = false;
            currentSpringPos = launchPos;
            numFramesHeld = 0;
            gate.rigidbody2D.simulated = false;
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
            barrier.rigidbody2D.simulated = true;
            springSource.Play();
        }

        currentSpringPos = new Vector2(currentSpringPos.x, launchPos.y - (numFramesHeld / (float)MaxNumFramesHold) * springHeight);
        transform.localScale = new Vector3(1, 1.0f - (numFramesHeld / (float)MaxNumFramesHold), 0);
        transform.position = new Vector3(currentSpringPos.x, currentSpringPos.y - 2.0f);
    }

    // Resets the functionality of the launcher
    public void restartGame()
    {
        newBall.rigidBall.position = new Vector3(launchPos.x, launchPos.y + 0.0f);
        userCanLaunch = true;
        newBall.rigidBall.velocity = new Vector2(0, 0);
        newBall.rigidBall.inertia = 0.0f;
        newBall.rigidBall.angularVelocity = 0;
        newBall.rigidBall.SetRotation(0);
        barrier.rigidbody2D.simulated = true;
        gate.rigidbody2D.simulated = false;
    }

    // When the gate registers that the ball has passed it, disable launch capabilities
    public void UserCantLaunch()
    {
        userCanLaunch = false;
    }
}
