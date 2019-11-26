using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBarrier : MonoBehaviour
{
    public BoxCollider2D collider;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        collider.enabled = true;
        collider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exited");
        collider.isTrigger = false;
    }
}
