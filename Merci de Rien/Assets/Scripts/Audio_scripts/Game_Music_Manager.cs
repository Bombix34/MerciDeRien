using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Music_Manager : MonoBehaviour
{
    public void SetMusic_Morbid()
    {
        AkSoundEngine.SetRTPCValue("village_karma", 100, gameObject);
        //AkSoundEngine.PostEvent("Music_Change_Morbid", this.gameObject);
    }

    public void SetMusic_Battle()
    {
        AkSoundEngine.SetSwitch("music_switch", "battle", gameObject);
    }

    public void SetMusic_Night()
    {
        AkSoundEngine.SetSwitch("music_switch", "village", gameObject);
        AkSoundEngine.SetRTPCValue("game_time", 24, gameObject);
        
    }

    public void SetMusic_Village()
    {
        AkSoundEngine.SetRTPCValue("game_time", 13, gameObject);
        AkSoundEngine.SetRTPCValue("village_karma", 0, gameObject);
        AkSoundEngine.SetSwitch("music_switch", "village", gameObject);
    }
}
