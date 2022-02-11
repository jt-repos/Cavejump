using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidbody;
    //Animator myAnimator;
    BoxCollider2D edgeCheckCollider;
    CapsuleCollider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody2D>();
        edgeCheckCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void CheckCollision()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !edgeCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        }
    }
}
