using UnityEngine;
using System.Collections;

public class deathHandling : MonoBehaviour {

	public bool isDead = false;

	//the randomness of falling direction
	public float randomness = 2f;

	//stength of the bouncing force when hit by a solid object
	public float reactionForceModifier = 30f;

	//the relative velocity between player and colliding body above which the collision will become deadly.
	public float impactVelocityThreshold = 15f; 

	//time of delay before restart the current level.
	public float delay = 5;

	//time elapsed after death
	float timeElapsed;

	Rigidbody playerRB;
	GameObject controller;
	bool loadingScene = false;
	Vector3 initialPosition;
	Quaternion initialRotation;
	//bool* fadingIn, fadingOut;

	void Start () {
		playerRB = GetComponent<Rigidbody>();
		controller = GameObject.Find("GameController");
		initialPosition = playerRB.transform.position;
		initialRotation = playerRB.transform.rotation;
		//fadingOut = &(controller.GetComponent<FadingTransition>().sceneEnding);
		//fadingIn = &(controller.GetComponent<FadingTransition>().sceneStarting);
	}

	void Update () {
		if (isDead){
			timeElapsed += Time.deltaTime;
//			print ("incremented");
		}

		if (timeElapsed > delay){
			//*fadingOut = true;


		//	if (Input.anyKeyDown && !(*fadingOut))
		//	{
				isDead = false;
				timeElapsed = 0f;
				playerRB.MovePosition(initialPosition);
				playerRB.rotation = initialRotation;
				print ("resurrected!");
				controller.GetComponent<FadingTransition>().sceneStarting = true;
			//}
			//stop her rolling around.
			//playerRB.freezeRotation = true;

			//controller.GetComponent<Fading>().BeginFading(1);
		}



		

//		if (Input.anyKeyDown)
//		{
//			controller.GetComponent<ChangeLevelTemp>().ActivateScene();
//		}

	}

//	IEnumerator reloadCurrentLevel() {


//	}

	void OnCollisionEnter (Collision collision) {

		//check if colliding with environment and check reletive speed.
		if (collision.gameObject.tag == "Environment"
		    && collision.relativeVelocity.magnitude > impactVelocityThreshold) {

			die(collision);
		}
	}

	//handle death. Let her fall to the ground in a random direction and disable movement
	void die(Collision collisionInfo) {
		isDead = true;

		//debug.
		print ("died!");

		//unfreeze rotation to let her fall down.
		playerRB.constraints = RigidbodyConstraints.None;

		//add some randomness to the way she falls by randomizing the position of the force applied
		playerRB.AddExplosionForce(collisionInfo.relativeVelocity.magnitude * reactionForceModifier, 
		                           collisionInfo.contacts[0].point + Random.insideUnitSphere * randomness, 
		                           5.0f, 2.0f);

		//disable player control of her movement.
		GetComponent<CharacterMovement>().enabled = false;

		timeElapsed = 0f;
	}
}
