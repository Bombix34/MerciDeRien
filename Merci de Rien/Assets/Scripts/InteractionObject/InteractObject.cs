using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public GameObject feedbackInteraction;

    public List<MeshRenderer> mesh;

    public Material[] interactMaterial;
    protected Material[] baseMaterial;

    public bool CanTakeObject { get; set; } = false;
    public bool CanInteract { get; set; } = true;

    protected TextMesh textContainer;
    float textPosX;
    public Dialogue interactText;

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
        if (feedbackInteraction != null)
        {
            textContainer = feedbackInteraction.GetComponentInChildren<TextMesh>();
            textPosX = textContainer.GetComponent<RectTransform>().localPosition.x;
        }
        if (GetComponent<PnjManager>() != null)
            CanTakeObject = true;
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
        UpdateFeedbackInteraction(isOn);
    }

    protected virtual void UpdateFeedbackInteraction(bool isOn)
    {
        if (feedbackInteraction == null)
            return;
        feedbackInteraction.SetActive(isOn);
        if (isOn)
        {
            if (EventManager.Instance.GetPlayer() != null)
            {
                RectTransform textPosition = textContainer.GetComponent<RectTransform>();
                float playerPositionX = EventManager.Instance.GetPlayer().transform.position.x;
                float result = this.transform.position.x - playerPositionX;

                if (result<0)
                {
                    //joueur a droite
                    if (textPosition.localPosition.x > 0)
                        textPosition.localPosition = new Vector3(-1* textPosition.localPosition.x, textPosition.localPosition.y, textPosition.localPosition.z);
                }
                else
                {
                    //joueur a gauche
                    if (textPosition.localPosition.x < 0)
                        textPosition.localPosition = new Vector3(-1* textPosition.localPosition.x, textPosition.localPosition.y, textPosition.localPosition.z);
                }
            }
            if (!CanTakeObject)
                textContainer.text = GetInteractText(true);
            else
                textContainer.text = GetInteractText(false);
        }
    }

    public string GetInteractText(bool isStealing)
    {
        string returnVal = "";
        SettingsManager settings = GameManager.Instance.settings;
        if (settings.currentLanguage==SettingsManager.Language.francais)
        {
            if (!isStealing)
                returnVal = interactText.frenchSentences[0];
            else
                returnVal = "Voler";
        }
        else if (settings.currentLanguage == SettingsManager.Language.english)
        {
            if (!isStealing)
                returnVal = interactText.englishSentences[0];
            else
                returnVal = "Steal";
        }
        return returnVal;
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
