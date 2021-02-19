using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public TextAsset jsonFile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartLevel(int lvlNummer)
    {
        LevelsFile lvlsF = ReadLevelFile();
        Level curLvl = lvlsF.Levels[lvlNummer];
        DataSaver.saveData(curLvl, "currentLevel");
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
