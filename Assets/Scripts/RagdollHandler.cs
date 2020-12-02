using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{

    public Collider mainCollider;
    private Collider[] allColliders;
    public float standUpTime = 0.5f;
    private float standUpTimeSum = 0;
    public bool deactivated = false;

    public bool isRagdollActive { private set; get; } = false;

    // Start is called before the first frame update
    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        mainCollider = transform.parent.GetComponent<CapsuleCollider>();
        DoRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRagdollActive)
        {
            standUpTimeSum += Time.deltaTime;
            if (standUpTimeSum >= standUpTime)
            {
                DoRagdoll(false);
                standUpTimeSum = 0;
            }
        }



    }

    public void DoRagdoll(bool isRagdoll)
    {
        if(isRagdoll && deactivated)
        {
            return;
        }

        foreach (var col in allColliders)
        {
            col.enabled = isRagdoll;
        }
        mainCollider.enabled = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
        isRagdollActive = isRagdoll;
        

    }
}
