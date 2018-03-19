using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundChecker : MonoBehaviour {

	public bool isGrounded = false;
    Vector2 min;
    Vector2 max;

    void FixedUpdate()
    {
        /* 
        box.enabled = true;
        min = box.bounds.min;
        max = box.bounds.max;
        max.y = GetComponentInParent<Collider2D>().bounds.min.y;
        box.enabled = false;
        */
		Collider2D box = GetComponent<Collider2D>();
        float[] points = new float[2];
        points[0] = box.bounds.min.x;
        points[1] = box.bounds.max.x;
		Debug.Log();
        isGrounded = Physics.Raycast(new Vector2(points[0], transform.position.y), Vector2.down, box.bounds.extents.x + 0.05f) && Physics2D.Raycast(new Vector2(points[1], transform.position.y), Vector2.down, box.bounds.extents.x + 0.05f);
	}
}
