using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractObject
{
    Animator animator;
    [SerializeField]
    GameObject particle;

    [SerializeField]
    PnjManager.CharacterType characterOwner;
    [SerializeField]
    PnjOwnerArea area;

    bool HasBeenOpened = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        particle.SetActive(false);
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        animator.SetBool("IsOpen", true);
        particle.SetActive(true);
        CheckSteal();
        HasBeenOpened = true;
    }

    public override void EndInteraction()
    {
        animator.SetBool("IsOpen", false);
        particle.SetActive(false);
    }

    public void CheckSteal()
    {
        if((area!=null)&&(!HasBeenOpened)&&(!CanTakeObject))
        {
            area.IncrementOtherObjectStealed();
        }
    }
}
