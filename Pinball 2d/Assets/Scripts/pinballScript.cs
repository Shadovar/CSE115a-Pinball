using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballScript : MonoBehaviour
{
    //References to gameObject fields
    public Rigidbody2D rigidBall;
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

		
    // Start is called before the first frame update
    void Start()
    {
        hitWallSource.clip = hitWallSound;
		bonusStates = new bool[3];		
		resetBonusStates();
    }

    // OnTriggerEnter2D is called when object enters an isTrigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the pinball is going fast at time of collision, make a noise
        if (rigidBall.velocity.magnitude > soundRequisiteSpeed)
        {
            hitWallSource.Play();
        }
        
        // If the pinball collides with something that ends the game
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            resetBonusStates();
            launcher.restartGame();
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
                if (checkBonus()){
					ScoreControl.scorevalue += 100;
					resetBonusStates();
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
            rigidBall.AddForce(bumperOutwardForce * (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - colliderLoc));
        }
    }

    // FlipperCollision will be called if one of the flippers registers a collision with the pinball, pushing it up
    public void FlipperCollision()
    {
        rigidBall.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);
    }

    // If all three bonus point bumpers have been hit, reset them to their default state
    void resetBonusStates(){
		for (int i=0; i<3; i++){
			bonusStates[i]=false;
            indicatorLights[i].GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
	
    // Check if all three bonus point bumpers have been hit
	bool checkBonus(){
		for (int i=0; i<3; i++){
			if (bonusStates[i]==false){
				return false;
			}
		}
		return true;
	}
}
