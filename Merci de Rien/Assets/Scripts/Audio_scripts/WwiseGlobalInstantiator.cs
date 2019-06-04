using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseGlobalInstantiator : MonoBehaviour
{
    [SerializeField]
    GameObject wwiseGlobalPREFAB;

    WwiseGlobalGeneral audioManager;

    void Awake()
    {
        {
            try
            {
                audioManager = GameObject.FindGameObjectWithTag("WwiseGlobalPrefab").GetComponent<WwiseGlobalGeneral>();
                Debug.Log("WwiseGlobal FOUND");
            }
            catch (NullReferenceException)
            {
                audioManager = Instantiate(wwiseGlobalPREFAB).GetComponent<WwiseGlobalGeneral>();
                Debug.Log("WwiseGlobal CREATED");
            }
        }
    }
}
