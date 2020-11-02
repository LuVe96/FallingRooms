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


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rigidbody = GetComponent<Rigidbody>();
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
        float playerSpeedWD = playerSpeed * Time.deltaTime;
        Vector2 movmentVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 newRigidbodyVelocity = new Vector3(0, rigidbody.velocity.y, 0);

        if ( movmentVector.x <= -0.1)
        {
            //transform.position += new Vector3(-playerSpeedWD, 0, 0);
            newRigidbodyVelocity = new Vector3(-playerSpeed, rigidbody.velocity.y, newRigidbodyVelocity.z);
        }
        else if (movmentVector.x >= 0.1)
        {
            //transform.position += new Vector3(playerSpeedWD, 0, 0);
            newRigidbodyVelocity = new Vector3(playerSpeed, rigidbody.velocity.y, newRigidbodyVelocity.z);
        }

        if (movmentVector.y <= -0.1)
        {
            //transform.position += new Vector3(0, 0, -playerSpeedWD);
            newRigidbodyVelocity = new Vector3(newRigidbodyVelocity.x, rigidbody.velocity.y, -playerSpeed);
        }
        else if (movmentVector.y >= 0.1)
        {
            //transform.position += new Vector3(0, 0, playerSpeedWD);
            newRigidbodyVelocity = new Vector3(newRigidbodyVelocity.x, rigidbody.velocity.y, playerSpeed);
        }

        if (movmentVector.x != 0 || movmentVector.y != 0)
        {
            var rotation = Quaternion.LookRotation(new Vector3(movmentVector.x, 0, movmentVector.y));
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, rotation, 10f);

        }

        rigidbody.velocity = newRigidbodyVelocity;

    }
}
