using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SettingsManager settings;

    public EventManager EventManager{ set; get; }

    public Dialogue PLACEHOLDER_PNJ_DIALOGUE;

    private void Start()
    {
        EventManager = GetComponent<EventManager>();
    }
}
