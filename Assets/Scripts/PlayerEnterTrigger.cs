using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterTrigger : MonoBehaviour
{

    public bool triggered { get; private set; } = false;
    public bool triggerExit { get; private set; } = false;
    public bool triggeredForSentryGun { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            triggered = true;
            triggeredForSentryGun = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerExit = true;
            triggeredForSentryGun = false;
        }
    }
}
