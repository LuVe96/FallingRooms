using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{

    public Collider mainCollider;
    private Collider[] allColliders;
    public float standUpTime = 0.5f;
    private float standUpTimeSum = 0;
    public float invincibilityTime = 3f;
    private float invincibilityTimeSum = 0f;

    public bool deactivated = false;

    public bool isRagdollActive { private set; get; } = false;
    public Rigidbody headRigidbody;
    public Transform playerTransform;
    public Transform rootTransform;
    private Vector3 postionBevoreRagdoll;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Animator animator;
    private float blinkTimeSum = 0;
    public Color blinkColor;
    private Color stdClolor;
    private bool InvincibilityModeisActive = false;


    // Start is called before the first frame update
    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        mainCollider = transform.parent.GetComponent<CapsuleCollider>();
        DoRagdoll(false);
        stdClolor = skinnedMeshRenderer.materials[1].color;
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
            if (standUpTimeSum >= standUpTime - 0.1f)
            {
                //var dif = postionBevoreRagdoll - rootTransform.position;
                //Debug.Log("SetPlayerPos: " + dif);
                //playerTransform.position += new Vector3(dif.x, 0, dif.z);

                Vector3 animatedToRagdolled = /*ragdolledHipPosition -*/ animator.rootPosition;
                //Vector3 newRootPosition = transform.position + animatedToRagdolled;

                playerTransform.position += new Vector3(animatedToRagdolled.x, 0, animatedToRagdolled.z);

            }
            if (standUpTimeSum >= standUpTime - 0.6f)
            {

                headRigidbody.AddForce(new Vector3(0, 1, 0) * 800);
                DoInvincibilityMode(true);

            }
        }

        if (InvincibilityModeisActive)
        {
            blinkTimeSum += Time.deltaTime;
            if (blinkTimeSum >= 0.3f)
            {
                if (skinnedMeshRenderer.materials[1].color == stdClolor)
                {
                    skinnedMeshRenderer.materials[1].color = blinkColor;
                    MaterialExtensions.ToFadeMode(skinnedMeshRenderer.materials[1]);
                }
                else
                {
                    skinnedMeshRenderer.materials[1].color = stdClolor;
                    MaterialExtensions.ToOpaqueMode(skinnedMeshRenderer.materials[1]);

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
        //mainCollider.enabled = !activated;
        //foreach (var col in allColliders)
        //{
        //    col.enabled = !activated;
        //}

        InvincibilityModeisActive = activated;
        if(!activated)
        {
            skinnedMeshRenderer.materials[1].color = stdClolor;
            MaterialExtensions.ToOpaqueMode(skinnedMeshRenderer.materials[1]);
            blinkTimeSum = 0;
        }


    }

    public void DoRagdoll(bool isRagdoll)
    {
        if(isRagdoll && deactivated)
        {
            return;
        }

        if (isRagdoll  && InvincibilityModeisActive) return;

        //if (isRagdoll)
        //{
        //    ragdolledHipPosition = animator.rootPosition;

        //}
       
        foreach (var col in allColliders)
        {
            col.enabled = isRagdoll;
        }
        SetKinematic(!isRagdoll);
        mainCollider.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
        isRagdollActive = isRagdoll;
        

    }

    void SetKinematic(bool newValue)
    {
        //Get an array of components that are of type Rigidbody
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

}


public static class MaterialExtensions
{
    public static void ToOpaqueMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    public static void ToFadeMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}

