using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStone : BringObject
{
    CapsuleCollider capsule;

    protected override void Start()
    {
        base.Start();
        CanInteract = false;
        capsule = GetComponent<CapsuleCollider>();
        objectType = InteractObject.ObjectType.MusicStone;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
    }

    public override void EndInteraction()
    {
    }
}
