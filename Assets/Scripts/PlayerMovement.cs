using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    public float playerSpeed = 3f;
    public Transform playerModel;
    private Rigidbody rigidbody_;

    private Animator animator;
    float distToGround;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rigidbody_ = GetComponent<Rigidbody>();


        animator = transform.Find("Astronaut").GetComponent<Animator>();
        playerInputActions.Player.Jump.started += Jump_started;
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.Find("Main Camera").GetComponent<OverviewHandler>().inOverview ||
            transform.GetComponentInChildren<RagdollHandler>().isRagdollActive)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float playerSpeedWD = playerSpeed * Time.deltaTime;
        Vector2 movmentVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 newRigidbodyVelocity = new Vector3(0, rigidbody_.velocity.y, 0);

        if(movmentVector.x > 0.1 || movmentVector.x < -0.1 || movmentVector.y > 0.1 || movmentVector.y < -0.1)
        {
            newRigidbodyVelocity = (new Vector3(movmentVector.x, 0, movmentVector.y).normalized * playerSpeed)
            + new Vector3(0, rigidbody_.velocity.y, 0);
        }


        if(movmentVector.x > 0.1 || movmentVector.x < -0.1 || movmentVector.y > 0.1 || movmentVector.y < -0.1)
        {
            var rotation = Quaternion.LookRotation(new Vector3(movmentVector.x, 0, movmentVector.y));
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, rotation, 10f);
            animator.SetBool("isWalking", true);

        } else
        {
            animator.SetBool("isWalking", false);
        }

        rigidbody_.velocity = newRigidbodyVelocity;
        //if(IsGrounded()) animator.SetBool("isJumping", false);
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", true);
            rigidbody_.AddForce(new Vector3(0, 150, 0));
            isJumping = true;
            StartCoroutine(JumpEnding());
        }
    }
    bool isJumping = false;

    bool IsGrounded(){
       return Physics.Raycast(playerModel.transform.position, -Vector3.up, 0.1f);
     }

    IEnumerator JumpEnding()
    {
        bool isFalling = false;
        while (isJumping)
        {
            if (!Physics.Raycast(playerModel.transform.position, -Vector3.up, 0.5f))
            {
                isFalling = true;
            }

            if (Physics.Raycast(playerModel.transform.position, -Vector3.up, 0.4f) && isFalling)
            {
                animator.SetBool("isJumping", false);
                isJumping = false;
            }
            yield return null;
        }
    }
}
