using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerHasWon)
        {
            transform.Find("WinMenu").gameObject.SetActive(true);
        }

        if (GameManager.Instance.playerIsDead)
        {
            transform.Find("LooseMenu").gameObject.SetActive(true);
        }
    }


    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        GameManager.Instance.OnRestart();
    }
}
