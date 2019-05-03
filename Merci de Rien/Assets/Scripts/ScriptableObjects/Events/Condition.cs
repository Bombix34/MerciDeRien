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

    public MultipleComparison comparisonCumul;

    public bool ConditionResult(EventDatabase database)
    {
        bool result = false;
        EventContainer eventConcerned = database.GetEvent(concernedEvent);
        switch(comparaison)
        {
            case ComparisonType.equal:
                result = valToTest == eventConcerned.value;
                break;
            case ComparisonType.superior:
                result = eventConcerned.value>valToTest;
                break;
            case ComparisonType.inferior:
                result = eventConcerned.value < valToTest;
                break;
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
