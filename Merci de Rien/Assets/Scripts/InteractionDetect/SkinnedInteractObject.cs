using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedInteractObject : InteractObject
{
    //Attention -> ne pas utiliser la variable mesh de InteractObject

    SkinnedMeshRenderer skinnedRenderer;

    protected override void Awake()
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
}
