using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollider : colliderCounter {

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
    }
}
