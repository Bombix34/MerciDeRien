using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjOwnerArea : MonoBehaviour
{

    [SerializeField]
    PnjManager.CharacterType characterOwner;

    [SerializeField]
    List<GameObject> objectsInZone;     //les objets dans la zone
    List<GameObject> pnjObjectMemory;   // quelles informations a le pnj sur les objets dans la zone

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
        int stealedObjets = pnjObjectMemory.Count - objectsInZone.Count;
        if(stealedObjets>0)
        {
            EventManager.Instance.GetDatas().UpdateCharacterEvent(EventDatabase.EventType.stealedObjectsTotal, characterOwner, stealedObjets);
            pnjObjectMemory = objectsInZone;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag=="PNJ" && other.gameObject.GetComponent<PnjManager>().GetCharacterType()==characterOwner)
        {
            PnjCheckStealedObjects();
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
            if(concernedObj.GetOwner()==characterOwner)
            {
                
                objectsInZone.Remove(concernedObj.gameObject);
            }
        }
    }
}
