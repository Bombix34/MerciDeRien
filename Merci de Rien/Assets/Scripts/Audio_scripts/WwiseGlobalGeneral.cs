using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseGlobalGeneral : MonoBehaviour
{
    //Script non utilisé !

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.Log("WwiseGlobal General initialized");
    }
}
