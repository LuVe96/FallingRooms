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

    public Text levelNameText;
    public Text timeText;
    public Text shocksText;
    public Text pointsText;

    public GameObject scorePanel;
    public Image mainImage;
    public Sprite looseImage;
    public Sprite pauseImage;

    public Button retryButton;
    public Button menuButton;
    public Button nextButton;

    private PlayerInputActions playerInputActions;

    private MenuType currentMenuType;
    private LevelScoreFile lvlScoreFile;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.PauseMenu.started += PauseMenuOpen;

        retryButton.onClick.AddListener(RetryClicked);
        menuButton.onClick.AddListener(MenuClicked);
        nextButton.onClick.AddListener(NextClicked);
        lvlScoreFile = DataSaver.loadData<LevelScoreFile>("levelScores");
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
        scorePanel.SetActive(true);
        mainImage.gameObject.SetActive(false);
        levelNameText.text = FindObjectOfType<LevelGenerator>().currentLevel.Name;
        timeText.text = "" + score.scoreTime; 
        shocksText.text = "" + score.scoreShocks;
        pointsText.text = "Score: " + score.points;

        updateScoreToFile(lvlScoreFile, score.stars);
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
        nextButton.GetComponentInChildren<Text>().text = "Next";
    }

    private void updateScoreToFile(LevelScoreFile lvlFile, int stars)
    {
        if(lvlFile == null)
        {
            List<LevelScore> scores = new List<LevelScore>();
            scores.Add(new LevelScore(stars));
            LevelScoreFile lSdata = new LevelScoreFile( scores);
            DataSaver.saveData(lSdata, "levelScores");
        } else
        {
            List<LevelScore> scores = lvlFile.levelScores;
            int i = FindObjectOfType<LevelGenerator>().currentLevel.Index;
            if ( i < scores.Count)
            {
                if( scores[i].stars < stars)
                {
                    scores[i].stars = stars;
                }
            }else
            {
                scores.Add(new LevelScore(stars));
            }
            LevelScoreFile lSdata = new LevelScoreFile(scores);
            DataSaver.saveData(lSdata, "levelScores");
        }
    }

    private void setupLooseScreen()
    {
        Time.timeScale = 0;
        scorePanel.SetActive(false);
        mainImage.gameObject.SetActive(true);
        mainImage.sprite = looseImage;
        levelNameText.text = "You are Dead";
        timeText.text = "";
        shocksText.text = "";
        pointsText.text = "";

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
        scorePanel.SetActive(false);
        mainImage.gameObject.SetActive(true);
        mainImage.sprite = pauseImage;
        levelNameText.text = "Pause";
        timeText.text = "";
        shocksText.text = "";
        pointsText.text = "";

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = false;
        }

        retryButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<Text>().text = "Resume";
    }


    private void NextClicked()
    {
        if(currentMenuType == MenuType.Win)
        {
            var lvlFile = DataSaver.loadData<LevelsFile>("allLevels");
            int newIndex = FindObjectOfType<LevelGenerator>().currentLevel.Index + 1;
            if (lvlFile.Levels.Length > newIndex)
            {
                transform.Find("MenuScreen").gameObject.SetActive(false);
                var newCurLvl = new CurrentLevel(newIndex, lvlFile.Levels[newIndex]);
                DataSaver.saveData(newCurLvl, "currentLevel");
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                GameManager.Instance.OnRestart();
            }
            else
            {
                nextButton.gameObject.SetActive(false);
            }
        }else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        currentMenuType = MenuType.None;
        Time.timeScale = 1;

    }

    private void MenuClicked()
    {
        currentMenuType = MenuType.None;
        Time.timeScale = 1;
        transform.Find("MenuScreen").gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    private void RetryClicked()
    {
        currentMenuType = MenuType.None;
        Time.timeScale = 1;
        transform.Find("MenuScreen").gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GameManager.Instance.OnRestart();

    }
}

enum MenuType
{
    Pause, Win, Loose, None
}