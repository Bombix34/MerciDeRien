using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SettingsManager settings;

    public EventManager EventManager{ set; get; }

    public Dialogue PLACEHOLDER_PNJ_DIALOGUE;

    List<Dialogue> playerHistoricDialogues;

    public int Patoune { get; set; } = 0;

    private void Start()
    {
        EventManager = GetComponent<EventManager>();
        playerHistoricDialogues = new List<Dialogue>();
    }

    public void AddToHistoric(Dialogue newDialogue)
    {
        foreach (var item in playerHistoricDialogues)
        {
            if (item == newDialogue)
                return;
        }
        playerHistoricDialogues.Add(newDialogue);
    }

    public bool IsDialogue(Dialogue concerned)
    {
        bool result = false;
        foreach (var item in playerHistoricDialogues)
        {
            if (item == concerned)
                result = true;
        }
        return result;
    }
}
