using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    [SerializeField]
    Dialogue textThanks;
    Text zoneText;

    [SerializeField]
    SettingsManager settings;

    void Start()
    {
        zoneText = GetComponent<Text>();
        if(settings.currentLanguage==SettingsManager.Language.francais)
        {
            zoneText.text = textThanks.frenchSentences[0];
        }
        else
        {
            zoneText.text = textThanks.englishSentences[0];
        }
    }
}
