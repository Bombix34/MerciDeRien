using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patoune : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameManager.Instance.Patoune++;
            //JOUER LE SON DES PATOUNES ICI
            other.gameObject.GetComponent<PlayerSoundManager>().PlayInteractFeedbackSound(this.gameObject);
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
