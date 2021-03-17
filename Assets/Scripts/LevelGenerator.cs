using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GeneratedPlatform[] generatedPlatforms;
    public Transform player;
    public Transform parent;
    [HideInInspector]
    public CurrentLevel currentLevel;
    public AudioClip[] audioclips;

    private Vector3 currPos;
    private float delta_z = 4.3326f; // 4.3326f;
    private float delta_x = 7.50f; // 7.50f;

    private bool evenLine = true;

    // Start is called before the first frame update
    private void Start()
    {
        currPos = new Vector3(0, 0, 0);
        currentLevel = DataSaver.loadData<CurrentLevel>("currentLevel");

        FindObjectOfType<PointsManager>().refTime = currentLevel.RefTime;
        FindObjectOfType<PlayerHandler>().setRequredKeys(currentLevel.ReqKeys);
        createLevel();

        GetComponent<AudioSource>().clip = audioclips[currentLevel.Song - 1];
        GetComponent<AudioSource>().Play();
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
                    pf.transform.position = currPos;
                    pf.transform.rotation = Quaternion.Euler(0,30,0) ;

                    if (item.Substring(0, 3) == PlatformKey.STT.ToString())
                    {
                        player.transform.position = currPos; // new Vector3(-currPos.z, 1, currPos.x); 
                    }

                    if (item.Length >= 4)
                    {
                        switch (item.Substring(3, 1))
                        {
                            case "1": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 1f, 0); break;
                            case "2": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 2f, 0); break;
                            case "3": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 3f, 0); break;
                            case "4": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 4f, 0); break;
                            case "5": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 5f, 0); break;
                            case "0": pf.transform.rotation = Quaternion.Euler(0, pf.transform.rotation.eulerAngles.y + 60 * 0f, 0); break;
                            case "l": setDirectionToPlatform(Direction.Left, pf); break;
                            case "r": setDirectionToPlatform(Direction.Right, pf); break;
                            default:
                                break;
                        }
                    }


                    currPos += new Vector3(delta_x, 0, 0);
                    
                }
            }


            if (item.Length >= 4)
            {
                switch (item.Substring(0, 4))
                {
                    case "brea":
                        //evenLine = !evenLine;
                        //if (evenLine)
                        //{
                        //    currPos = new Vector3(0, 0, currPos.z - delta_z);
                        //} else
                        //{
                        //    currPos = new Vector3(-delta_x/2, 0, currPos.z - delta_z);
                        //}
                        currPos = new Vector3(0, 0, currPos.z - delta_z);
                        break;
                    case "EMPT": currPos += new Vector3(delta_x, 0, 0); break;
                    case "shif":
                        currPos += new Vector3(delta_x, 0, 0); break;
                    default:
                        break;
                }
            }
        }

    }

    private void setDirectionToPlatform(Direction dir, GameObject pf)
    {
        try
        {
            pf.GetComponent<BaseRotator>().setRotation(dir);
        }
        catch (Exception e)
        { Debug.LogWarning(e);}

        try
        {
            foreach (var item in pf.GetComponentsInChildren<BaseRotator>())
            {
                item.setRotation(dir);
            }
        }
        catch (Exception e)
        { Debug.LogWarning(e); }
    }

    private void shiftLine()
    {
        evenLine = !evenLine;
        if (evenLine)
        {
            currPos += new Vector3(0, 0, delta_z);
        }
        else
        {
            currPos += new Vector3(0, 0, -delta_z);
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