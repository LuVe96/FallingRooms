using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanel : MonoBehaviour
{
    public Sprite pickedUpKey;
    public GameObject keyPrefab;

    public void UpdateKeys(int currentKeysCount)
    {
        for (int i = 0; i < currentKeysCount; i++)
        {
            if( i < transform.childCount - 1)
            {
                if (transform.GetChild(i) != null)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = pickedUpKey;
                }
            }


        }
    }

    public void SetKeys(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var k = Instantiate(keyPrefab, transform);
            k.transform.SetAsFirstSibling();
        }
    }

}
