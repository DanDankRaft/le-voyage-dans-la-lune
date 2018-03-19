using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
	
	//ALLLLLL THE PUBLIC VARIABLES
	public bool isGrounded;
	public bool isJumping;
	public bool isFalling;
	[SerializeField] AnimationCurve jumpCurve;
	[SerializeField] float gravity;
	public Vector2 jumpVertex;
	[SerializeField] float fallTime; //fallTime is where on the gravity curve the player is- even if they're not falling, but rather jumping or standing still, they are still somewhere on the fall curve

	//getting player input
	void Update()
	{
		if(Input.GetKeyDown("space") && isGrounded)
			startJumping();
		
		if(isJumping) //I use GetAxisRaw for normal movement because it gives a better feeling of perciseness, while normal GetAxis gives jumping a more natural, slippery feeling
			movementAxis = Input.GetAxis("Horizontal");
		else
			movementAxis = Input.GetAxisRaw("Horizontal");
		
		if(Input.GetAxisRaw("Horizontal") != 0)
			transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x) * Input.GetAxisRaw("Horizontal"), transform.lossyScale.y, transform.lossyScale.z);

		jumpCurve = generateCurve();
	}

	[SerializeField] public float speed;
	float movementAxis;
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().drag = 0; //used this to fix a bug, I think? i don't know anymore...

		isGrounded = GetComponent<groundChecker>().isGrounded;
		//isGrounded = legColliders > 0;
		if(!isGrounded && !isJumping) //well, if they're not on the ground, and they're not jumping, they're probably falling!
		{
			isFalling = true;
			
		}
		if(isJumping || isFalling) //use gravity based on the allmighty CURVE
		{
			gravity = evaluateDelta(fallTime, (fallTime + 1));
			fallTime += 1; //fallTime is measured in FixedUpdates
		}
		if(isGrounded && (!isJumping || fallTime > 10)) //the second part of the condition was to fix a bug, where the player would refuse to jump because there is still a collision
		{
			fallTime = jumpVertex.x;
			isJumping = false;
			isFalling = false;
			gravity = 0;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(movementAxis * speed, gravity * 60); //we are multiplying by 60 to convert gravity from m/sec m/frame

	}

	void startJumping()
	{
		isJumping = true;
		fallTime = 0;
	}

	AnimationCurve generateCurve()
	{
		AnimationCurve newCurve = new AnimationCurve();
		newCurve.AddKey(0,0);
		newCurve.AddKey(jumpVertex.x, jumpVertex.y);
		newCurve.AddKey(jumpVertex.x*2, 0);
		return newCurve;
	}

	float evaluateDelta(float firstValue, float secondValue)
	{
		return evaluateCurve(jumpVertex ,secondValue) - evaluateCurve(jumpVertex ,firstValue);
	}

	float evaluateCurve(Vector2 vertex, float point)
	{
		return (-vertex.y/(vertex.x*vertex.x))*(point-vertex.x)*(point - vertex.x) + vertex.y;
	}

	void PlayerHeadCollision()
	{
		if(isJumping)
			fallTime = jumpVertex.x*2 - fallTime;
	}

	public int legColliders = 0;
	void PlayerLegCollision()
	{
		Debug.Log("Collision entered");
		legColliders++;
	}

	void PlayerLegCollisionExit()
	{
		Debug.Log("Collision Exited");
		legColliders--;
	}
}

