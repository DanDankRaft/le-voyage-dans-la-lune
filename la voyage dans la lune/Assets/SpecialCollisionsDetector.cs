using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollisionsDetector : MonoBehaviour {

	public bool doingHeadCollisions;
	public bool doingSideCollisions;
	public bool doingLegCollisions;
	Vector3 firstContactPoint;
	Vector3 secondContactPoint;

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D playerCollider = GetComponent<Collider2D>();
		Vector3 firstContactPoint = col.contacts[0].point;
		Vector3 secondContactPoint = col.contacts[1].point;

		if(firstContactPoint.y == secondContactPoint.y && firstContactPoint.y > transform.position.y && doingHeadCollisions) //Head Collision
		{
			 col.gameObject.SendMessage("PlayerHeadCollision", SendMessageOptions.DontRequireReceiver); //Sends the message to both the player and the collider it hit.
			 SendMessage("PlayerHeadCollision", SendMessageOptions.DontRequireReceiver);
		}

		if( firstContactPoint.y == secondContactPoint.y && firstContactPoint.y < transform.position.y && doingLegCollisions) //Leg Collision
		{
			col.gameObject.SendMessage("PlayerLegCollision", SendMessageOptions.DontRequireReceiver);
			SendMessage("PlayerLegCollision", SendMessageOptions.DontRequireReceiver);
		}

		if(firstContactPoint.x == secondContactPoint.x && doingSideCollisions) //Side Collision
		{
			col.gameObject.SendMessage("PlayerSideCollision", SendMessageOptions.DontRequireReceiver);
			SendMessage("PlayerSideCollision", SendMessageOptions.DontRequireReceiver);
		}
	}

	Vector3 globalFirstContactPoint;
	Vector3 globalSecondContactPoint;
	void OnCollisionStay2D(Collision2D col)
	{
		Collider2D playerCollider = GetComponent<Collider2D>();
		Vector3 firstContactPoint = col.contacts[0].point;
		Vector3 secondContactPoint = col.contacts[1].point;
		globalFirstContactPoint = firstContactPoint;
		globalSecondContactPoint = secondContactPoint;

		if( firstContactPoint.y == secondContactPoint.y && firstContactPoint.y < transform.position.y && doingLegCollisions) //Leg Collision
		{
			col.gameObject.SendMessage("PlayerLegCollisionStay", SendMessageOptions.DontRequireReceiver);
			SendMessage("PlayerLegCollisionStay", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		Collider2D playerCollider = GetComponent<Collider2D>();

		if( globalFirstContactPoint.y == globalSecondContactPoint.y && globalFirstContactPoint.y < transform.position.y && doingLegCollisions) //Leg Collision
		{
			col.gameObject.SendMessage("PlayerLegCollisionExit", SendMessageOptions.DontRequireReceiver);
			SendMessage("PlayerLegCollisionExit", SendMessageOptions.DontRequireReceiver);
		}
	}
	public static double round(double originalNumber, int places)
	{
		float multipliedNumber = (float) (originalNumber*Mathf.Pow(10, places));
		return Mathf.Round(multipliedNumber) / Mathf.Pow(10, places);
	}

}
