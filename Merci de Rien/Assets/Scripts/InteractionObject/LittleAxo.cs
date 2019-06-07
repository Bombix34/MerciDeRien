using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAxo : SkinnedInteractObject
{

    [SerializeField]
    List<Dialogue> textToDisplay;

    Dialogue previousDialogue = null;

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
        SelectDialogue();
    }

    public override void EndInteraction()
    {
        //  DialogueUiManager.Instance.EndDialogue();
    }


    public void SelectDialogue()
    {
        if (textToDisplay == null)
            return;
        Dialogue selected = textToDisplay[(int)Random.Range(0f, textToDisplay.Count)];
        if(previousDialogue!=null)
        {
            if(selected==previousDialogue&&textToDisplay.Count>1)
            {
                SelectDialogue();
                return;
            }
        }
        previousDialogue = selected;
        DialogueUiManager.Instance.StartDialogue(selected);


    }
}
