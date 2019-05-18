using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField]
    Game_Music_Manager.MusicType musicToSwitchTo;

    [SerializeField]
    [Header("Return to prev music on trigger exit")]
    bool IsReturnOnTriggerExit = false;

    //Game_Music_Manager.MusicType prevMusic;

    Game_Music_Manager music_manager;

    private void Start()
    {
        music_manager = Game_Music_Manager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && music_manager != null)
        {
            music_manager.SwitchMusic(musicToSwitchTo);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && music_manager != null && IsReturnOnTriggerExit)
        {
            music_manager.SwitchMusic(musicToSwitchTo);
        }

    }
}
