using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public GameObject feedbackInteraction;

    void Start()
    {
        UpdateFeedback(false);
    }

    public void UpdateFeedback(bool isOn)
    {
        feedbackInteraction.SetActive(isOn);
    }
}
