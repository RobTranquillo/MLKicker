using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiball-EffectSettings", menuName = "KickerItem/Create Multiball effect settings")]
public class ItemEffectMultiBall : ScriptableObject
{
    [Header("General Settings")]
    public GameObject particleEffect;
    [Header("Effect Settings")]
    [Range(0, 20)]
    public int count = 4;
    [Range(0, 20)]
    public int livingTime = 4;
}
