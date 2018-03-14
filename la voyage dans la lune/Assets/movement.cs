using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {


	//getting player input
	void Update()
	{
		if(Input.GetKeyDown("space") && isGrounded)
			startJumping();
		
		if(isJumping)
			movementAxis = Input.GetAxis("Horizontal");
		else
			movementAxis = Input.GetAxisRaw("Horizontal");

		jumpCurve = generateCurve();
	}

	[SerializeField] public float speed;
	float movementAxis;
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().drag = 0;
		isGrounded = GetComponentInChildren<legCollider>().isGrounded;
		if(!isGrounded && !isJumping)
		{
			isFalling = true;
			
		}
		if(isJumping|| isFalling)
		{
			Debug.Log("falltime:" + fallTime);
			gravity = evaluateDelta(fallTime, (fallTime + 1));
			Debug.Log(gravity);
			fallTime += 1;
		}
		if(isGrounded && (!isJumping || fallTime > 10))
		{
			fallTime = jumpVertex.x;
			isJumping = false;
			isFalling = false;
			gravity = 0;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(movementAxis * speed, gravity * 60); //we are multiplying by 60 to convert gravity from m/sec m/frame

	}

	public bool isGrounded;
	public bool isJumping;
	public bool isFalling;
	[SerializeField] AnimationCurve jumpCurve;
	[SerializeField] float gravity;
	public Vector2 jumpVertex;
	[SerializeField] float fallTime;

	void startJumping()
	{
		Debug.Log("jump Started");
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
}

