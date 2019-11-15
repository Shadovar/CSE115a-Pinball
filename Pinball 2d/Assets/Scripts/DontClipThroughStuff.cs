using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontClipThroughStuff : MonoBehaviour
{

    public Rigidbody2D rb;
    public CircleCollider2D cc2d;
    private float sqrBreadthOfCollider; //The squared size of how far the circle can reach
    private Vector2 previousLocation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        sqrBreadthOfCollider = Mathf.Min(cc2d.bounds.extents.x, cc2d.bounds.extents.y);
        sqrBreadthOfCollider = sqrBreadthOfCollider * sqrBreadthOfCollider; //Squaring it for comparison to magnitudes
        previousLocation = rb.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 stepMovement = rb.position - previousLocation;
        float sqrMovementMagnitude = stepMovement.sqrMagnitude;

        //If we have moved more than our minimum
        if(sqrMovementMagnitude > sqrBreadthOfCollider)
        {
            float movementMagnitude = Mathf.Sqrt(sqrMovementMagnitude);
            RaycastHit2D hitPossible;
            //if (Physics2D.Raycast())
            //{

            //}
        }
    }
}
