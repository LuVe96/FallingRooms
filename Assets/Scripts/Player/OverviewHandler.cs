using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewHandler : MonoBehaviour
{

    private Vector3 stdPostion;
    public Vector3 overviewPostition;

    private PlayerInputActions playerInputActions;
    private bool buttonPressed = false;
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

        //overviewPostition = new Vector3(PlatformsTransform.position.x / 2, overviewPostition.y, PlatformsTransform.position.z + overviewPostition.z);
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
        //if (inOverview)
        //{
        //    if (buttonPressed)
        //    {
        //        if (transform.position.y >= overviewPostition.y && transform.position.z <= overviewPostition.z)
        //        {
        //            timeSum = 0;
        //        }
        //        else
        //        {
        //            timeSum += Time.deltaTime;
        //            Vector3 newPos = Vector3.Lerp(stdPostion, overviewPostition, timeSum / duration);
        //            transform.localPosition = newPos;
        //        }

        //    }
        //    else
        //    {
        //        if (transform.position.y <= stdPostion.y && transform.position.z >= stdPostion.z)
        //        {
        //            timeSum = 0;
        //        }
        //        else
        //        {
        //            timeSum += Time.deltaTime;
        //            Vector3 newPos = Vector3.Lerp(overviewPostition, stdPostion, timeSum / duration);
        //            transform.localPosition = newPos;
        //        }
        //    }
        //}

        int divider = 10;
        //float speed = 20 * Time.deltaTime;

        for (int i = 0; i < divider; i++)
        {
            if (buttonPressed)
            {

                if (transform.localPosition.y <= overviewPostition.y)
                {
                    transform.localPosition += new Vector3(0, direction.y * speedUp * Time.fixedDeltaTime / divider, 0);
                }
                if (transform.localPosition.z >= overviewPostition.z)
                {
                    transform.localPosition += new Vector3(0, 0, direction.z * speedUp * Time.fixedDeltaTime / divider);
                }
                //if (transform.localPosition.x <= overviewPostition.x)
                //{
                //    transform.localPosition += new Vector3(direction.x * speedUp * Time.deltaTime / divider, 0, 0);
                //}
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
                //if (transform.localPosition.x >= stdPostion.x)
                //{
                //    transform.localPosition -= new Vector3(direction.x * speedUp * Time.deltaTime / divider, 0, 0);
                //}
            }
        }



        if (!buttonPressed)
        {
           
            if (transform.localPosition.y <= 12)
            {
                timeSum = 0;
                inOverview = false;
            }
        }
    }

    private void Overview_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        buttonPressed = true;
        direction = (overviewPostition - stdPostion).normalized;
        inOverview = true;
      
    }

    private void Overview_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        buttonPressed = false;
    }
}


//if (buttonPressed)
//            {
//                if (transform.position.y <= overviewPostition.y)
//                {
//                    transform.position += new Vector3(0, direction.y* speed / divider, 0);
//                }
//                if(transform.position.z >= overviewPostition.z)
//                {
//                    transform.position += new Vector3(0,0, direction.z* speed / divider);
//                }
//            }
//            else
//            {
//                if (transform.position.y >= stdPostion.y)
//                {
//                    transform.position -= new Vector3(0, direction.y* speed / divider, 0);
//                }
//                if (transform.position.z <= stdPostion.z)
//                {
//                    transform.position -= new Vector3(0, 0, direction.z* speed / divider);
//                }

//            }       


//int divider = 10;
//        //float speed = 20 * Time.deltaTime;

//        for (int i = 0; i<divider; i++)
//        {
//            if (buttonPressed)
//            {

//                if (transform.localPosition.y <= overviewPostition.y)
//                {
//                    transform.localPosition += new Vector3(0, direction.y* speedUp * Time.fixedDeltaTime / divider, 0);
//                }
//                if (transform.localPosition.z >= overviewPostition.z)
//                {
//                    transform.localPosition += new Vector3(0, 0, direction.z* speedUp * Time.fixedDeltaTime / divider);
//                }
//                //if (transform.localPosition.x <= overviewPostition.x)
//                //{
//                //    transform.localPosition += new Vector3(direction.x * speedUp * Time.deltaTime / divider, 0, 0);
//                //}
//            }
//            else
//            {
//                if (transform.localPosition.y >= stdPostion.y)
//                {
//                    transform.localPosition -= new Vector3(0, direction.y* speedDown * Time.fixedDeltaTime / divider, 0);
//                }
//                if (transform.localPosition.z <= stdPostion.z)
//                {
//                    transform.localPosition -= new Vector3(0, 0, direction.z* speedDown * Time.fixedDeltaTime / divider);
//                }
//                //if (transform.localPosition.x >= stdPostion.x)
//                //{
//                //    transform.localPosition -= new Vector3(direction.x * speedUp * Time.deltaTime / divider, 0, 0);
//                //}
//            }
//        }