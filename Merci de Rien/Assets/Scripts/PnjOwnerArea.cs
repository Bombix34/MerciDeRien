﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjOwnerArea : MonoBehaviour
{

    [SerializeField]
    PnjManager.CharacterType characterOwner;

    [SerializeField]
    List<GameObject> objectsInZone;     //les objets dans la zone
    List<GameObject> pnjObjectMemory;   // quelles informations a le pnj sur les objets dans la zone

    int otherObjectsStealed = 0;

    private void Start()
    {
        pnjObjectMemory = new List<GameObject>();
        foreach(var item in objectsInZone)
        {
            pnjObjectMemory.Add(item);
        }
    }

    void PnjCheckStealedObjects()
    {
        int stealedObjets = (pnjObjectMemory.Count - objectsInZone.Count)+otherObjectsStealed;
        if(stealedObjets>0)
        {
            EventManager.Instance.UpdateCharacterEvent(EventDatabase.EventType.stealedObjectsTotal, characterOwner, stealedObjets);
            pnjObjectMemory = objectsInZone;
            ResetOtherObjectStealed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag=="PNJ" && other.gameObject.GetComponent<PnjManager>().GetCharacterType()==characterOwner)
        {
            PnjCheckStealedObjects();
        }
       else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerManager>().IsBringingObject() != null)
        {
            //SI JE REVIENS DANS LA ZONE AVEC UN OBJET DU PERSO
            BringObject concernedObj = other.gameObject.GetComponent<PlayerManager>().IsBringingObject();
            CheckEventObject(concernedObj);
            if (concernedObj.GetComponent<InteractObject>().CanTakeObject)
                return;
            if (concernedObj.GetOwner() == characterOwner)
            {
                objectsInZone.Add(concernedObj.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PNJ" && other.gameObject.GetComponent<PnjManager>().GetCharacterType() == characterOwner)
        {
            PnjCheckStealedObjects();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerManager>().IsBringingObject()!=null)
        {
            //QUAND ON SORT DE LA ZONE EN VOLANT UN OBJET
            BringObject concernedObj = other.gameObject.GetComponent<PlayerManager>().IsBringingObject();
            if (concernedObj.GetComponent<InteractObject>().CanTakeObject)
                return;
            if(concernedObj.GetOwner()==characterOwner)
            {
                
                objectsInZone.Remove(concernedObj.gameObject);
            }
        }
    }

    public void IncrementOtherObjectStealed()
    {
        //pour le coffre
        otherObjectsStealed++;
    }

    public void ResetOtherObjectStealed()
    {
        otherObjectsStealed = 0;
    }

    public void CheckEventObject(InteractObject obj)
    {
        switch(obj.objectType)
        {
            case InteractObject.ObjectType.Baton:
                if (characterOwner == PnjManager.CharacterType.Artisan)
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectBoisToArtisan, 1);
                break;
            case InteractObject.ObjectType.Plante:
                if (characterOwner == PnjManager.CharacterType.Healer)
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectPlanteToShaman, 1);
                break;
            case InteractObject.ObjectType.Fourche:
                if (characterOwner == PnjManager.CharacterType.Paysan)
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectFourcheToPaysan, 1);
                break;
            case InteractObject.ObjectType.Potion:
                if (characterOwner == PnjManager.CharacterType.Pecheur)
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectPotionToPecheur, 1);
                break;
            default:
                break;
        }
    }
}
