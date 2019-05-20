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

    [SerializeField]
    GameObject player;
    
    void Start()
    {
        database.ResetDatabase();
        foreach(var item in predicats)
        {
            item.database = database;
        }
    }

    public void UpdateCharacterEvent(EventDatabase.EventType eventTypeGeneral, PnjManager.CharacterType character, int val)
    {
        database.UpdateCharacterEvent(eventTypeGeneral, character, val);
        ApplyPredicats();
    }

    public void UpdateEvent(EventDatabase.EventType eventType, int val)
    {
        database.UpdateEvent(eventType, val);
        ApplyPredicats();
    }


    public void ApplyPredicats()
    {
        foreach(var predicat in predicats)
        {
            if (predicat.IsPredicatTrue())
                predicat.ApplyEvent();
        }
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

    public GameObject GetPlayer()
    {
        return player;
    }
}
