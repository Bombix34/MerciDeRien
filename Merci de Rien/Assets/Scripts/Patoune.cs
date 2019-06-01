using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patoune : MonoBehaviour
{

    private void Start()
    {
        AkSoundEngine.PostEvent("ENV_spawn_patoune_play", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameManager.Instance.AddPatoune(1);
            AkSoundEngine.PostEvent("ENV_spawn_patoune_play", gameObject);
            //other.gameObject.GetComponent<PlayerSoundManager>().PlayInteractFeedbackSound(this.gameObject);

            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
