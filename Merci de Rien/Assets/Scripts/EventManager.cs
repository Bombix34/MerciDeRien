using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    [SerializeField]
    EventDatabase database;

    [SerializeField]
    List<PnjManager> pnjs;

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

    public PnjManager GetPNJ(PnjManager.CharacterType concerned)
    {
        PnjManager returnVal = null;
        foreach(var item in pnjs)
        {
            if (item.GetCharacterType() == concerned)
                returnVal = item;
        }
        return returnVal;
    }
}
