using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    Game_Music_Manager musicManager;
    PlayerManager manager;
    GameObject interactedObject;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    void Start()
    {
        musicManager = Game_Music_Manager.Instance;
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

    private void Update()
    {
        AkSoundEngine.SetRTPCValue("water_distance", transform.position.y);
    }
}
