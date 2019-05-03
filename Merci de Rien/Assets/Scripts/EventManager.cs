using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventDatabase database;

    void Start()
    {
        database.ResetDatabase();
    }

}
