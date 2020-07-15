using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiball-EffectSettings", menuName = "KickerItem/Create Multiball effect settings")]
public class ItemEffectMultiBall : ItemEffect
{
    [Header("Settings")]
    public short count = 2;
}
