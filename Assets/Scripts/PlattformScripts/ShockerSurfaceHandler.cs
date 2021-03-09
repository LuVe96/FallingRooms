using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockerSurfaceHandler : MonoBehaviour
{

    public GameObject middle;
    public GameObject border;
    public float switchTime = 1;

    private float timeSum = 0;
    private bool switchDings = false;


    private void Start()
    {
        middle.SetActive(true);
        border.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        timeSum += Time.deltaTime;

        if(timeSum >= switchTime)
        {
            timeSum = 0;
            middle.SetActive(!switchDings);
            border.SetActive(switchDings);
            switchDings = !switchDings;

            //foreach (var c in middle.GetComponentsInChildren<BoxCollider>())
            //{
            //    c.enabled = switchDings;
            //}
            //foreach (var p in middle.GetComponentsInChildren<ParticleSystem>())
            //{
            //    p.gameObject.SetActive(switchDings);
            //}

            //foreach (var c in border.GetComponentsInChildren<BoxCollider>())
            //{
            //    c.enabled = !switchDings;
            //}
            //foreach (var p in border.GetComponentsInChildren<ParticleSystem>())
            //{
            //    p.gameObject.SetActive(!switchDings);
            //}
        }

    }
}
