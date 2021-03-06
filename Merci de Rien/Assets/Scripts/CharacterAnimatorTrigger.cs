﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorTrigger : MonoBehaviour
{
    public void LaunchObjectAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        if (manager.GetCurrentState().stateName != "PLAYER_USE_TOOL_STATE")
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        curState.ShootObject();
    }



    public void UseObjectStartAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        if (manager.GetCurrentState().stateName != "PLAYER_USE_TOOL_STATE")
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        curState.UseTool();
    }

    public void UseObjectEndAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        if (manager.GetCurrentState().stateName != "PLAYER_USE_TOOL_STATE")
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        curState.EndUseTool();
    }

    public void AutoriseMovementUseToolAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        if (manager.GetCurrentState().stateName != "PLAYER_USE_TOOL_STATE")
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        curState.CanMove = true;
    }

    public void LaunchBringObjectAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        if (manager.GetCurrentState().stateName != "PLAYER_BRING_OBJECT_STATE")
            return;
        PlayerBringObjectState curState = (PlayerBringObjectState)manager.GetCurrentState();
        curState.ShootObject();
    }

    public void ThrowObjectSFX()
    {
        //AkSoundEngine.PostEvent("MC_throw_play", gameObject);
    }

    public void UseObjectSFX()
    {
        AkSoundEngine.PostEvent("MC_attack_play", gameObject);
    }
}
