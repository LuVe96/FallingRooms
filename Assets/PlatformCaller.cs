using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCaller : MonoBehaviour
{
    public float riseVelocity = 2;
    private float timeSum = 0;
    private float random;

    private void Start()
    {
        random = Random.Range(3, 10);
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
            if(timeSum >= random)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }



    


}
