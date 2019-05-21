using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maillet : BringObject
{
    CapsuleCollider capsule;

    protected override void Start()
    {
        base.Start();
        objectType = ObjectType.Maillet;
        body = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        body.useGravity = false;
        capsule.isTrigger = false;
        //body.isKinematic = true;
    }

    public override void EndInteraction()
    {
        body.useGravity = true;
        capsule.isTrigger = false;
        //body.isKinematic = true;
        body.constraints = RigidbodyConstraints.FreezeRotationY;
    }
}