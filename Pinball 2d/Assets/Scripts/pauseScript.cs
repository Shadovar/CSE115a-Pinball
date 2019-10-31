using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;
    public Rigidbody2D ball;
    Vector3 holdBallVelocity;
    float holdBallGravity;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed");
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

    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
        ball.velocity = holdBallVelocity;
        ball.gravityScale = holdBallGravity;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
        holdBallVelocity = ball.velocity;
        holdBallGravity = ball.gravityScale;
        ball.velocity = new Vector3(0,0,0);
        ball.gravityScale = 0f;
    }

    public void exitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

}
