using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewHandler : MonoBehaviour
{

    private Vector3 stdPostion;
    public Vector3 overviewPostition;
    public GameObject drohne;

    private PlayerInputActions playerInputActions;
    private bool buttonPressed = false;
    private bool drohneIsFlying = false;
    private Vector3 direction;
    private float timeSum = 0;
    private float duration = 2f;

    public bool inOverview = true;
    public float speedUp = 20;
    public float speedDown = 20;
    

    //public Transform PlatformsTransform;

    void Awake()
    {
        stdPostion = transform.localPosition;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Overview.started += Overview_started;
        playerInputActions.Player.Overview.canceled += Overview_canceled;
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
    private void FixedUpdate()
    {

        int divider = 10;
        //float speed = 20 * Time.deltaTime;

        for (int i = 0; i < divider; i++)
        {
            if (buttonPressed && drohneIsFlying)
            {

                if (transform.localPosition.y <= overviewPostition.y)
                {
                    transform.localPosition += new Vector3(0, direction.y * speedUp * Time.fixedDeltaTime / divider, 0);
                }
                if (transform.localPosition.z >= overviewPostition.z)
                {
                    transform.localPosition += new Vector3(0, 0, direction.z * speedUp * Time.fixedDeltaTime / divider);
                }

            }
            else
            {
                if (transform.localPosition.y >= stdPostion.y)
                {
                    transform.localPosition -= new Vector3(0, direction.y * speedDown * Time.fixedDeltaTime / divider, 0);
                }
                if (transform.localPosition.z <= stdPostion.z)
                {
                    transform.localPosition -= new Vector3(0, 0, direction.z * speedDown * Time.fixedDeltaTime / divider);
                }

            }
        }



        if (!buttonPressed)
        {
            if (transform.localPosition.y <= 12)
            {
                timeSum = 0;
                inOverview = false;
                if (drohneIsFlying)
                {
                    drohneIsFlying = false;
                    //StartCoroutine(landDrohne());
                }


            }
        }
    }

    private void Overview_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        buttonPressed = true;
        direction = (overviewPostition - stdPostion).normalized;
        inOverview = true;

        StopAllCoroutines();
        StartCoroutine(startDrohne());
    }

    private void Overview_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        buttonPressed = false;
        drohne.GetComponent<Animator>().SetBool("IsStarting", false);
        drohne.GetComponent<Animator>().SetFloat("StartMultiplier", -1f);
        StartCoroutine(landDrohne());
    }

    IEnumerator startDrohne()
    {
        drohne.SetActive(true);
        drohne.GetComponent<Animator>().SetBool("IsStarting", true);
        drohne.GetComponent<Animator>().SetFloat("StartMultiplier", 1f);
        drohne.GetComponent<Animator>().SetBool("IsFlying", true);
        yield return new WaitForSeconds(0.2f);
        drohneIsFlying = true;

    }

    IEnumerator landDrohne()
    {
        yield return new WaitForSeconds(0.4f);
        drohne.GetComponent<Animator>().SetBool("IsFlying", false);
        yield return new WaitForSeconds(0.7f);
        drohne.SetActive(false);
    }
}
