using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorHandler : MonoBehaviour
{

    private Color stdColor;
    public Color openingFailedColor;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        stdColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameObject.Find("Player").GetComponent<PlayerHandler>().currentKeysCount 
        //    >= GameObject.Find("Player").GetComponent<PlayerHandler>().requiredeKeysCount)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (GameObject.Find("Player").GetComponent<PlayerHandler>().currentKeysCount
           >= GameObject.Find("Player").GetComponent<PlayerHandler>().requiredeKeysCount)
            {
                StartCoroutine(HandleDoor(true));
            }
            else
            {
                StartCoroutine(HandleDoor(false));
            }

        }
    }
    
    IEnumerator HandleDoor(bool opening)
    {
        float timeSum = 0;
        float blinkTimeSum = 0;

        if (opening)
        {
            GetComponent<Collider>().enabled = false;
        }
        while (timeSum < 1)
        {
            timeSum += Time.deltaTime;

            blinkTimeSum += Time.deltaTime;
            if (!opening) meshRenderer.material.color = openingFailedColor;
            else  if (blinkTimeSum >= 0.1f)
            {
                blinkTimeSum = 0;
                meshRenderer.material.color = stdColor;
                if (meshRenderer.enabled == true)
                {
                    //meshRenderer.material.color = openDoorColor;
                    meshRenderer.enabled = false;
                }
                else
                {
                    meshRenderer.enabled = true;
                }

            }
            yield return null;
        }

        if(opening) Destroy(gameObject);

    }

}
