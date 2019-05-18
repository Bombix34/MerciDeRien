using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public GameObject feedbackInteraction;

    public List<MeshRenderer> mesh;

    public Material[] interactMaterial;
    protected Material[] baseMaterial;

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
                foreach(var item in mesh)
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
        if(feedbackInteraction!=null)
            feedbackInteraction.SetActive(isOn);
    }

    public virtual void StartInteraction()
    { }
    public virtual void EndInteraction()
    { }
}
