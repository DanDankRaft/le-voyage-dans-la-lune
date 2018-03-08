using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderCounter : MonoBehaviour {

	[HideInInspector] public List<Collider2D> collidedObjects = new List<Collider2D>();
	public int colliderCount;

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log(col.gameObject.name);
		calculateCollisions(col);
	}

	void calculateCollisions(Collision2D col)
	{
		//Debug.Log(col.gameObject.name);
		if(!collidedObjects.Contains(col.collider))
			collidedObjects.Add(col.collider);
	}

	void FixedUpdate()
	{
		colliderCount = collidedObjects.Count;
		//collidedObjects.Clear();
	}

	void OnCollisonExit2D(Collision2D col)
	{
		collidedObjects.Remove(col.collider);
	}
}
