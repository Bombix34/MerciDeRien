using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventDatabase database;

    //public List<Pnjma>

    void Start()
    {
        database.ResetDatabase();
    }

}
