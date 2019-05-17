using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    PlayerManager manager;
    GameObject interactedObject;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    void Start()
    {
        
    }

    public void PlayInteractFeedbackSound(GameObject actualObject)
    {
        if((interactedObject!=null)&&(actualObject==interactedObject))
        {
            return;
        }
        else
        {
            interactedObject = actualObject;
            AkSoundEngine.PostEvent("UI_item_highlight_play", this.gameObject);
        }
    }

    public void ResetInteractObject()
    {
        interactedObject = null;
    }
}
