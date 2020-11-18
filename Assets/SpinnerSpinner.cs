using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSpinner : MonoBehaviour
{
    public float rotationSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector4(0, 1, 0), rotationSpeed * Time.deltaTime);
    }
}
