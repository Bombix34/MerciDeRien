using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFXManager : Singleton<FlashFXManager>
{
    [SerializeField]
    GameObject flashPanel;

    private void Start()
    {
        flashPanel.SetActive(false);
    }

    public void Fondu(Color color)
    {
        StartCoroutine(StartFondu(color));
    }

    public void Flash(Color color)
    {
        StartCoroutine(StartFlash(color));
    }

    IEnumerator StartFlash(Color color)
    {
        Image flashImg = flashPanel.GetComponent<Image>();
        flashImg.color = new Vector4(color.r, color.g, color.b, 0.8f);
        flashPanel.SetActive(true);
        while(flashImg.color.a>0)
        {
            flashImg.color = new Vector4(color.r, color.g, color.b, flashImg.color.a-Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator StartFondu(Color color)
    {
        Debug.Log("fondu");
        Image flashImg = flashPanel.GetComponent<Image>();
        flashImg.color = new Vector4(color.r, color.g, color.b, 0f);
        flashPanel.SetActive(true);
        while (flashImg.color.a < 1)
        {
            flashImg.color = new Vector4(color.r, color.g, color.b, flashImg.color.a +(Time.deltaTime/3));
            yield return null;
        }
    }
}
