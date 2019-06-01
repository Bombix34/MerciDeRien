using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAxo : SkinnedInteractObject
{

    [SerializeField]
    List<Dialogue> textToDisplay;

    protected override void Start()
    {
        base.Start();
        CanTakeObject = true;
        objectType = ObjectType.LittleAxo;
        characterOwner = PnjManager.CharacterType.none;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (textToDisplay != null)
            DialogueUiManager.Instance.StartDialogue(textToDisplay[(int)Random.Range(0f,textToDisplay.Count)]);
    }

    public override void EndInteraction()
    {
        //  DialogueUiManager.Instance.EndDialogue();
    }
}
