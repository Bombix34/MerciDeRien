using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedInteractObject : InteractObject
{
    //Attention -> ne pas utiliser la variable mesh de InteractObject

    SkinnedMeshRenderer skinnedRenderer;

    protected override void Start()
    {
        InitEventManager();
        skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        baseMaterial = skinnedRenderer.materials;
        if (feedbackInteraction != null)
            textContainer = feedbackInteraction.GetComponentInChildren<TextMesh>();
        if (GetComponent<PnjManager>() != null)
            CanTakeObject = true;
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
        UpdateFeedbackInteraction(isOn);
    }

    public override void StartInteraction()
    {
    }

    public override void EndInteraction()
    {
    }
}
