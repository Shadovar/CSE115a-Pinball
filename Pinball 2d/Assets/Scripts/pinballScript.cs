using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballScript : MonoBehaviour
{
    //References to gameObject fields
    public Rigidbody2D rigidbody2D;
    public LauncherSpring launcher;
    public AudioClip hitWallSound;
    public AudioSource hitWallSource;
	public GameObject[] indicatorLights;

    //References to primitive fields
    public Vector3 startPos;
    private float forceFromFlipper = 50f;
	public bool[] bonusStates;
    private float soundRequisiteSpeed = 2f;
    private float bumperOutwardForce = 100f;
    private Vector3 pausedVelocity;
    private float pausedGravity = 0f;

		
    // Start is called before the first frame update
    void Start()
    {
        hitWallSource.clip = hitWallSound;
		bonusStates = new bool[3];		
		ResetBonusStates();
        startPos = gameObject.transform.position;
        pausedVelocity = new Vector3(0, 0, 0);
    }

    // OnTriggerEnter2D is called when object enters an isTrigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the pinball is going fast at time of collision, make a noise
        if (rigidbody2D.velocity.magnitude > soundRequisiteSpeed)
        {
            hitWallSource.Play();
        }
        
        // If the pinball collides with something that ends the game
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            ResetBonusStates();
            launcher.RestartGame();
        }

        // If the pinball collides with one of the bonus point scoring bumpers
        if (collision.transform.name.StartsWith("Bonus"))
        {
            //Identify the correct one, accounting for offByOne errors
			int whichBonus = collision.transform.name[5] - '1';
			
            //If the one it hit wasn't lit up, light it up
			if (bonusStates[whichBonus]==false){
				bonusStates[whichBonus]=true;
                indicatorLights[whichBonus].GetComponent<SpriteRenderer>().color = Color.green;
                if (CheckBonus()){
					ScoreControl.scorevalue += 100;
					ResetBonusStates();
				}
			}
            //if it was already lit up, turn it off
			else {
				bonusStates[whichBonus]=false;
				indicatorLights[whichBonus].GetComponent<SpriteRenderer>().color = Color.red;
			}
        }
    }

    // OnCollisionEnter2D is called when the object collides with any collider
    private void OnCollisionEnter2D (Collision2D collision)
    {
        //If the pinball collided with a bumper
        if (collision.gameObject.tag.Equals("Bumper"))
        {
            //Uptick score, push ball away
            ScoreControl.scorevalue += 10;
            Vector2 colliderLoc = collision.transform.position;
            rigidbody2D.AddForce(bumperOutwardForce * (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - colliderLoc));
        }
    }

    // Method to pause functionality
    public void Pause()
    {
        //Save current movement values, then stop movement
        pausedVelocity = rigidbody2D.velocity;
        pausedGravity = rigidbody2D.gravityScale;
        rigidbody2D.velocity = new Vector3(0, 0, 0);
        rigidbody2D.gravityScale = 0f;
    }

    // Method to unpause functionality
    public void Resume()
    {
        rigidbody2D.velocity = pausedVelocity;
        rigidbody2D.gravityScale = pausedGravity;
    }

    // FlipperCollision will be called if one of the flippers registers a collision with the pinball, pushing it up
    public void FlipperCollision()
    {
        rigidbody2D.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);
    }

    // If all three bonus point bumpers have been hit, reset them to their default state
    void ResetBonusStates(){
		for (int i=0; i<3; i++){
			bonusStates[i]=false;
            indicatorLights[i].GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
	
    // Check if all three bonus point bumpers have been hit
	bool CheckBonus(){
		for (int i=0; i<3; i++){
			if (bonusStates[i]==false){
				return false;
			}
		}
		return true;
	}
}
