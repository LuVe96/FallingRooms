using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject lvlSelector;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject tutorialMenu;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartLevelMenu()
    {
        lvlSelector.SetActive(true);
    }

    public void StartTutorialMenu()
    {
        tutorialMenu.SetActive(true);
    }

    public void OpenControlsMenu()
    {
        controlsMenu.SetActive(true);
    }

    public void CloseControlsMenu()
    {
        controlsMenu.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        creditsMenu.SetActive(false);
    }

}
