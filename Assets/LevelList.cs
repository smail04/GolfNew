using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public List<LevelListItem> items = new List<LevelListItem>();
    public LevelSwitcher levelSwitcher;
    public GameObject listItemPrefab;

    private void Start()
    {
        Clear();
        ShowList();
    }

    private void ShowList()
    {
        foreach (Level level in levelSwitcher.levels)
        {
            GameObject newItemObject = Instantiate(listItemPrefab, transform);
            items.Add(newItemObject.GetComponent<LevelListItem>().Initialize(level));
        }
    }

    private void Clear()
    {
        foreach (LevelListItem item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }

}
