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
    private float stdPlayerSpeed;
    private bool isShocked = false;
    private bool isFalling = false;

    public AudioSource jumpSound;
    public AudioSource jumpEndSound;
    private bool coliding = false;
    private bool mouseMovingPressed = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rigidbody_ = GetComponent<Rigidbody>();


        animator = transform.Find("Astronaut").GetComponent<Animator>();
        playerInputActions.Player.Jump.started += Jump_started;
        playerInputActions.Player.MouseMove.performed += MouseMove_started;
        playerInputActions.Player.MouseMove.canceled += MouseMove_started;
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        stdPlayerSpeed = playerSpeed;
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
        if (isFalling) { return; }

        if(!IsGrounded() && coliding) { return; }

        if (isShocked)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        if (transform.Find("CameraHolder").Find("Main Camera").GetComponent<OverviewHandler>().inOverview 
            /*transform.GetComponentInChildren<RagdollHandler>().isRagdollActive*/)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float playerSpeedWD = playerSpeed * Time.deltaTime;
        Vector2 movmentVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector2 mousePos = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        if (mouseMovingPressed)
        {
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            movmentVector = new Vector2(mousePos.x - playerScreenPos.x, mousePos.y - playerScreenPos.y);
        }

        Vector3 newRigidbodyVelocity = new Vector3(0, rigidbody_.velocity.y, 0);

        if(movmentVector.x > 0.1 || movmentVector.x < -0.1 || movmentVector.y > 0.1 || movmentVector.y < -0.1)
        {
            newRigidbodyVelocity = (new Vector3(movmentVector.x, 0, movmentVector.y).normalized * playerSpeed)
            + new Vector3(0, rigidbody_.velocity.y, 0);
        }


        if(movmentVector.x > 0.1 || movmentVector.x < -0.1 || movmentVector.y > 0.1 || movmentVector.y < -0.1)
        {
            var rotation = Quaternion.LookRotation(new Vector3(movmentVector.x, 0, movmentVector.y));
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, rotation, 400f * Time.deltaTime);
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        rigidbody_.velocity = newRigidbodyVelocity;
        //if(IsGrounded()) animator.SetBool("isJumping", false);
    }

    private void MouseMove_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
            mouseMovingPressed = true;
        if (obj.canceled)
            mouseMovingPressed = false;

    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsGrounded() && !isShocked)
        {
            animator.SetBool("isJumping", true);
            jumpSound.Play();
            rigidbody_.AddForce(new Vector3(0, 150, 0));
            isJumping = true;
            StartCoroutine(JumpEnding());
        }
    }
    bool isJumping = false;

    bool IsGrounded(){
       return Physics.Raycast(playerModel.transform.position, -Vector3.up, 0.1f);
     }

    public void setShocked(bool shocked)
    {
        isShocked = shocked;
        if (shocked)
        {
            playerSpeed = 0;
            animator.SetFloat("ShockedMultiplier", 13);
        } else
        {
            playerSpeed = stdPlayerSpeed;
            animator.SetFloat("ShockedMultiplier", 1);
        }

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
                jumpEndSound.Play();
                animator.SetBool("isJumping", false);
                isJumping = false;
            }
            yield return null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        coliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        coliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FallingTrigger")
        {
            animator.SetBool("isFalling", true);
            StartCoroutine(isDieing());
            isFalling = true;
            //foreach (var col in GetComponents<Collider>())
            //{
            //    col.enabled = false;
            //}

            //foreach (var col in GetComponentsInChildren<Collider>())
            //{
            //    col.enabled = false;
            //}
        }
    }

   

    IEnumerator isDieing()
    {
        Debug.LogWarning("Oh no");
        yield return new WaitForSeconds(2);
        GameManager.Instance.playerIsDead = true;
    }
}
