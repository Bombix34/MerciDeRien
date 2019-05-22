using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Condition
{ 
    public EventDatabase.EventType concernedEvent;
    public ComparisonType comparaison;
    public int valToTest = 0;
    public Dialogue concernedDialogue=null;

    public MultipleComparison comparisonCumul;

    public bool ConditionResult(EventDatabase database)
    {
        bool result = false;
        EventContainer eventConcerned = database.GetEvent(concernedEvent);
        if (concernedEvent == EventDatabase.EventType.DialogueHasBeenSaid)
        {
            if(valToTest==0)
            {
                //On cherche si le dialogue n'a pas été dit
                result = GameManager.Instance.IsDialogue(concernedDialogue) == false;
            }
            else
            {
                //On cherche si le dialogue a été dit
                result = GameManager.Instance.IsDialogue(concernedDialogue);
            }
        }
        else
        {
            switch (comparaison)
            {
                case ComparisonType.equal:
                    result = valToTest == eventConcerned.value;
                    break;
                case ComparisonType.superior:
                    result = eventConcerned.value > valToTest;
                    break;
                case ComparisonType.inferior:
                    result = eventConcerned.value < valToTest;
                    break;
            }
        }
        return result;
    }

    public enum ComparisonType
    {
        superior,
        inferior,
        equal
    }

    public enum MultipleComparison
    {
        NONE, 
        OR,
        AND
    }
}
