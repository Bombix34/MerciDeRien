using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Music_Manager : Singleton<Game_Music_Manager>
{
    public MusicType CurMusic { get; set; }

    private DayLightCycle dayLightCycle;
    private float gameCurrentTime;

    private void Start()
    {
        //SwitchMusic(MusicType.Village);

        try
        {
            dayLightCycle = Camera.main.GetComponent<DayLightCycle>();
            Debug.Log("Camera get");
        }
        catch (NullReferenceException)
        {
            Debug.Log("Didn't get the camera :<");
            return;
        }
    }

    private void Update()
    {
        if (dayLightCycle != null)
        {
            gameCurrentTime = dayLightCycle.GetCurrentTime() * 48f;
            AkSoundEngine.SetRTPCValue("game_time", gameCurrentTime, gameObject);
        }
    }

    public void MusicStoneDistance(int stoneID, float distance)
    {
        AkSoundEngine.SetRTPCValue("Distance_MS_0"+stoneID, distance, dayLightCycle.gameObject);
        AkSoundEngine.SetRTPCValue("Distance_MS_Gen", distance, dayLightCycle.gameObject);
    }

    public void MusicStoneHurt(int stoneId)
    {
        AkSoundEngine.PostEvent("Ocarina_0" + stoneId + "_Damaged", dayLightCycle.gameObject);
    }

    public void MusicStoneDestroyed(int stoneId)
    {
        AkSoundEngine.PostEvent("Ocarina_0" + stoneId + "_Destroyed", dayLightCycle.gameObject);
    }


    public void SwitchMusic(MusicType newType)
    {
        //APPELER CETTE FONCTION POUR CHANGER DE MUSIQUE
        if (newType == CurMusic)
            return;
        CurMusic = newType;
        PlayMusic();
    }

    private void PlayMusic()
    {
        switch(CurMusic)
        {
            case MusicType.Village:
                Debug.Log("switching to VILLAGE music");
                AkSoundEngine.SetRTPCValue("village_karma", 0, gameObject);
                AkSoundEngine.SetSwitch("music_switch", "village", gameObject);
                break;
            case MusicType.Ceremony:
                Debug.Log("switching to CEREMONY music");
                AkSoundEngine.SetSwitch("music_switch", "festival", gameObject);
                break;
            case MusicType.Battle:
                Debug.Log("switching to BATTLE music");
                AkSoundEngine.SetSwitch("music_switch", "battle", gameObject);
                break;
            case MusicType.Menu:
                Debug.Log("switching to MENU music");
                AkSoundEngine.SetSwitch("music_switch", "menu", gameObject);
                break;
        }
    }

    public void EndAudioScene()
    {
        AkSoundEngine.PostEvent("GAME_silence", gameObject);
    }

    public enum MusicType
    {
        Village,
        Village_Night,
        Battle,
        Menu,
        Ceremony
    }
}
