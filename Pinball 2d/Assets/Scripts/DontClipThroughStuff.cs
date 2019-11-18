using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontClipThroughStuff : MonoBehaviour
//Partially Retrieved from: https://wiki.unity3d.com/index.php?title=DontGoThroughThings#C.23_-_DontGoThroughThings.cs
{
    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;

    private int layerMask = 1 << 10; //Raycast will mask all layers except 10, the flipper layer
    public float skinWidth = 0.1f; //probably doesn't need to be changed 

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector2 previousPosition;
    private Rigidbody2D rb2d;
    private CircleCollider2D collider2d;

    //initialize values 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CircleCollider2D>();
        previousPosition = rb2d.position;
        minimumExtent = Mathf.Min(collider2d.bounds.extents.x, collider2d.bounds.extents.y);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector2 movementThisStep = rb2d.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit2D hitInfo = Physics2D.Raycast(previousPosition, movementThisStep, movementMagnitude, layerMask);

            //check for obstructions we might have missed 
            if (hitInfo.collider != null)
            {
                rb2d.position = hitInfo.point - (movementThisStep / movementMagnitude) /** partialExtent*/;
                Debug.Log("DontClip: collider name: " + hitInfo.collider.gameObject.name);
                rb2d.AddForce(-movementThisStep*500);
            }
        }

        previousPosition = rb2d.position;
    }
}
