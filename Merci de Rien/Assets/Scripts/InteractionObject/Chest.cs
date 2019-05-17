using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractObject
{
    Animator animator;
    [SerializeField]
    GameObject particle;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        particle.SetActive(false);
    }

    public override void StartInteraction()
    {
        animator.SetBool("IsOpen", true);
        particle.SetActive(true);
    }

    public override void EndInteraction()
    {
        animator.SetBool("IsOpen", false);
        particle.SetActive(false);
    }
}
