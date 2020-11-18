using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{

    public float riseVelocity = 2;
    private float timeSum = 0;
    private float random;
    public Material stdMaterial;
    public Material redMaterial;
    public float warningTime = 2;
    private bool warnMaterialIsActive = false;

    [HideInInspector]
    public bool isFalling = false;
    public float frequenzTime = 0.5f;
    private float frequenzTimeSum = 0;

    public float deletionTime = 10f;
    private float deltionTimeSum = 0;

    private void Start()
    {
        random = UnityEngine.Random.Range(5, 8);
    }

    void Update()
    {
        if (gameObject.transform.Find("PlatformElevateTrigger").GetComponent<PlayerEnterTrigger>().triggered)
        {
            if (transform.position.y < 0)
            {
                transform.position += new Vector3(0, riseVelocity * Time.deltaTime, 0);
                if (transform.position.y >= 0)
                {
                    transform.position += new Vector3(0, -transform.position.y, 0);
                }

            }
        }

        if (gameObject.transform.Find("PlatformDropTrigger").GetComponent<PlayerEnterTrigger>().triggered)
        {
            timeSum += Time.deltaTime;

            if (timeSum >= (random - warningTime))
            {
                frequenzTimeSum += Time.deltaTime;
                if (frequenzTimeSum >= frequenzTime)
                {
                    frequenzTimeSum = 0;
                    ToggleMaterial();
                }
            }

            // hexagon is falling
            if (timeSum >= random)
            {
                isFalling = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;

                deltionTimeSum += Time.deltaTime;

                
                transform.localScale *= 0.997f;
                
                if(deltionTimeSum >= deletionTime)
                {
                    Destroy(gameObject);
                }
            }


        }
    }

    private void ToggleMaterial()
    {

        if (!warnMaterialIsActive)
        {
            warnMaterialIsActive = true;
            GetComponentInChildren<MeshRenderer>().material = redMaterial;
        }
        else
        {
            warnMaterialIsActive = false;
            GetComponentInChildren<MeshRenderer>().material = stdMaterial;
        }
    }
}
