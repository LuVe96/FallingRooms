using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GeneratedPlatform[] generatedPlatforms;
    private Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = DataSaver.loadData<Level>("currentLevel");
        Debug.Log("Level:");
        Debug.Log(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

[System.Serializable]
public class GeneratedPlatform
{
    public string key;
    public GameObject plattform;
}