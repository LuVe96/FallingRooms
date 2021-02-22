using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GeneratedPlatform[] generatedPlatforms;
    public Transform player;
    public Transform parent;
    private Level currentLevel;

    private Vector3 currPos;
    private float delta_x = 8.6608f;
    private float delta_z = 7.4978f;

    private bool evenLine = true;

    // Start is called before the first frame update
    void Start()
    {
        currPos = new Vector3(0, 0, 0);
         currentLevel = DataSaver.loadData<Level>("currentLevel");
        createLevel();
    }

    private void createLevel()
    {
        string[] platformStrings = currentLevel.LevelString.Split(',');

        foreach (var item in platformStrings)
        {

            foreach (var platform in generatedPlatforms)
            {
                if(item.Substring(0,3) == platform.key.ToString())
                {
                    GameObject pf = Instantiate(platform.plattform, parent);
                    platform.plattform.transform.position = currPos;
                    platform.plattform.transform.rotation = Quaternion.Euler(-90,0,0) ;

                    if (item.Substring(0, 3) == PlatformKey.STT.ToString())
                    {
                        player.transform.position = currPos; // new Vector3(-currPos.z, 1, currPos.x); 
                    }

                    switch (item.Substring(3, 1))
                    {
                        case "0": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 0f, 0); break;
                        case "1": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 1f, 0) ; break;
                        case "2": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 2f, 0); break;
                        case "3": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 3f, 0); break;
                        case "4": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 4f, 0); break;
                        case "5": platform.plattform.transform.rotation = Quaternion.Euler(-90, 60 * 5f, 0); break;
                        default:
                            break;
                    }

                    currPos += new Vector3(delta_x, 0, 0);
                    
                }
            }

            switch (item)
            {
                case "break":
                    evenLine = !evenLine;
                    if (evenLine)
                    {
                        currPos = new Vector3(0, 0, currPos.z - delta_z);
                    } else
                    {
                        currPos = new Vector3(-delta_x/2, 0, currPos.z - delta_z);
                    }
                    break;
                case "EMPT": currPos += new Vector3(delta_x, 0, 0); break;
                case "shift":
                    break;
                case "end":
                    break;
                default:
                    break;
            }

        }

    }

}

[System.Serializable]
public class GeneratedPlatform
{
    public PlatformKey key;
    public GameObject plattform;
}

    public enum PlatformKey
    {
        NOR, STT, GOL, KEY, BRD, NUT, ROT, BND, SHT, SPI, LSH, LSP, GUN, SHO
    }