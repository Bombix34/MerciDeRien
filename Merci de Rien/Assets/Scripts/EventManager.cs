using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    [SerializeField]
    EventDatabase database;

    [SerializeField]
    List<Predicat> predicats;
    
    void Start()
    {
        database.ResetDatabase();
    }

    public EventDatabase GetDatas()
    {
        return database;
    }

    public EventDatabase ReadDatas()
    {
        return database;
    }
}
