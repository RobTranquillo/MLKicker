using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectSettings_BouncyBall", menuName = "KickerItem/Create BouncyBall effect settings")]
public class ItemEffectBouncyBall : ScriptableObject
{
    [Header("General Settings")]
    public GameObject particleEffect;
    [Header("Effect Settings")]
    [Range(0,10)]
    public float bounciness = 0.2f;
    [Range(0, 20)]
    public float duration = 10f;
}
