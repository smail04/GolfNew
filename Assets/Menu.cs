using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject endLevelMenu;
    public GameObject inGameElements;
    public GameObject buttonContinue;
    public GameObject buttonNext;
    public LevelSwitcher levelSwitcher;
    
    void Start()
    {
        OpenMenu(); 
    }
    
    public void OpenMenu()
    {
        menu.SetActive(true);
        endLevelMenu.SetActive(false);
        inGameElements.SetActive(false);
        Time.timeScale = 0;
    }

    public void OpenEndLevelMenu(bool stopTime)
    {
        Pause(stopTime);
        buttonContinue.SetActive(false);
        buttonNext.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        Pause();
        buttonContinue.SetActive(true);
        buttonNext.SetActive(false);
    }

    public void Pause(bool stopTime = true)
    {
        menu.SetActive(false);
        endLevelMenu.SetActive(true);
        inGameElements.SetActive(false);
        Time.timeScale = (stopTime) ? 0 : 1;
    }

    public void OpenGameLayer() 
    {
        menu.SetActive(false);
        endLevelMenu.SetActive(false);
        inGameElements.SetActive(true);
        Time.timeScale = 1;
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
