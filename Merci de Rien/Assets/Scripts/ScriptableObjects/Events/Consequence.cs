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


    //ADD/REMOVE DIALOGUE
    public Dialogue dialogueConcerned;

    //INCREMENT/DECREMENT DATABASE
    public EventDatabase eventDatabase;
    public EventDatabase.EventType eventType;

    //INTERACTIVE OBJECTS
    public InteractObject objectConcerned;


    //INT
    public int intModificator;


    public enum ConsequenceType
    {
        PnjChangeBehavior,
        AddDialogue,
        RemoveDialogue,
        IncrementDatabase,
        DecrementDatabase,
        AutorisationTakeObject,
        RemoveAutorisationTakeObject,
        GainKey,
        RemoveKey,
        AutorisationInteractionObject,
        RemoveAutorisationInteractionObject
    }

    public enum CharacterAction
    {
        PursuitPlayer,
        Boude
    }
}
