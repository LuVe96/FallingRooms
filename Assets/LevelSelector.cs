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
    public GameObject levelUiPrefab;

    private Level currentSelecteLevel;
    private LevelsFile lvlsF;
    private LevelScoreFile lvlScoreFile;
    private List<UiLevelPanelHandler> uiLevelHandlers;

    private 
    // Start is called before the first frame update
    void Start()
    {
        lvlsF = ReadLevelFile();
        //lvlScoreFile = DataSaver.loadData<LevelScoreFile>("levelScores");
        uiLevelHandlers = new List<UiLevelPanelHandler>();

        UiLevelPanelHandler.buttonClickDelegate += AnyLevelSelcted;
        startButton.onClick.AddListener(startButtonClicked);
        backButton.onClick.AddListener(backButtonClicked);


        for (int i = 0; i < lvlsF.Levels.Length; i++)
        {
            Level lvl = lvlsF.Levels[i];
            var l = Instantiate(levelUiPrefab, levelsHolder);
            l.transform.SetAsLastSibling();
            var lH = l.GetComponent<UiLevelPanelHandler>();
            lH.Setup(i,lvl, 1, (i == 0));
            uiLevelHandlers.Add(lH);
        }
    }

    private void AnyLevelSelcted(int index)
    {
        for (int i = 0; i < lvlsF.Levels.Length; i++)
        {

            uiLevelHandlers[i].setSelected(false);

            if( i == index)
            {
                uiLevelHandlers[i].setSelected(true);
                currentSelecteLevel = lvlsF.Levels[i];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private LevelsFile ReadLevelFile()
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
    public int RefTime;
    public string LevelString;
}

[Serializable]
public class LevelScoreFile
{
    public LevelScore[] levelScores;
}

[Serializable]
public class LevelScore
{
    public string Name;
    public int stars;
}

