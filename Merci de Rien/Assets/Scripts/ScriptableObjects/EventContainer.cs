using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EventContainer 
{
    public EventDatabase.EventType type;
    public int value;

    public EventContainer(EventDatabase.EventType newType, int newVal)
    {
        type = newType;
        value = newVal;
    }
}
