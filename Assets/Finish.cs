using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Level level;
    private bool isActive = true;
    private LevelSwitcher levelSwitcher;
    private Menu menu;

    private void Start()
    {
        levelSwitcher = FindObjectOfType<LevelSwitcher>();
        menu = FindObjectOfType<Menu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            FinishLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() && isActive)
        {
            isActive = false;
            FinishLevel();            
        }
    }

    private void FinishLevel()
    {
        level.finished = true;
        menu.OpenEndLevelMenu(false);
    }

}
