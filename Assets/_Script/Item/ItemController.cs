using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Any item effect must implement this class
/// </summary>
public interface ItemEffect
{
    void Setup(object settings);
}


/// <summary>
/// Contains the logic, when, where, which item appears with which properties.
/// </summary>
public class ItemController : MonoBehaviour
{
    public GameObject ball;
    public Item[] availableItems;
    
    [Tooltip("Wie langen soll die Items erscheinen.")]
    [Range(1,60)]
    public float itemsVisible = 5f;
    [Tooltip("Wieviel Zeit soll zwischen dem erscheinen der Items sein.")]
    [Range(1,60)]
    public float intervallWithoutItems = 3f;

    void Start()
    {
        Invoke("SpawnItemsEndless", intervallWithoutItems);
    }

    private void SpawnItemsEndless()
    {
        foreach (var item in availableItems)
            item.Display();
        
        Invoke("DestroyItems", itemsVisible);
    }

    private void DestroyItems()
    {
        foreach (var item in availableItems)
            item.DestroyItems();
        Invoke("SpawnItemsEndless", intervallWithoutItems);
    }
}
