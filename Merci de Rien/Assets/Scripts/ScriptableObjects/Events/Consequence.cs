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

    public enum ConsequenceType
    {
        PnjChangeBehavior,
        AddDialogue
    }

    public enum CharacterAction
    {
        PursuitPlayer
    }
}
