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
        float bestTimeInSeconds = 0;
        if (PlayerPrefs.HasKey("BestTimeLevel" + level.id))
        {
            bestTimeInSeconds = PlayerPrefs.GetFloat("BestTimeLevel" + level.id);
            int mins = Mathf.FloorToInt(bestTimeInSeconds / 60);
            float secs = bestTimeInSeconds - mins * 60;
            bestTime.text = "Best: " + mins.ToString() + ":" + string.Format("{0:00.0}", secs);
        }
        else
        {
            bestTime.text = "Best: --:--.-";
        }
        preview.sprite = level.preview;
        return this;
    }

}
