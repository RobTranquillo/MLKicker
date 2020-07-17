using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectSettings-GoalSize", menuName = "KickerItem/Create GoalSize effect settings")]
public class ItemEffectGoalSize : ScriptableObject
{
    [Header("General Settings")]
    public GameObject particleEffect;
    [Header("Effect Settings")]
    [Range(0,10)]
    public float sizingFactor = 0.2f;
    [Range(0, 20)]
    public float duration = 10f;
}
