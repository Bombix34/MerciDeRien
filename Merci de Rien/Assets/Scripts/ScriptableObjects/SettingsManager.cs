using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : ScriptableObject
{
    public Language currentLanguage;

    public enum Language
    {
        francais,
        english
    }
}
