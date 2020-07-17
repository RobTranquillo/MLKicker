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

    void Start()
    {
        availableItems[0].Display();
        availableItems[1].Display();
    }
}
