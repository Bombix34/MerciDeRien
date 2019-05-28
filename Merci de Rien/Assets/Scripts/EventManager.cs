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

    [SerializeField]
    List<InteractObject> interactivesObjects;
    
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
        List<Predicat> toRemove = new List<Predicat>();
        foreach(var predicat in predicats)
        {
            if (predicat.IsPredicatTrue())
            {
                Debug.Log(predicat.name);
                predicat.ApplyEvent();
                toRemove.Add(predicat);
            }
        }
        foreach(var item in toRemove)
        {
            predicats.Remove(item);
        }
    }

    public void StrangerApparitionEvent()
    {
        StrangerManager stranger = (StrangerManager)GetPNJ(PnjManager.CharacterType.Etranger);
        if (stranger == null)
            return;
        stranger.ChangeState(new StrangerApparitionState(stranger));
    }

    public void StrangerWaitAtPositionEvent(Vector3 destination)
    {
        StrangerManager stranger = (StrangerManager)GetPNJ(PnjManager.CharacterType.Etranger);
        if (stranger == null)
            return;
        stranger.ChangeState(new StrangerWaitAtPosition(stranger, destination));
    }

    public List<InteractObject> GetObjectOfType(InteractObject.ObjectType objectType, PnjManager.CharacterType pnj)
    {
        CleanListObject();
        List<InteractObject> result = new List<InteractObject>();
        foreach(var item in interactivesObjects)
        {
            if((item.objectType==objectType)&&(item.characterOwner==pnj))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public void CleanListObject()
    {
        List<InteractObject> toRm = new List<InteractObject>();
        foreach(var item in interactivesObjects)
        {
            if (item == null)
                toRm.Add(item);
        }
        foreach(var item in toRm)
        {
            interactivesObjects.Remove(item);
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

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void AddPNJ(GameObject pnj)
    {
        pnjs.Add(pnj.GetComponent<PnjManager>());
    }

    public void AddInteractiveObject(InteractObject obj)
    {
        interactivesObjects.Add(obj);
    }
}
