using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public int requiredeKeysCount = 3;
    public int currentKeysCount { get; private set; } = 0;

    private GameObject[] keys;
    private ArrowHandler arrowHandler; 

    // Start is called before the first frame update
    void Start()
    {
        arrowHandler = GetComponentInChildren<ArrowHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeys();
    }

    private void CheckForKeys()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");

        GameObject nearestKey = null;
        float? nearestDistance = null;

        foreach (var key in keys)
        {
            float distance = new Vector3(transform.position.x - key.transform.position.x, 0,
                transform.position.z - key.transform.position.z).magnitude;
            if (!nearestDistance.HasValue || distance <= nearestDistance)
            {
                nearestKey = key;
                nearestDistance = distance;
            }
        }

        //if (currentKeysCount < requiredeKeysCount && nearestKey != null)
        //{
        //    arrowHandler.setKeyGoal(nearestKey.transform);
        //}
        //else
        //{
        //    arrowHandler.setKeyGoal(null);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            currentKeysCount += 1;
            Destroy(other.gameObject);
            GameObject.Find("KeyPanel").GetComponent<KeyPanel>().UpdateKeys(currentKeysCount);
        }

        if (other.tag == "RagdollActivator")
        {
            GetComponentInChildren<RagdollHandler>().DoRagdoll(true);
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.tag == "RagdollActivator")
    //    {
    //        GetComponentInChildren<RagdollHandler>().DoRagdoll(true);
    //    }
    //}
}
