using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCaller : MonoBehaviour
{
    public float riseVelocity = 2;
    private float timeSum = 0;
    private float random;
    public Material stdMaterial;
    public Material redMaterial;
    public float warningTime = 2;
    private bool warnMaterialIsActive = false;

    public float frequenzTime = 0.5f;
    public float frequenzTimeSum = 0;


    private void Start()
    {
        random = UnityEngine.Random.Range(3, 10);
    }

    void Update()
    {
        if (gameObject.transform.GetComponentInChildren<CallerTrigger>().triggered)
        {
            if(transform.position.y < 0)
            {
                transform.position += new Vector3(0, riseVelocity * Time.deltaTime, 0);
                if(transform.position.y >= 0)
                {
                    transform.position += new Vector3(0, -transform.position.y, 0);
                }
      
            }
        }

        if (gameObject.transform.GetComponentInChildren<CallerTrigger>().triggerExit)
        {
            timeSum += Time.deltaTime;

            if(timeSum >= (random - warningTime)){
                frequenzTimeSum += Time.deltaTime;
                if(frequenzTimeSum >= frequenzTime)
                {
                    frequenzTimeSum = 0;
                    ToggleMaterial();
                }
            }

            // hexagon is falling
            if(timeSum >= random)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }

    private void ToggleMaterial()
    {

        if (!warnMaterialIsActive)
        {
            warnMaterialIsActive = true;
            GetComponentInChildren<MeshRenderer>().material = redMaterial;
        } else
        {
            warnMaterialIsActive = false;
            GetComponentInChildren<MeshRenderer>().material = stdMaterial;
        }
    }
}
