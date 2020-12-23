using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float rotationSpeed = 1;
    private int random;
    private GameObject player = null;

    private void Start()
    {
        random = Random.Range(0, 2);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector4(0, 1, 0), ((random == 0) ? rotationSpeed : -rotationSpeed) * Time.deltaTime);

        if (player != null)
        {
            player.transform.Find("CameraHolder").rotation = statRot;
        }
    }

    Quaternion statRot;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(transform);
            statRot = other.transform.Find("CameraHolder").rotation;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(GameObject.Find("World").transform);
            player = null;
        }
    }
}
