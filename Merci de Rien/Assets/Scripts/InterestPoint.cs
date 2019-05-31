using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [SerializeField]
    InterestPointType type;

    public InterestPointType GetInterestType()
    {
        return type;
    }

    public enum InterestPointType
    {
        champ,
        healerHouse,
        artisanHouse,
        stonehedge
    }
}
