using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Consequence
{

    public ConsequenceType consequence;

    //PNJ CHANGE BEHAVIOR
    public PnjManager.CharacterType characterConcerned;
    public CharacterAction actionChoice;

    //PNJ MOOD
    public PnjManager.Mood mood;

    //ADD/REMOVE DIALOGUE
    public Dialogue dialogueConcerned;

    //INCREMENT/DECREMENT DATABASE
    public EventDatabase eventDatabase;
    public EventDatabase.EventType eventType;

    //INTERACTIVE OBJECTS
    public InteractObject.ObjectType objectConcerned;

    //INT
    public int intModificator;

    //PLACE POSITION
    public InterestPoint.InterestPointType placeConcerned;

    public void ApplyConsequence()
    {
        switch (consequence)
        {
            case Consequence.ConsequenceType.PnjChangeBehavior:
                ApplyPnjSwitchBehavior();
                break;
            case Consequence.ConsequenceType.PnjChangeMood:
                if(characterConcerned!=PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if (pnjConcerned == null)
                        return;
                    pnjConcerned.CurrentMood = mood;
                }
                break;
            case Consequence.ConsequenceType.AddDialogue:
                if (characterConcerned != PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if ((pnjConcerned == null)||(dialogueConcerned==null))
                        return;
                    pnjConcerned.dialogueManager.AddDialogue(dialogueConcerned);
                }
                break;
            case Consequence.ConsequenceType.RemoveDialogue:
                if (characterConcerned != PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if ((pnjConcerned == null) || (dialogueConcerned == null))
                        return;
                    pnjConcerned.dialogueManager.RemoveDialogue(dialogueConcerned);
                }
                break;
            case Consequence.ConsequenceType.AutorisationTakeObject:
                //  objectConcerned.GetComponent<InteractObject>().CanTakeObject = true;
                List<InteractObject> concerned = EventManager.Instance.GetObjectOfType(objectConcerned, characterConcerned);
                foreach(var item in concerned)
                {
                    item.CanTakeObject = true;
                }
                break;
            case Consequence.ConsequenceType.RemoveAutorisationTakeObject:
                concerned = EventManager.Instance.GetObjectOfType(objectConcerned, characterConcerned);
                foreach (var item in concerned)
                {
                    item.CanTakeObject = false;
                }
                break;
            case Consequence.ConsequenceType.AutorisationInteractionObject:
                concerned = EventManager.Instance.GetObjectOfType(objectConcerned, characterConcerned);
                foreach (var item in concerned)
                {
                    item.CanInteract = true;
                }
                break;
            case Consequence.ConsequenceType.RemoveAutorisationInteractionObject:
                concerned = EventManager.Instance.GetObjectOfType(objectConcerned, characterConcerned);
                foreach (var item in concerned)
                {
                    item.CanInteract = false;
                }
                break;
            case Consequence.ConsequenceType.GainKey:
                GameManager.Instance.AddKey(intModificator);
                break;
            case Consequence.ConsequenceType.RemoveKey:
                GameManager.Instance.RemoveKey(intModificator);
                break;
            case Consequence.ConsequenceType.StrangerApparition:
                EventManager.Instance.StrangerApparitionEvent();
                break;
            case Consequence.ConsequenceType.StrangerApparitionAtPoint:
                Vector3 pnjDesiredPosition = GameManager.Instance.GetInterestPointManager().GetInterestPointPosition(placeConcerned);
                EventManager.Instance.StrangerWaitAtPositionEvent(pnjDesiredPosition);
                break;
        }
    }

    void ApplyPnjSwitchBehavior()
    {
        if (characterConcerned == PnjManager.CharacterType.none)
            return;
        PnjManager pnj = EventManager.Instance.GetPNJ(characterConcerned);
        switch(actionChoice)
        {
            case CharacterAction.Boude:
                break;
            case CharacterAction.PursuitPlayer:
                pnj.ChangeState(new PursuitPlayerState(pnj, pnj.GetCurrentState()));
                break;
            case CharacterAction.GoToPlace:
                Vector3 pnjDesiredPosition = GameManager.Instance.GetInterestPointManager().GetInterestPointPosition(placeConcerned);
                pnj.ChangeState(new WanderAroundState(pnj, pnjDesiredPosition));
                break;
        }
    }

    public enum ConsequenceType
    {
        PnjChangeBehavior,
        PnjChangeMood,
        AddDialogue,
        RemoveDialogue,
        AutorisationTakeObject,
        RemoveAutorisationTakeObject,
        GainKey,
        RemoveKey,
        AutorisationInteractionObject,
        RemoveAutorisationInteractionObject,
        StrangerApparition,
        StrangerApparitionAtPoint
    }

    public enum CharacterAction
    {
        PursuitPlayer,
        Boude,
        GoToPlace
    }
}
