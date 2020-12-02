using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    public float playerSpeed = 3f;
    public Transform playerModel;
    private Rigidbody rigidbody;

    private Animator animator;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rigidbody = GetComponent<Rigidbody>();


        animator = transform.Find("Astronaut").GetComponent<Animator>();
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
        if (transform.Find("Main Camera").GetComponent<OverviewHandler>().inOverview) return;

        float playerSpeedWD = playerSpeed * Time.deltaTime;
        Vector2 movmentVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 newRigidbodyVelocity = new Vector3(0, rigidbody.velocity.y, 0);

        if(movmentVector.x > 0.1 || movmentVector.x < -0.1 || movmentVector.y > 0.1 || movmentVector.y < -0.1)
        {
            newRigidbodyVelocity = (new Vector3(movmentVector.x, 0, movmentVector.y).normalized * playerSpeed)
            + new Vector3(0, rigidbody.velocity.y, 0);
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

        rigidbody.velocity = newRigidbodyVelocity;

    }
}
