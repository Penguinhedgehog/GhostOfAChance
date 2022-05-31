using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashCD = 1f;
    [SerializeField] GameObject ghostBody;
    [SerializeField] GameObject ghostSpawn;
    [SerializeField] GameObject aliveGround;
    [SerializeField] GameObject deathGround;
    [SerializeField] PhysicsMaterial2D deadPlayer;


    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    CapsuleCollider2D myPlayerCollider;

    private bool canDash = true;
    private float timeTillMove;
    private bool isAlive = true;
    private float startGravity;
    Vector2 deathKick = new Vector2(0f, 20f);

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myPlayerCollider = GetComponent<CapsuleCollider2D>();
        startGravity = myRigidbody.gravityScale;
        
    }

	void FixedUpdate() {
        if(!isAlive) {return;}
        Move();
        FlipSprite();
        updateDash();
        Die();
	}

    void OnMove(InputValue value) {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if(!isAlive) {return;}
        if (myPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ) {
            if(value.isPressed) {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }           
        }
    }

    void OnDash(InputValue value) {
        if(!isAlive || !canDash) {return;}
        if(value.isPressed) {
            myRigidbody.velocity +=  new Vector2(moveInput.x * dashSpeed, 0f);
            timeTillMove = dashCD;
            canDash = false;
        }
    }

    void Move() {
        if(!canDash) {
            return;
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }


    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void updateDash() {
        if(canDash) {
            return;
        } else {
            timeTillMove -= Time.deltaTime;
            if(timeTillMove < 0) {
                canDash = true;
            }
        }        
    }

    void Die() {
        if(myPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
            isAlive = false;
            myRigidbody.freezeRotation = false;
            //myAnimator.SetTrigger("die");
            myRigidbody.velocity = deathKick;
            Invoke("SpawnGhost", 5f);
        }
    }

    void SpawnGhost() {
        deathGround.SetActive(true);
        aliveGround.SetActive(false);        
        Instantiate(ghostBody, ghostSpawn.transform.position, Quaternion.identity);
    }

    public bool PlayerLife() {
        return isAlive;
    }

    public Rigidbody2D PlayerRB() {
        return myRigidbody;
    }


}
