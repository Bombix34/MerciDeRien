using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public GameObject feedbackInteraction;

    protected MeshRenderer mesh;

    public Material[] interactMaterial;
    protected Material[] baseMaterial;

    protected virtual void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        baseMaterial = mesh.materials;

        UpdateFeedback(false);
    }

    public virtual void UpdateFeedback(bool isOn)
    {
        if (interactMaterial != null)
        {
            if (isOn)
                mesh.materials = interactMaterial;
            else
                mesh.materials = baseMaterial;
        }
        if(feedbackInteraction!=null)
            feedbackInteraction.SetActive(isOn);
    }
}
