using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollisionsDetector : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.contacts[0].p)
	}
}
