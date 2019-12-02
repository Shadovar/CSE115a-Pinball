using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballScript : MonoBehaviour
{

    public Rigidbody2D rigidBall;
    public LauncherSpring launcher;
    public Vector3 startPos;
    public AudioClip hitWallSound;
    public AudioSource hitWallSource;
    private float forceFromFlipper = 50f;
	public bool[] bonusStates;
	public GameObject[] indicatorLights;
    // Start is called before the first frame update
	void resetBonusStates(){
		for (int i=0; i<3; i++){
			bonusStates[i]=false;
            indicatorLights[i].GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
	
	bool checkBonus(){
		for (int i=0; i<3; i++){
			if (bonusStates[i]==false){
				return false;
			}
		}
		return true;
	}
		
	
    void Start()
    {
        hitWallSource.clip = hitWallSound;
		bonusStates = new bool[3];		
		resetBonusStates();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (rigidBall.velocity.magnitude > 2)
        {
            hitWallSource.Play();
            Debug.Log("played");
        }
        else
        {
            Debug.Log(rigidBall.velocity.magnitude);
        }
        if (collision.gameObject.CompareTag("Flipper"))
        {
            Debug.Log("PinballScript: Hit Flipper");
            //rigidBall.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);

        }
        // hit bumpers
        /*if (collision.transform.name == "Triangle")
        {
            ScoreControl.scorevalue += 10;
        }*/
        
        // lose the game
        if (collision.transform.name == "YouLose")
        {
            ScoreControl.scorevalue = 0;
            resetBonusStates();
            launcher.restartGame();
        }
        // bonus points
        if (collision.transform.name.StartsWith("Bonus"))
        {
			int whichBonus = collision.transform.name[5] - '1';
			
			if (bonusStates[whichBonus]==false){
				bonusStates[whichBonus]=true;
                indicatorLights[whichBonus].GetComponent<SpriteRenderer>().color = Color.green;
                if (checkBonus()){
					ScoreControl.scorevalue += 100;
					resetBonusStates();
				}
			}
			else {
				bonusStates[whichBonus]=false;
				indicatorLights[whichBonus].GetComponent<SpriteRenderer>().color = Color.red;
			}
            Debug.Log(collision.transform.name + bonusStates[whichBonus]);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        //Debug.Log("Pinball Script: OnCollisionEnter2D: " + collision.gameObject.name + ", " + collision.gameObject.tag);

        if (collision.gameObject.tag.Equals("Bumper"))
        {
            //Debug.Log("pinballScript: Hit bumper");
            ScoreControl.scorevalue += 10;
            Vector2 colliderLoc = collision.transform.position;
            float outwardForce = 100f;
            rigidBall.AddForce(outwardForce * (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - colliderLoc));
            //rigidBall.AddForce(-rigidBall.velocity * 50);
        }
    }

    public void FlipperCollision()
    {
        Debug.Log("pinballScript: recived message for Flipper Collision");
        rigidBall.AddForce(Vector2.up * forceFromFlipper, ForceMode2D.Impulse);
    }
}
