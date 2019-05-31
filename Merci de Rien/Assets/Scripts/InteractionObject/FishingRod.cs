using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : InteractObject
{
    Animator animator;

    bool hasBeenUsed = false;
    [SerializeField]
    GameObject fishingObject;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        objectType = ObjectType.FishingRod;
        CanTakeObject = true;
        CanInteract = false;
    }

    public override void StartInteraction()
    {
        if ((hasBeenUsed) || (CanInteract == false))
        {
            return;
        }
        base.StartInteraction();
        animator.SetTrigger("Orb");
        hasBeenUsed = true;
        CanInteract = false;
        StartCoroutine(ExitWait());
    }

    public override void EndInteraction()
    {
    }

    public void ExitPlayerState()
    {
        PlayerManager player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        PlayerTransitionState statePlayer = (PlayerTransitionState)player.GetCurrentState();
        statePlayer.ReturnBackToPrevState();
    }

    IEnumerator ExitWait()
    {
        yield return new WaitForSeconds(0.6f);
        if (fishingObject != null)
        {
            fishingObject.transform.parent = null;
            if (fishingObject.GetComponent<Rigidbody>() != null)
                fishingObject.GetComponent<Rigidbody>().useGravity = true;
        }
        ExitPlayerState();
    }
}