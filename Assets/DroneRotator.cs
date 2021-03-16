using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRotator : MonoBehaviour
{
    public float rotationSpeed = 1;

    void Update()
    {

        transform.Rotate(new Vector4(0, 0, 1), rotationSpeed * Time.deltaTime);
    }
}
