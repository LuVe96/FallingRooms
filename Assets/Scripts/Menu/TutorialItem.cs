using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialItem : MonoBehaviour
{

    public Text txtField;
    public Image image;


    public void Setup(TutorialItemContent tutorialItemContent)
    {
        txtField.text = tutorialItemContent.text;
        image.sprite = tutorialItemContent.sprite;
    }
}

[System.Serializable]
public class TutorialItemContent
{
    public Sprite sprite;
    [TextArea]
    public string text;
}

