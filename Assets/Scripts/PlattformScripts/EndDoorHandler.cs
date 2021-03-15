using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlatformHandler;

public class EndDoorHandler : MonoBehaviour
{

    private Color stdColor;
    public Color openingFailedColor;
    public LightContainer lightMeshRenderer;

    public Material light_geen;
    public Material light_red;
    public Material light_std;


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
            if (!opening) {
                Material[] mats = lightMeshRenderer.renderer.materials;
                mats[lightMeshRenderer.index] = light_red;
                lightMeshRenderer.renderer.materials = mats;
            }
            else  if (blinkTimeSum >= 0.1f)
            {
                blinkTimeSum = 0;
                Material[] mats = lightMeshRenderer.renderer.materials;
                mats[lightMeshRenderer.index] = light_std;
                lightMeshRenderer.renderer.materials = mats;
                if (lightMeshRenderer.renderer.materials[lightMeshRenderer.index].name == light_std.name)
                {
                    //meshRenderer.material.color = openDoorColor;
                    Material[] mats1 = lightMeshRenderer.renderer.materials;
                    mats1[lightMeshRenderer.index] = light_geen;
                    lightMeshRenderer.renderer.materials = mats1;
                }
                else
                {
                    Material[] mats1 = lightMeshRenderer.renderer.materials;
                    mats1[lightMeshRenderer.index] = light_std;
                    lightMeshRenderer.renderer.materials = mats1;
                }

            }
            yield return null;
        }

        if(opening) Destroy(gameObject);

        yield return new WaitForSeconds(5);
        Material[] mats2 = lightMeshRenderer.renderer.materials;
        mats2[lightMeshRenderer.index] = light_std;
        lightMeshRenderer.renderer.materials = mats2;

    }

}
