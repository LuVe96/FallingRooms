using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelPanelHandler : MonoBehaviour
{

    public Button btn;
    public Image[] stars;
    public Text nameText;
    public Image bgImage;
    public Image lvlImage;
    public Sprite[] sprites;

    public Sprite stdStar;
    public Sprite filledStar;
    public Color selectedColor;
    public Color disabledColor;
    public Color stdColor;

    public delegate void SelectButtonClicked(int index);
    public static SelectButtonClicked buttonClickDelegate;

    private int index;
    private bool selected;
    private bool opened;

    private void btnClicked()
    {
        Debug.Log("btn Clicked");
        buttonClickDelegate(index);
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            bgImage.color = selectedColor;
        }
        else
        {
            bgImage.color = opened ? stdColor : disabledColor;
        }
    }

    public void Setup(int index, Level lvl, int starsCount, bool selected, bool opened)
    {
        this.index = index;
        this.selected = selected;
        this.opened = opened;
        nameText.text = lvl.Name;

        foreach (var img in sprites)
        {
            if(img.name == lvl.Image)
            {
                lvlImage.sprite = img;
            }
        }

        if(opened)
        {
            btn.onClick.AddListener(btnClicked);
        } else
        {
            btn.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if(i < starsCount)
            {
                stars[i].sprite = filledStar;
            } else
            {
                stars[i].sprite = stdStar;
            }
        }

    }

    public void setSelected(bool selected)
    {
        this.selected = selected;
    }
}
