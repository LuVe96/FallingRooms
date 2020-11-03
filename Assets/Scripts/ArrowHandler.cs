using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{

    public Transform goalObject;
    private Transform keyGoal;

    public Material endGoalMaterial;
    public Material keyGoalMaterial;

    void Update()
    {

        if (keyGoal == null)
        {
            transform.LookAt(new Vector3(goalObject.transform.position.x, 3, goalObject.transform.position.z));
            GetComponent<MeshRenderer>().material = endGoalMaterial;
        }
        else
        {
            Debug.Log("4");
            transform.LookAt(new Vector3(keyGoal.position.x, 3, keyGoal.position.z));
            GetComponent<MeshRenderer>().material = keyGoalMaterial;
        }

    }

    public void setKeyGoal(Transform key)
    {
        keyGoal = key;
    }
}
