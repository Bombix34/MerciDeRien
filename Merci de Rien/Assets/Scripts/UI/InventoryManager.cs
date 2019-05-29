using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Image key01, key02, key03;

    [SerializeField]
    Color disabledColor;

    [SerializeField]
    Text shellsNbText;
    [SerializeField]
    GameObject shellsDisplay;

    void Start()
    {
        key01.color = disabledColor;
        key02.color = disabledColor;
        key03.color = disabledColor;

    }

    public void AddInventoryKey(int id)
    {
        if (id == 0)
        {
            key01.color = Color.white;
            key01.GetComponent<Animator>().SetTrigger("Unlock");
        }
        else if (id == 1)
        {
            key02.color = Color.white;
            key02.GetComponent<Animator>().SetTrigger("Unlock");
        }
        else if (id == 2)
        {
            key03.color = Color.white;
            key03.GetComponent<Animator>().SetTrigger("Unlock");
        }
    }

    public void UpdateShellsNb(int nb)
    {
        if (shellsDisplay.activeInHierarchy)
            shellsDisplay.SetActive(true);
        shellsNbText.text = nb.ToString();
        shellsNbText.GetComponent<Animator>().SetTrigger("Add");
    }
}
