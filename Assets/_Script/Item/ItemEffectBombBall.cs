using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BombBall_EffectSettings", menuName = "KickerItem/Create BombBall effect settings")]
public class ItemEffectBombBall : ScriptableObject
{
    [Header("General Settings")]
    public GameObject particleEffect;
    [Header("Effect Settings")]
    [Range(0, 20)]
    public int count = 4;
    [Range(0, 20)]
    public int livingTime = 4;
}
