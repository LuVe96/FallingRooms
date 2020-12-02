using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanel : MonoBehaviour
{
    public Sprite pickedUpKey;

    public void UpdateKeys(int currentKeysCount)
    {
        for (int i = 0; i < currentKeysCount; i++)
        {
            if(transform.GetChild(i) != null)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = pickedUpKey;
            }

        }
    }

}
