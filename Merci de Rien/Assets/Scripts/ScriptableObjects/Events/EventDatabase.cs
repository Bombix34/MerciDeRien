using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDatabase : ScriptableObject
{
    /*
     * CHARACTER______________
     *  Artisan
     *  Paysan
     *  Responsable
     *  Healer
     *  Troubadour
     *  Etranger   
     */

    //ATTENTION
    //LA LISTE D'EVENT DOIT ETRE DANS LORDRE DE LENUM
    public List<EventContainer> events;


    public void ResetDatabase()
    {
        events = new List<EventContainer>();
        int dataCount = (int)EventDatabase.EventType.NUMBER_OF_EVENT;
        for (int i = 0; i < dataCount; i++)
        {
            events.Add(new EventContainer((EventDatabase.EventType)i, 0));
        }
    }

    public void UpdateEvent(EventType eventType, int val)
    {
        events[(int)eventType].value += val;
    }

    public void UpdateCharacterEvent(EventType eventTypeGeneral, PnjManager.CharacterType character, int val)
    {
        if (eventTypeGeneral != EventType.violenceTotal && eventTypeGeneral != EventType.conversationTotal && eventTypeGeneral != EventType.brokeObjectsTotal
            && eventTypeGeneral != EventType.stealedObjectsTotal && eventTypeGeneral != EventType.questTotal)
            return;

        events[(int)eventTypeGeneral].value += val;

        if (character == PnjManager.CharacterType.none)
            return;
        //ATTENTION : LES EVENT PERSO DOIVENT TOUJOURS ETRE DANS LORDRE ART - PAYS - RESP - HEAL - TROUB - ETRANG
        int indexEventCharacter = (int)eventTypeGeneral + (int)character + 1;
        events[indexEventCharacter].value += val;

        UpdateCharactersMet(eventTypeGeneral);
    }

    public EventContainer GetEvent(EventType eventType)
    {
        return events[(int)eventType];
    }

    public void UpdateCharactersMet(EventType eventType)
    {
        if (eventType != EventType.conversationTotal)
            return;
        EventContainer characterMet = GetEvent(EventType.charactersMet);
        characterMet.value = 0;
        if (GetEvent(EventType.conversationWithHealer).value > 0)
            characterMet.value++;
        if (GetEvent(EventType.conversationWithPaysan).value > 0)
            characterMet.value++;
        if (GetEvent(EventType.conversationWithArtisan).value > 0)
            characterMet.value++;
        if (GetEvent(EventType.conversationWithEtranger).value > 0)
            characterMet.value++;
        if (GetEvent(EventType.conversationWithTroubadour).value > 0)
            characterMet.value++;
        if (GetEvent(EventType.conversationWithResponsable).value > 0)
            characterMet.value++;
    }

    public enum EventType
    {
        violenceTotal,                  //DONE
        violenceAgainstArtisan,         //DONE
        violenceAgainstPaysan,          //DONE
        violenceAgainstResponsable,     //DONE
        violenceAgainstHealer,          //DONE
        violenceAgainstTroubadour,      //DONE
        violenceAgainstEtranger,        //DONE

        distanceDone,
        zoneDiscovered,
        timePassed,

        conversationTotal,              //DONE
        conversationWithArtisan,        //DONE
        conversationWithPaysan,         //DONE
        conversationWithResponsable,    //DONE
        conversationWithHealer,         //DONE
        conversationWithTroubadour,     //DONE
        conversationWithEtranger,       //DONE
        charactersMet,                  //DONE

        brokeObjectsTotal,              //DONE
        brokeObjectsArtisan,            //DONE
        brokeObjectsPaysan,             //DONE
        brokeObjectsResponsable,        //DONE
        brokeObjectsHealer,             //DONE
        brokeObjectsTroubadour,         //DONE
        brokeObjectsEtranger,           //DONE
        floreBrokeObjects,              //DONE

        stealedObjectsTotal,
        stealedObjectsArtisan,
        stealedObjectsPaysan,
        stealedObjectsResponsable,
        stealedObjectsHealer,
        stealedObjectsTroubadour,
        stealedObjectsEtranger,

        questTotal,
        questArtisan,
        questPaysan,
        questResponsable,
        questHealer,
        questTroubadour,
        questEtranger,
        currentQuest,

        NUMBER_OF_EVENT
    }

}


/*
 //TOD0___________________________________
    [Header("VIOLENCE AGAINST CHARACTERS")]//_________________________________________
    public int violenceTotal = 0;
    [Space]
    public int violenceAgainstArtisan = 0;
    public int violenceAgainstPaysan = 0;
    public int violenceAgainstResponsable = 0;
    public int violenceAgainstHealer = 0;
    public int violenceAgainstTroubadour = 0;
    public int violenceAgainstEtranger = 0;

    [Space]

    [Header("ZONE, DISTANCE AND TIME")]//_________________________________________
    public int distanceDone = 0;
    public int zoneDiscovered = 0;
    public int timePassed = 0;

    [Space]

    [Header("TALKING EVENTS")]//_________________________________________
    public int conversationTotal = 0;
    [Space]
    public int conversationWithArtisan = 0;
    public int conversationWithPaysan = 0;
    public int conversationWithResponsable = 0;
    public int conversationWithHealer = 0;
    public int conversationWithTroubadour = 0;
    public int conversationWithEtranger = 0;
    [Space]
    public int charactersMet = 0;

    [Space]

    [Header("BREAK OBJECTS")]//_________________________________________
    public int brokeObjectsTotal = 0;
    [Space]
    public int brokeObjectsArtisan = 0;
    public int brokeObjectsPaysan = 0;
    public int brokeObjectsResponsable = 0;
    public int brokeObjectsHealer = 0;
    public int brokeObjectsTroubadour = 0;
    public int brokeObjectsEtranger = 0;
    [Space]
    public int floreBrokeObjects = 0;

    [Space]

    [Header("STEAL OBJECTS")]//_________________________________________
    public int stealedObjectsTotal = 0;
    [Space]
    public int stealedObjectsArtisan = 0;
    public int stealedObjectsPaysan = 0;
    public int stealedObjectsResponsable = 0;
    public int stealedObjectsHealer = 0;
    public int stealedObjectsTroubadour = 0;
    public int stealedObjectsEtranger = 0;

    [Header("QUEST")]//_________________________________________
    public int questTotal = 0;
    [Space]
    public int questArtisan = 0;
    public int questPaysan = 0;
    public int questResponsable = 0;
    public int questHealer = 0;
    public int questTroubadour = 0;
    public int questEtranger = 0;
    [Space]
    public int currentQuest = 0;
*/