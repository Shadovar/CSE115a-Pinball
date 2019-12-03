using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{

    // References to gameObject fields
    public GameObject pauseMenu;
    public GameObject pinball; 
    public GameObject leftFlipper;
    public GameObject rightFlipper;
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

    // Unpause is the common functionalities between Resume and Restart
    private void Unpause()
    {
        pauseMenu.SetActive(false);
        paused = false;
        leftFlipper.gameObject.SendMessage("Resume");
        rightFlipper.gameObject.SendMessage("Resume");
    }

    // Called when pause is ended, allowing resumption of normal Update procedure
    public void Resume()
    {
        pinball.gameObject.SendMessage("Resume");
        pauseMenu.SetActive(false);
        paused = false;
        leftFlipper.gameObject.SendMessage("Resume");
        rightFlipper.gameObject.SendMessage("Resume");

    }

    // Called when a restart is requested, restart game
    public void Restart()
    {
        Resume();
        launcher.RestartGame();
    }

    // Called when a pause is requested, forcing normal update functionality to no longer work
    public void Pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
        pinball.gameObject.SendMessage("Pause");
        leftFlipper.gameObject.SendMessage("Pause");
        rightFlipper.gameObject.SendMessage("Pause");
    }

    // Called when exiting the game is desired, exiting the game
    public void ExitGame()
    {
        ScoreControl.CommitScore("User");
        ScoreControl.Save();
        Application.Quit();
    }

}
