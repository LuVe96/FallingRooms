using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private PlayerInputActions playerInputActions;

    private MenuType currentMenuType;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.PauseMenu.started += PauseMenuOpen;

        retryButton.onClick.AddListener(RetryClicked);
        menuButton.onClick.AddListener(MenuClicked);
        nextButton.onClick.AddListener(NextClicked);

    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }


    private void PauseMenuOpen(InputAction.CallbackContext obj)
    {
       
        currentMenuType = MenuType.Pause;
        transform.Find("MenuScreen").gameObject.SetActive(true);
        setupPauseScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerHasWon & currentMenuType != MenuType.Win )
        {
            currentMenuType = MenuType.Win;
            transform.Find("MenuScreen").gameObject.SetActive(true);
            Score score = FindObjectOfType<PointsManager>().calculatePoints();
            setupWinScreen(score);
        }

        if (GameManager.Instance.playerIsDead & currentMenuType != MenuType.Loose)
        {
            currentMenuType = MenuType.Loose;
            transform.Find("MenuScreen").gameObject.SetActive(true);
            setupLooseScreen();
        }
    }

    private void setupWinScreen(Score score)
    {
        Time.timeScale = 0;
        timeText.text = "Time: " + score.scoreTime; 
        shocksText.text = "Shockes: " + score.scoreShocks;
        pointsText.text = "Points: " + score.points;
    
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled =true;
            if ( i < score.stars)
            {
                starImages[i].sprite = filledStar;
            }
            else
            {
                starImages[i].sprite = stdStar;
            }
        }


        retryButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<Text>().text = "Next Level";
    }

    private void setupLooseScreen()
    {
        Time.timeScale = 0;
        timeText.text = "";
        shocksText.text = "";
        pointsText.text = "You are a Looser";

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = false;
        }

        retryButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    private void setupPauseScreen()
    {
        Time.timeScale = 0;
        timeText.text = "";
        shocksText.text = "";
        pointsText.text = "Pause";

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = false;
        }

        retryButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<Text>().text = "Resume";
    }

    //public void Restart()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    //    GameManager.Instance.OnRestart();
    //}


    private void NextClicked()
    {
        Time.timeScale = 1;
        transform.Find("MenuScreen").gameObject.SetActive(false);
        currentMenuType = MenuType.None;
    }

    private void MenuClicked()
    {
        Time.timeScale = 1;
        transform.Find("MenuScreen").gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        currentMenuType = MenuType.None;
    }

    private void RetryClicked()
    {
        Time.timeScale = 1;
        transform.Find("MenuScreen").gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GameManager.Instance.OnRestart();
        currentMenuType = MenuType.None;
    }
}

enum MenuType
{
    Pause, Win, Loose, None
}