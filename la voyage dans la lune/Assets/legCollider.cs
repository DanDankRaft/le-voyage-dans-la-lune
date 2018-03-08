using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollider : MonoBehaviour {

    public bool isGrounded = false;
    BoxCollider2D box;
    Vector2 min;
    Vector2 max;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        box.enabled = true;
        min = box.bounds.min;
        max = box.bounds.max;
        box.enabled = false;

        isGrounded = Physics2D.OverlapArea(min, max);
    }
}
