using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPointManager : MonoBehaviour
{
    [SerializeField]
    List<InterestPoint> points;
   

    public Vector3 GetInterestPointPosition(InterestPoint.InterestPointType type)
    {
        Vector3 result = Vector3.zero;
        foreach(var item in points)
        {
            if(item.GetInterestType()==type)
            {
                result = item.transform.position;
            }
        }
        return result;
    }
}
