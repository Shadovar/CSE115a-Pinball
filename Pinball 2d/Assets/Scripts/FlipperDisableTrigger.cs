using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperDisableTrigger : MonoBehaviour
{
    public PolygonCollider2D polygonCollider2D;
    private bool colliderEnabled = false;
    private bool colliderStateChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(colliderStateChanged && colliderEnabled)
        {
            polygonCollider2D.enabled = true;
            colliderStateChanged = false;
        }
        else if(colliderStateChanged && !colliderEnabled)
        {
            polygonCollider2D.enabled = false;
            colliderStateChanged = false;
        }
    }

    void ChangeColliderState(bool isEnabled)
    {
        if (colliderEnabled != isEnabled)
        {
            Debug.Log("FlipperDisableTrigger: ChangeColliderState called w/ different input");
            colliderEnabled = isEnabled;
            colliderStateChanged = true;
        }
    }
}
