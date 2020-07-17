using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for items
///  - Editor editing
///  - name
///  - kind (e.g. team related unbalanced or balanced effect)
///  - appearance
///  - effect
///   - effect settings
///  
/// Item Storage
///  - List of any available item
///  - availability
///  
/// Item Controller
///   Contains the logic, when, where, which item appears with which properties.
///   - spawn time
///   - manager about which item is available
///   - spawn point
///   - endurance
///   - frequency/probability (not a value, its a calc based on values of the manger)
///   - team advantage
///
/// Manager of Items and Points
///     which item has every team earned
///  
/// </summary>


public enum ItemType
{
    Balanced,
    Unbalanced
}

[CreateAssetMenu(fileName = "New item", menuName = "KickerItem/Create new item")]
public class Item : ScriptableObject
{
    public string description;
    public ItemType type;
    public GameObject appearance;

    /// <summary>
    /// Make the item visible on the field
    /// </summary>
    public void Display()
    {
        GameObject go = Instantiate(appearance);
    }
}
