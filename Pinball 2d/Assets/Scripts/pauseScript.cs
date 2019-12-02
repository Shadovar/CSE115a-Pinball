using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{

    // References to gameObject fields
    public GameObject pauseMenu;
    public Rigidbody2D ball;   
    public leftFlipper leftFlipperScript;
    public rightFlipper RightFlipperScript;
    public LauncherSpring launcher;

    // References to primitive fields
    public static bool paused = false;
    Vector3 holdBallVelocity;
    float holdBallGravity;
    KeyCode PauseKey = KeyCode.P;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        ScoreControl.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PauseKey))
        {
            
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Called when pause is ended, allowing resumption of normal Update procedure
    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
        ball.velocity = holdBallVelocity;
        ball.gravityScale = holdBallGravity;
        leftFlipperScript.leftChangePauseState();
        RightFlipperScript.rightChangePauseState();
    }

    // Called when a restart is requested, restart game
    public void Restart()
    {
        pauseMenu.SetActive(false);
        paused = false;
        ball.gravityScale = holdBallGravity;
        leftFlipperScript.leftChangePauseState();
        RightFlipperScript.rightChangePauseState();
        launcher.restartGame();
    }

    // Called when a pause is requested, forcing normal update functionality to no longer work
    public void Pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
        holdBallVelocity = ball.velocity;
        holdBallGravity = ball.gravityScale;
        ball.velocity = new Vector3(0,0,0);
        ball.gravityScale = 0f;
        leftFlipperScript.leftChangePauseState();
        RightFlipperScript.rightChangePauseState();
    }

    // Called when exiting the game is desired, exiting the game
    public void exitGame()
    {
        ScoreControl.CommitScore("User");
        ScoreControl.Save();
        Application.Quit();
    }

}
