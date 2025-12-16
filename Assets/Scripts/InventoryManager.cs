using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<int> InventoryItems = new List<int>{};
    public List<int> StartingItems = new List<int>{3, 4};
    void Start()
    {
        AddStartingItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddStartingItems()
    {
        for (int i = 0; i < StartingItems.Count; i++)
        {
            InventoryItems.Add(StartingItems[i]);
        }
    }
}
