using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	void Start()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
	}

	//getting player input
	void Update()
	{
		if(Input.GetKeyDown("space"))
			startJumping();
		
		if(isJumping)
			movementAxis = Input.GetAxis("Horizontal");
		else
			movementAxis = Input.GetAxisRaw("Horizontal");
	}

	Rigidbody2D playerRigidbody;
	[SerializeField] public AnimationCurve jumpCurve;
	[SerializeField] public float speed;
	float movementAxis;
	[SerializeField] public float gravity;

	void FixedUpdate () {
		//jumping
		if(isJumping)
		{
			jumpDistance = jumpCurve.Evaluate(jumpFrame/(jumpTime*60)) - jumpCurve.Evaluate((jumpFrame - 1)/(jumpTime*60));
			jumpFrame++;
			if(jumpCurve.keys[jumpCurve.length - 1].time < (jumpFrame/(jumpTime*60))) //if jump is over (determined by wether the jumpFrame is bigger)
			{
				isJumping = false;
				jumpDistance = 0;
			}
			playerRigidbody.velocity = new Vector2(movementAxis * speed, jumpDistance * 60);
		} else
			playerRigidbody.velocity = new Vector2(movementAxis * speed, -gravity);

	}

	float jumpDistance;
	[SerializeField] bool isJumping = false;
	[SerializeField] int jumpFrame;
	float jumpTime = 1;
	public void startJumping()
	{
		jumpDistance = 0;
		isJumping = true;
		jumpFrame = 0;
	}
}
