using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrigger : MonoBehaviour
{
    // Reference to other gameObjects
    public LaunchGate launcherGate; // The physical gate object, not the trigger (this)
    public GameObject launcherSpring;

    // OnTriggerExit2D is called when a collider exits this gameObject's collider
    private void OnTriggerExit2D(Collider2D col)
    {
        //Ensure that the launch area doesn't interfere with play
        launcherGate.rigidbody2D.simulated = true;
        launcherSpring.SendMessage("UserCantLaunch");
    }
}
