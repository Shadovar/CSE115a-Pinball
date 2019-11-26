using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFlipperCollision : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private float thrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Flipper"))
        {
            Debug.Log("BallFlipperCollision: Of type flipper");
            Debug.Log("BallFlipperCollision: Name of object is " + gameObject.transform.name);
            rb2d.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
        }
    }
}
