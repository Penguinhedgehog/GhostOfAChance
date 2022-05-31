using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float levitateMoveSpeed = 5f;
    [SerializeField] float levitateRate = 5f;
    [SerializeField] GameObject deadBody;

    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    Vector2 playerDirection;
    CapsuleCollider2D myPlayerCollider;
    Vector2 deathKick = new Vector2(0f, 20f);


    private bool isAlive = true;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myPlayerCollider = GetComponent<CapsuleCollider2D>();
    }

	void Update() {
        if(!isAlive) {return;}
        Move();
        Die();
	}

    void OnMove(InputValue value) {
        if(!isAlive) {return;}
        Debug.Log("Input value: " + value);
        moveInput = value.Get<Vector2>();
    }

    void OnLevitate() {
        Debug.Log("Levitate given object");
        var playerDirection = -(deadBody.transform.position - transform.position).normalized;
        FindObjectOfType<PlayerMovement>().PlayerRB().velocity = new Vector2(playerDirection.x, playerDirection.y) *20f;

    }

    void Move() {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidbody.velocity = playerVelocity;

    }


    void Die() {
        if(myPlayerCollider.IsTouchingLayers(LayerMask.GetMask("GhostHazards"))) {
            isAlive = false;
            myRigidbody.freezeRotation = false;
            myRigidbody.velocity = -deathKick;
            Invoke("ResetLevel", 3f);

        }
    }

    void ResetLevel() {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }


}
