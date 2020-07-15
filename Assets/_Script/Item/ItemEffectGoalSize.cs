using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectSettings-GoalSize", menuName = "KickerItem/Create GoalSize effect settings")]
public class ItemEffectGoalSize : ItemEffect
{
    [Header("Settings")]
    public float sizingFactor = 0.2f;
}
