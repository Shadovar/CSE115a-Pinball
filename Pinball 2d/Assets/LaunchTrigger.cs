using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrigger : MonoBehaviour
{
    public LaunchGate gate; // The physical gate object, not the trigger (this)
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exited");
        gate.rigidBody.simulated = true;
        //gate.rigidbody.simulated = true;
    }
}
