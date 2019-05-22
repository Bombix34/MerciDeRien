using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Music_Manager : Singleton<Game_Music_Manager>
{
    public MusicType CurMusic { get; set; }

    //Il faudra procéder différamment quand le cycle jour/nuit sera réellement implémenté
    private DayLightCycle dayLightCycle;
    private float gameCurrentTime;

    private void Start()
    {
        SwitchMusic(MusicType.Village);

        dayLightCycle = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DayLightCycle>();

    }

    private void Update()
    {
        if (dayLightCycle != null)
        {
        gameCurrentTime = dayLightCycle.GetCurrentTime() * 24f;
        AkSoundEngine.SetRTPCValue("game_time", gameCurrentTime, gameObject);
        }
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
                AkSoundEngine.SetRTPCValue("game_time", 13, gameObject);
                AkSoundEngine.SetRTPCValue("village_karma", 0, gameObject);
                AkSoundEngine.SetSwitch("music_switch", "village", gameObject);
                break;
            case MusicType.Village_Night:
                AkSoundEngine.SetSwitch("music_switch", "village", gameObject);
                AkSoundEngine.SetRTPCValue("game_time", 24, gameObject);
                break;
            case MusicType.Battle:
                AkSoundEngine.SetSwitch("music_switch", "battle", gameObject);
                break;
            case MusicType.Morbid:
                AkSoundEngine.SetRTPCValue("village_karma", 100, gameObject);
                //AkSoundEngine.PostEvent("Music_Change_Morbid", this.gameObject);
                break;
        }
    }

    public enum MusicType
    {
        Village,
        Village_Night,
        Battle,
        Morbid
    }
}
