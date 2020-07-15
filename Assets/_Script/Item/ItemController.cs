using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the logic, when, where, which item appears with which properties.
/// </summary>
public class ItemController : MonoBehaviour
{
    public Item[] availableItems;
    
    void Start()
    {
        availableItems[0].Display();
        availableItems[1].Display();
    }
}
