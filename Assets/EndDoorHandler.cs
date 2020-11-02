using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorHandler : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerHandler>().currentKeysCount 
            >= GameObject.Find("Player").GetComponent<PlayerHandler>().requiredeKeysCount)
        {
            Destroy(gameObject);
        }
    }


}
