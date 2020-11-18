using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSpinner : MonoBehaviour
{
    public float rotationSpeed = 1;
    private int random;

    private void Start()
    {
        random = Random.Range(0, 2);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(random);
        transform.Rotate(new Vector4(0, 1, 0), ((random == 0) ?  rotationSpeed : -rotationSpeed) * Time.deltaTime);
    }
}
