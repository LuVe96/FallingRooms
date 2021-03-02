﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelPanelHandler : MonoBehaviour
{

    public Button btn;
    public Image[] stars;
    public Text nameText;

    public Sprite stdStar;
    public Sprite filledStar;
    public Color selectedColor;
    public Color disabledColor;

    public delegate void SelectButtonClicked(int index);
    public static SelectButtonClicked buttonClickDelegate;

    private int index;
    private bool selected;
    private bool opened;
    private Image btnImage;
    private Color stdColor;


    // Start is called before the first frame update
    void Start()
    {
        btnImage = btn.GetComponent<Image>();
        stdColor = btnImage.color;

    }

    private void btnClicked()
    {
        buttonClickDelegate(index);
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            btnImage.color = selectedColor;
        }
        else
        {
            btnImage.color = opened ? stdColor : disabledColor;
        }
    }

    public void Setup(int index, Level lvl, int starsCount, bool selected, bool opened)
    {
        this.index = index;
        this.selected = selected;
        this.opened = opened;
        nameText.text = lvl.Name;

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
