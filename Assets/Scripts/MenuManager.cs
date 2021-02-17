using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PointsManager;

public class MenuManager : MonoBehaviour
{

    public Image[] starImages;
    public Sprite stdStar;
    public Sprite filledStar;

    public Text timeText;
    public Text shocksText;
    public Text pointsText;

    public Button retryButton;
    public Button menuButton;
    public Button nextButton;

    private MenuType currentMenuType;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(RetryClicked);
        menuButton.onClick.AddListener(MenuClicked);
        nextButton.onClick.AddListener(NextClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerHasWon & currentMenuType != MenuType.Win )
        {
            currentMenuType = MenuType.Win;
            transform.Find("WinScreen").gameObject.SetActive(true);
            Score score = FindObjectOfType<PointsManager>().calculatePoints();
            setupWinScreen(score);
        }

        if (GameManager.Instance.playerIsDead)
        {
            transform.Find("LooseMenu").gameObject.SetActive(true);
        }
    }

    private void setupWinScreen(Score score)
    {
        timeText.text = "Time: " + score.scoreTime; 
        shocksText.text = "Shockes: " + score.scoreShocks;
        pointsText.text = "Points: " + score.points;
    
        for (int i = 0; i < starImages.Length; i++)
        {
            if( i < score.stars)
            {
                starImages[i].sprite = filledStar;
            }
            else
            {
                starImages[i].sprite = stdStar;
            }
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        GameManager.Instance.OnRestart();
    }


    private void NextClicked()
    {
        throw new NotImplementedException();
    }

    private void MenuClicked()
    {
        throw new NotImplementedException();
    }

    private void RetryClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        GameManager.Instance.OnRestart();
    }
}

enum MenuType
{
    Pause, Win, Loose
}