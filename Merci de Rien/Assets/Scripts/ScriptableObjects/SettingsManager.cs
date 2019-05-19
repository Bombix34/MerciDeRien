using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : ScriptableObject
{
    public Language currentLanguage;
    [Range (0.01f,0.15f)]
    public float textSpeed = 0.01f;

    public enum Language
    {
        francais,
        english
    }
}
