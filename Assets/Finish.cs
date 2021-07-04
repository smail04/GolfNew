using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    public Level level;
    private bool isActive = true;
    private Menu menu;
    private GameProgress progress;

    private void Start()
    {
        menu = FindObjectOfType<Menu>();
        progress = FindObjectOfType<GameProgress>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball && isActive)
        {
            isActive = false;
            ball.levelTimer.StopTimer();
            FinishLevel(ball.levelTimer.Value);            
        }
    }

    private void FinishLevel(float timeResult)
    {
        level.finished = true;
        if (PlayerPrefs.HasKey("BestTimeLevel" + level.id))
        {
            if (PlayerPrefs.GetFloat("BestTimeLevel" + level.id) > timeResult)
            {
                PlayerPrefs.SetFloat("BestTimeLevel" + level.id, timeResult);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTimeLevel" + level.id, timeResult);
        }
        progress.Save(level.id);
        menu.OpenEndLevelMenu(false, timeResult);        
    }

}
