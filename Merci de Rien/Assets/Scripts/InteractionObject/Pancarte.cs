using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancarte : InteractObject
{

    protected override void Start()
    {
        base.Start();
        CanTakeObject = true;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
    }

    public override void EndInteraction()
    {
    }
}
