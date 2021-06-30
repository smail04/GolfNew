using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListItem : MonoBehaviour
{
    public Level level;
    public Menu menu;
    public Text levelName;
    public Text bestTime;
    public Image preview;
    public void Select()
    {
        menu = FindObjectOfType<Menu>();
        if (menu)
        {
            menu.OpenGameLayer();
            menu.levelSwitcher.Load(level, true);
            menu.CloseSelectLevelMenu();
        }
    }

    public LevelListItem Initialize(Level level)
    {
        this.level = level;
        levelName.text = "Level " + level.id;
        bestTime.text = "Best: 00:00.0";
        preview.sprite = level.preview;
        return this;
    }

}
