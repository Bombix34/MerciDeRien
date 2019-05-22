using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorTrigger : MonoBehaviour
{
    public void LaunchObjectAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        if (curState == null)
            return;
        curState.ShootObject();
    }

    public void UseObjectStartAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        if (curState == null)
            return;
        curState.UseTool();
    }

    public void UseObjectEndAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        if (curState == null)
            return;
        curState.EndUseTool();
    }

    public void AutoriseMovementUseToolAnimatorTrigger()
    {
        PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
        if (manager == null)
            return;
        PlayerUseToolState curState = (PlayerUseToolState)manager.GetCurrentState();
        if (curState == null)
            return;
        curState.CanMove = true;
    }
}
