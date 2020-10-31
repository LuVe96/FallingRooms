using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    public float playerSpeed = 3f;
    public Transform playerModel;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
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

        if ( movmentVector.x <= -0.1)
        {
            transform.position += new Vector3(-playerSpeedWD, 0, 0);
        }
        else if (movmentVector.x >= 0.1)
        {
            transform.position += new Vector3(playerSpeedWD, 0, 0);
        }

        if (movmentVector.y <= -0.1)
        {
            transform.position += new Vector3(0, 0, -playerSpeedWD);
        }
        else if (movmentVector.y >= 0.1)
        {
            transform.position += new Vector3(0, 0, playerSpeedWD);
        }

        if (movmentVector.x != 0 && movmentVector.y != 0)
        {
            var rotation = Quaternion.LookRotation(new Vector3(movmentVector.x, 0, movmentVector.y));
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, rotation, 30f);
        }

    }
}
