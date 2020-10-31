using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{

    public Transform goalObject;
    void Update()
    {
        transform.LookAt(new Vector3(goalObject.transform.position.x, 3, goalObject.transform.position.z));

    }
}
