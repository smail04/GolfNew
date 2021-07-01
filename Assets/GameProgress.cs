using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Clear();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log( PlayerPrefs.GetInt("LastFinishedLevel"));
        }
    }

    public void Save(int levelId)
    {
       PlayerPrefs.SetInt("LastFinishedLevel", levelId); 
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey("LastFinishedLevel");
    }

}
