using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSpinner : BaseRotator
{
    public float rotationSpeed = 1;
    private int random;
    private int dir;


    private void Start()
    {
        random = Random.Range(0, 2);
    }
    // Update is called once per frame
    void Update()
    {
        dir = (direction == Direction.Right) ? 1 : -1;
        transform.Rotate(new Vector4(0, 1, 0), (rotationSpeed * dir) * Time.deltaTime);
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        //GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
}
