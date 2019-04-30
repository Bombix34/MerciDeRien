using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorbidZone : MonoBehaviour
{
    Game_Music_Manager wwiseGlobal;

    private void Start()
    {
       wwiseGlobal = transform.parent.GetComponentInParent<Game_Music_Manager>();

        if (wwiseGlobal == null)
            Debug.Log("music manager null");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && wwiseGlobal != null)
        {
            wwiseGlobal.SetMusic_Morbid();
        }

    }

}
