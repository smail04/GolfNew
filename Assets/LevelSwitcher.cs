using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{
    public Level[] levels = new Level[100];
    public Level currentLevel;
    [SerializeField] private Ball player;    
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        int id = 0;
        foreach (var level in levels)
        {
            level.id = id++;
            HideLevel(level);
        }
        Load(levels[0], false); 
    }

    public void LoadNextLevel()
    {
        try
        {
            var nextLevel = levels[currentLevel.id + 1];
            if (nextLevel)
            {
                Load(nextLevel);
            }
            else
            {
                Load(levels[0]);
            }
        }
        catch (IndexOutOfRangeException e)
        {
            Load(levels[0]);
        }        
    }

    public void ReloadLevel()
    {
        Load(levels[currentLevel.id], true);
    }

    public void Load(Level level, bool MovePlayerToStartPoint = true)
    {
        DestroyCurrentLevel();
        GameObject newLevel = Instantiate(level.gameObject, Vector3.zero, Quaternion.identity);
        newLevel.SetActive(true);        
        currentLevel = newLevel.GetComponent<Level>();
        if (MovePlayerToStartPoint)
            player.MoveToStart();
    }

    private void DestroyCurrentLevel()
    {
        if (currentLevel)
            Destroy(currentLevel.gameObject);   
    }

    private void HideLevel(Level level)
    {
        if (level)
            level.gameObject.SetActive(false);
    }

    public void StartFadeIn()
    {
        StartCoroutine(nameof(FadeIn));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.enabled = true;
        for (int i = 0; i < 100; i++)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 100; i > 0; i--)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        fadeImage.enabled = false;
    }

}
