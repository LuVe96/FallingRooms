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

    public Sprite stdStar;
    public Sprite filledStar;
    public Color selectedColor;

    public delegate void SelectButtonClicked(int index);
    public static SelectButtonClicked buttonClickDelegate;

    private int index;
    private bool selected;
    private Image btnImage;
    private Color stdColor;


    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(btnClicked);
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
            btnImage.color = stdColor;
        }
    }

    public void Setup(int index, Level lvl, int starsCount, bool selected)
    {
        this.index = index;
        this.selected = selected;
        nameText.text = lvl.Name;

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
