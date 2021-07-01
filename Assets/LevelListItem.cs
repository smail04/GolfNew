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

    [SerializeField] private Image selfImage;
    [SerializeField] private Button selfButton;

    private bool isActive;

    public bool IsActive
    {
        get { return isActive; }
        set {
            isActive = value;
            selfImage.enabled = isActive;
            selfButton.enabled = isActive;
        }
    }

    public void Start()
    {
        IsActive = level.id <= PlayerPrefs.GetInt("LastFinishedLevel")+1;
    }

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
