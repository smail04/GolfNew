using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject endLevelMenu;
    public GameObject inGameElements;
    public GameObject settingsMenu;

    public Camera menuCamera;
    public Camera mainCamera;    
    
    public GameObject buttonContinue;
    public GameObject buttonNext;
    public Text timeResultText;
    public LevelSwitcher levelSwitcher;
    
    void Start()
    {
        OpenMenu(); 
    }
    
    public void OpenMenu()
    {
        menu.SetActive(true);
        settingsMenu.SetActive(false);
        endLevelMenu.SetActive(false);
        inGameElements.SetActive(false);
        menuCamera.enabled = true;
        mainCamera.enabled = false;
        Time.timeScale = 0;
    }

    public void Pause(bool stopTime = true)
    {
        menu.SetActive(false);
        settingsMenu.SetActive(false);
        endLevelMenu.SetActive(true);
        inGameElements.SetActive(false);
        menuCamera.enabled = false;
        mainCamera.enabled = true;
        Time.timeScale = (stopTime) ? 0 : 1;
    }

    public void OpenGameLayer()
    {
        menu.SetActive(false);
        settingsMenu.SetActive(false);
        endLevelMenu.SetActive(false);
        inGameElements.SetActive(true);
        menuCamera.enabled = false;
        mainCamera.enabled = true;
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        menu.SetActive(false);
        settingsMenu.SetActive(true);
        endLevelMenu.SetActive(false);
        inGameElements.SetActive(false);
        menuCamera.enabled = true;
        mainCamera.enabled = false;
        Time.timeScale = 0;
    }

    public void OpenEndLevelMenu(bool stopTime, float timeResult = 0)
    {
        Pause(stopTime);
        buttonContinue.SetActive(false);
        buttonNext.SetActive(true);
        timeResultText.gameObject.SetActive(true);
        timeResultText.text = "Your time\n" + string.Format("{0:0.00}", timeResult);
    }

    public void OpenPauseMenu()
    {
        Pause();
        buttonContinue.SetActive(true);
        buttonNext.SetActive(false);
        timeResultText.gameObject.SetActive(false);
    }
    
    public void PlayButton()
    {
        if (levelSwitcher.currentLevel == null)
        {
            OpenGameLayer();
            levelSwitcher.LoadNextLevel();
        }
        else if (levelSwitcher.currentLevel.finished)
        {
            OpenEndLevelMenu(false);
        }
        else
        {
            OpenGameLayer();
        }
        
    }

    public void NextLevelButton()
    {
        OpenGameLayer();
        levelSwitcher.LoadNextLevel();
    }

    public void RestartLevelButton()
    {
        OpenGameLayer();
        levelSwitcher.ReloadLevel();
    }

}
