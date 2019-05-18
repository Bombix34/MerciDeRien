using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedInteractObject : InteractObject
{
    //Attention -> ne pas utiliser la variable mesh de InteractObject

    SkinnedMeshRenderer skinnedRenderer;

    protected override void Start()
    {
        skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        baseMaterial = skinnedRenderer.materials;
    }

    public override void UpdateFeedback(bool isOn)
    {
        if (interactMaterial != null)
        {
            if (isOn)
                skinnedRenderer.materials = interactMaterial;
            else
                skinnedRenderer.materials = baseMaterial;
        }
        if (feedbackInteraction != null)
            feedbackInteraction.SetActive(isOn);
    }

    public override void StartInteraction()
    {
    }

    public override void EndInteraction()
    {
    }
}
