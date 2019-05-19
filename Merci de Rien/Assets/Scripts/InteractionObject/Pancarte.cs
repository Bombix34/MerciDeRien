using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancarte : InteractObject
{

    [SerializeField]
    Dialogue textToDisplay;

    protected override void Start()
    {
        base.Start();
        CanTakeObject = true;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (textToDisplay != null)
            DialogueUiManager.Instance.StartDialogue(textToDisplay);
    }

    public override void EndInteraction()
    {
        DialogueUiManager.Instance.EndDialogue();
    }
}
