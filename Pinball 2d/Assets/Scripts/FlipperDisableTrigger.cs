using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperDisableTrigger : MonoBehaviour
{

    //References to gameObject fields
    public PolygonCollider2D polygonCollider2D;

    //Field caching current state
    private bool colliderEnabled;


    // Start is called before the first frame update
    void Start()
    {
        //As flipper starts at rest, start with collider disabled
        polygonCollider2D.enabled = false;
        colliderEnabled = false;
    }

    // ChangeColliderState is called when parent messages that the collider should be either enabled or disabled
    void ChangeColliderState(bool isEnabled)
    {
        // If the newly requested state differs from the current state, change the current state
        if (colliderEnabled != isEnabled)
        {
            polygonCollider2D.enabled = isEnabled;
            colliderEnabled = isEnabled;
        }
    }
}
