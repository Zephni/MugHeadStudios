using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

[ExecuteInEditMode()]
[RequireComponent(typeof(BoxCollider2D))]
public class CollidableObject : MonoBehaviour
{
    Action<GameObject> onCollisionAction;
    Action<GameObject> onCollisionExitAction;
    Action<List<GameObject>> collidingAction;

    List<GameObject> colliders = new List<GameObject>();

    public void Awake()
    {
        Collider2D c2d = gameObject.GetComponent<BoxCollider2D>();
        c2d.transform.localScale = new Vector3(1, 1, 0);
    }

    public void Update()
    {
        if(collidingAction != null)
            collidingAction(colliders);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!colliders.Contains(col.gameObject))
            colliders.Add(col.gameObject);

        if (onCollisionAction != null)
            onCollisionAction(col.gameObject);
    }

    void OnCollisionExit(Collision2D col)
    {
        if (colliders.Contains(col.gameObject))
            colliders.Remove(col.gameObject);

        if (onCollisionExitAction != null)
            onCollisionExitAction(col.gameObject);
    }
}