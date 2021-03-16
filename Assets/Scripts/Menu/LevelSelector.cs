using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public TextAsset jsonFile;

    public Transform levelsHolder;
    public Button backButton;
    public Button startButton;
    public Button pageBackButton;
    public Button pageNextButton;
    public GameObject levelUiPrefab;
    public int itemsPerPage = 8;

    private Level currentSelecteLevel;
    private int currentSelectedLevelIndex = 0;
    private int nextOpenedLevelIndex = 0;
    private LevelsFile lvlsF;
    private LevelScoreFile lvlScoreFile;
    private List<UiLevelPanelHandler> uiLevelHandlers;
    private int currentPage = 0;
    private int maxPages;

    // Start is called before the first frame update
    void Start()
    {
        ///
        //ResetSaves();
        ///
        lvlsF = ReadLevelFile();
        lvlScoreFile = DataSaver.loadData<LevelScoreFile>("levelScores");
        uiLevelHandlers = new List<UiLevelPanelHandler>();
        maxPages = (int)(lvlsF.Levels.Length-1)/8 +1;
        currentSelecteLevel = lvlsF.Levels[0];
        currentPage = 0;

        UiLevelPanelHandler.buttonClickDelegate += AnyLevelSelcted;
        startButton.onClick.AddListener(startButtonClicked);
        backButton.onClick.AddListener(backButtonClicked);
        pageNextButton.onClick.AddListener(pageNextButtonClicked);
        pageBackButton.onClick.AddListener(pageBackButtonClicked);

        updateNavigationButtons();
        createLevelUi(currentPage * itemsPerPage, currentPage * itemsPerPage + itemsPerPage);
    }

    public void ResetSaves()
    {
        List<LevelScore> scores = new List<LevelScore>();
        LevelScoreFile lSdata = new LevelScoreFile(scores);
        DataSaver.saveData(lSdata, "levelScores");
    }

    private void AnyLevelSelcted(int index)
    {
        for (int i = 0; i < lvlsF.Levels.Length; i++)
        {
            if(i < uiLevelHandlers.Count) uiLevelHandlers[i].setSelected(false);
            if( i == index)
            {
                uiLevelHandlers[i -(currentPage * itemsPerPage)].setSelected(true);
                currentSelectedLevelIndex = i;
                currentSelecteLevel = new CurrentLevel(i, lvlsF.Levels[i]);
            }
        }
    }

    private void pageBackButtonClicked()
    {
        currentPage -= 1;
        createLevelUi(currentPage * itemsPerPage, currentPage * itemsPerPage + itemsPerPage);
        updateNavigationButtons();
    }

    private void pageNextButtonClicked()
    {
        currentPage += 1;
        createLevelUi(currentPage * itemsPerPage, currentPage * itemsPerPage + itemsPerPage);
        updateNavigationButtons();
    }

    private void backButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void startButtonClicked()
    {
        DataSaver.saveData(currentSelecteLevel, "currentLevel");
        SceneManager.LoadScene(1);
    }

    private void updateNavigationButtons()
    {
        pageBackButton.gameObject.SetActive(currentPage > 0);
        pageNextButton.gameObject.SetActive(currentPage < maxPages-1);
    }

    private void createLevelUi(int statIndex, int endIndex)
    {
        for (int i = 0; i < levelsHolder.transform.childCount; i++)
        {
            var go = levelsHolder.transform.GetChild(i).gameObject;
            if (go.name != "LNB") Destroy(go);
        }
        uiLevelHandlers.Clear();

        if (statIndex < lvlScoreFile.levelScores.Count)
        {
            nextOpenedLevelIndex = statIndex;
        }

        for (int i = statIndex; i < endIndex; i++)
        {
            if( i < lvlsF.Levels.Length)
            {
                Level lvl = lvlsF.Levels[i];
                int stars = 0;
                if(lvlScoreFile != null)
                {
                    if (i < lvlScoreFile.levelScores.Count)
                    {
                        stars = lvlScoreFile.levelScores[i].stars;
                    }
                }
                var l = Instantiate(levelUiPrefab, levelsHolder);
                l.transform.SetAsLastSibling();
                var lH = l.GetComponent<UiLevelPanelHandler>();
                lH.Setup(i, lvl, stars, (i == currentSelectedLevelIndex), (i == nextOpenedLevelIndex));
                uiLevelHandlers.Add(lH);

                if (lvlScoreFile != null && i < lvlScoreFile.levelScores.Count)
                {
                    if (i < lvlScoreFile.levelScores.Count)
                    {
                        nextOpenedLevelIndex = i + 1;
                    }
                }
            }
        }
    }

    public LevelsFile ReadLevelFile()
    {
        //Load saved Json
        byte[] jsonByte = null;
        try
        {
            jsonByte = jsonFile.bytes;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
        }

        //Convert to json string
        string jsonData = Encoding.ASCII.GetString(jsonByte);

        //Convert to Object
        object resultValue = JsonUtility.FromJson<LevelsFile>(jsonData);
        LevelsFile lvls = (LevelsFile)Convert.ChangeType(resultValue, typeof(LevelsFile));

        DataSaver.saveData(lvls, "allLevels");
        return lvls;
    }
}

[Serializable]
public class LevelsFile
{
    public Level[] Levels;

}

[Serializable]
public class Level
{
    public string Name;
    public string Image;
    public int RefTime;
    public int ReqKeys;
    public string LevelString;
}

[Serializable]
public class CurrentLevel : Level
{
    public int Index;

    public CurrentLevel(int index, Level lvl)
    {
        Index = index;
        Name = lvl.Name;
        Image = lvl.Image;
        RefTime = lvl.RefTime;
        ReqKeys = lvl.ReqKeys;
        LevelString = lvl.LevelString;
    }
}

[Serializable]
public class LevelScoreFile
{
    public List<LevelScore> levelScores;
    public LevelScoreFile(List<LevelScore> levelScores)
    {
        this.levelScores = levelScores;
    }
}

[Serializable]
public class LevelScore
{
    //public string Name;
    public int stars;

    public LevelScore(/*string name,*/ int stars)
    {
        //Name = name;
        this.stars = stars;
    }
}

