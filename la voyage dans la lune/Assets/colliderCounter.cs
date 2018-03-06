using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderCounter : MonoBehaviour {

	[HideInInspector] public List<Collider2D> collidedObjects = new List<Collider2D>();
	public int colliderCount;

	void OnCollisionEnter2D(Collision2D col)
	{
		if(!collidedObjects.Contains(col.collider))
			collidedObjects.Add(col.collider);
		colliderCount = collidedObjects.Count;
	}

	void OnCollisionStay2D(Collision2D col)
	{
		OnCollisionEnter2D(col);
	}

	void FixedUpdate()
	{
		collidedObjects.Clear();
	}
}
