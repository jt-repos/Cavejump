using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //configuration parameters
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] int health = 3;
    [SerializeField] float immunityTime = 1f;
    [SerializeField] int numberOfBlinks = 10;
    [SerializeField] float deathAnimationTime = 1f;
    [SerializeField] int framesRewindAfterRespawn = 5;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //State
    bool isAlive = true;

    //cached reference
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D bodyPlayerCollider;
    BoxCollider2D feetPlayerCollider;
    float gravityScaleAtStart;
    List<Vector3> respawnPoints;

    //Constants
    int playerLayer = 10;
    int immuneLayer = 15;
    int playerClimbLayer = 17;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoints = new List<Vector3>();
        bodyPlayerCollider = GetComponent<CapsuleCollider2D>();
        feetPlayerCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Climb();
        Jump();
        FlipSprite();
        CheckIfHit();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 and +1
        Vector2 playerVelociy = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelociy;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; //mathf.epsilon = 0
        myAnimator.SetBool("Running", playerHasHorizontalSpeed); //run animation if player has horizontal speed
    }

    private void Jump()
    {
        if (!feetPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladder")))
        {
            return;
        } //if not touching ground, don't jump
        if(!bodyPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) && gameObject.layer == playerLayer)
        {
            SetRespawnPoint();
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if(myRigidbody.velocity.y == 0)
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocityToAdd;
            }
        }
    }

    private void SetRespawnPoint()
    {
        respawnPoints.Add(transform.position);
        if (respawnPoints.Count > 2 * framesRewindAfterRespawn)
        {
            respawnPoints.RemoveAt(0);
        }
    }

    /* private void Dash()
    {
        if (CrossPlatformInputManager.GetButtonDown("Dash") && !isDashing)
        {
            StartCoroutine(ProceedDashing());
        }
    }

    IEnumerator ProceedDashing()
    {
        isDashing = true;
        myAnimator.SetBool("Dashing", true);
        for (int i = 0; i < rollingFrames; i++)
        {
            myRigidbody.velocity += new Vector2((dashVelocity - i * dashVelocity / rollingFrames) * transform.localScale.x, 0); //zrob ze nie hard code, mozesz zmienic ze nie biega podczas dashowania i wtedy zmien velocity y na nie 0
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
        myAnimator.SetBool("Dashing", false);
    } */

    private void Climb()
    {
        if (!feetPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) //if not touching ladder, don't climb
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value is between -1 and +1
        if (Mathf.Abs(controlThrow) == 1)
        {
            myRigidbody.gravityScale = 0f;
            Vector2 climbVelocity = new Vector2(0, controlThrow * climbSpeed);
            myRigidbody.velocity = climbVelocity;
        }
        else
        {
            //gameObject.layer = playerLayer; //Player
            myRigidbody.gravityScale = 0f;
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, 0);
            myRigidbody.velocity = climbVelocity;
        }
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon; //mathf.epsilon = 0
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed); //run animation if player has horizontal speed
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; //mathf.epsilon = 0
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void CheckIfHit()
    {
        if (bodyPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            Die();
        } 
    }

    IEnumerator ImmunityFrames()
    {
        var color = GetComponent<SpriteRenderer>().color; 
        for (int i = 0; i < numberOfBlinks; i++)
        {
            color.a = 0.1f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(immunityTime / numberOfBlinks / 2);
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(immunityTime / numberOfBlinks / 2);
        }
        gameObject.layer = playerLayer; //player
    }

    private void Die()
    {
        health--;
        gameObject.layer = immuneLayer;
        isAlive = false;
        myAnimator.SetBool("Dying", true);
        GetComponent<Rigidbody2D>().velocity = deathKick;
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(deathAnimationTime);
        StartCoroutine(ImmunityFrames());
        isAlive = true;
        myAnimator.SetBool("Dying", false);
        transform.position = respawnPoints[respawnPoints.Count - framesRewindAfterRespawn - 1];
    }
    public int GetHealth()
    {
        return health;
    }
}
