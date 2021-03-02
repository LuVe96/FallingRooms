using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public int requiredeKeysCount = 3;
    public int currentKeysCount { get; private set; } = 0;
    public ParticleSystem shockParticle;
    public float shockDuration;

    private GameObject[] keys;
    private ArrowHandler arrowHandler;
    private bool isShocked = false;

    public float invincibilityTime = 3f;
    private float invincibilityTimeSum = 0f;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private float blinkTimeSum = 0;
    public Color blinkColor;
    private Color stdClolor;
    private bool InvincibilityModeisActive = false;


    // Start is called before the first frame update
    void Start()
    {
        arrowHandler = GetComponentInChildren<ArrowHandler>();
        stdClolor = skinnedMeshRenderer.materials[1].color;
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

        if (InvincibilityModeisActive)
        {
            blinkTimeSum += Time.deltaTime;
            if (blinkTimeSum >= 0.1f)
            {
                if (skinnedMeshRenderer.materials[1].color == stdClolor)
                {
                    skinnedMeshRenderer.materials[1].color = blinkColor;
                    skinnedMeshRenderer.enabled = false;
                }
                else
                {
                    skinnedMeshRenderer.materials[1].color = stdClolor;
                    skinnedMeshRenderer.enabled = true;

                }
                blinkTimeSum = 0;
            }

            invincibilityTimeSum += Time.deltaTime;
            if (invincibilityTimeSum >= invincibilityTime)
            {
                DoInvincibilityMode(false);
                invincibilityTimeSum = 0;
            }
        }
    }

    private void DoInvincibilityMode(bool activated)
    {

        isShocked = activated;
        InvincibilityModeisActive = activated;
        if (!activated)
        {
            skinnedMeshRenderer.materials[1].color = stdClolor;
            skinnedMeshRenderer.enabled = true;
            blinkTimeSum = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            currentKeysCount += 1;
            Destroy(other.gameObject);
            GameObject.Find("KeyPanel").GetComponent<KeyPanel>().UpdateKeys(currentKeysCount);
        }

        if(other.tag == "Shocker" && !isShocked)
        {
            
            StartCoroutine(doShock());
        }

        if (other.tag == "RagdollActivator")
        {
            GetComponentInChildren<RagdollHandler>().DoRagdoll(true);
        }
    }

    IEnumerator doShock()
    {
        shockParticle.gameObject.SetActive(true);
        GetComponent<PlayerMovement>().setShocked(true);
        isShocked = true;

        yield return new WaitForSeconds(shockDuration);

        shockParticle.gameObject.SetActive(false);
        GetComponent<PlayerMovement>().setShocked(false);
        isShocked = false;
        DoInvincibilityMode(true);
    }

    public void setRequredKeys(int keyCount)
    {
        requiredeKeysCount = keyCount;
        GameObject.Find("KeyPanel").GetComponent<KeyPanel>().SetKeys(keyCount);

    }
}
