using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MDR/new predicat")]
public class Predicat : ScriptableObject
{
    public EventDatabase database;

    public List<Condition> conditions;

    public List<Consequence> consequences;

    public bool IsPredicatTrue()
    {
        bool result = false;
        for (int i = 0; i < conditions.Count;i++)
        {
            if (i == 0)
                result = conditions[i].ConditionResult(database);
            else
            {
                switch(conditions[i-1].comparisonCumul)
                {
                    case Condition.MultipleComparison.AND:
                    case Condition.MultipleComparison.NONE:
                        result = result && conditions[i].ConditionResult(database);
                        break;
                    case Condition.MultipleComparison.OR:
                        result = result ||conditions[i].ConditionResult(database);
                        break;
                }
            }
        }
        return result;
    }
}
