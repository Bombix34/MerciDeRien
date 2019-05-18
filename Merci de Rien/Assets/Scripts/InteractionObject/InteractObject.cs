using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public GameObject feedbackInteraction;

    public List<MeshRenderer> mesh;

    public Material[] interactMaterial;
    protected Material[] baseMaterial;

    public bool CanStealObject { get; set; } = false;
    public bool CanInteract { get; set; } = true;

    protected virtual void Start()
    {
        if(mesh.Count==0)
        {
            mesh = new List<MeshRenderer>
            {
                GetComponent<MeshRenderer>()
            };
        }
        baseMaterial = mesh[0].materials;

        UpdateFeedback(false);
    }

    public virtual void UpdateFeedback(bool isOn)
    {
        if (interactMaterial != null)
        {
            if (isOn)
            {
                if (!CanInteract)
                    return;
                foreach (var item in mesh)
                {
                    item.materials = interactMaterial;
                }
            }
            else
            {
                foreach (var item in mesh)
                {
                    item.materials = baseMaterial;
                }
            }
        }
        if (feedbackInteraction != null)
        {
            if (!CanInteract)
                return;
            feedbackInteraction.SetActive(isOn);
        }
    }
    

    public virtual void StartInteraction()
    {
        if (!CanInteract)
            return;
    }
    public virtual void EndInteraction()
    {
    }
}
