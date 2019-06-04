using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteleTrigger : MonoBehaviour
{
    [SerializeField]
    Transform orbPosition;

    PlayerManager player;

    [SerializeField]
    GameObject triggerCameraOnStele;

    [SerializeField]
    GameObject ceremonieParticle;

    private void Awake()
    {
        ceremonieParticle.SetActive(false);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ceremonie")
        {
            AkSoundEngine.PostEvent("GAME_festival", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            player = other.GetComponent<PlayerManager>();
            if(player.IsBringingObject()!=null && player.IsBringingObject().objectType==InteractObject.ObjectType.Orb)
            {
                GameObject orb = player.IsBringingObject().gameObject;
                PlayerBringObjectState statePlayer = (PlayerBringObjectState)player.GetCurrentState();
                statePlayer.TryPoseObject();
                player.ChangeState(new PlayerWaitState(player, new PlayerBaseState(player)));
                player.Move(false);
                TriggerEffect(orb);
            }
        }
    }

    public void TriggerEffect(GameObject orb)
    {
        triggerCameraOnStele.SetActive(false);
        BringObject orbBringing = orb.GetComponent<BringObject>();
        orb.transform.parent = orbPosition;
        orb.transform.position = orbPosition.position;
        orbBringing.CanInteract = false;
        orb.GetComponent<Rigidbody>().useGravity = false;
        orb.GetComponent<Rigidbody>().mass = 1;
        orb.GetComponent<Rigidbody>().isKinematic = true;
       // orb.GetComponent<S>
        GetComponentInParent<Animator>().SetBool("Glow", true);
        AkSoundEngine.PostEvent("ENV_orb_activated_play", gameObject);
        ceremonieParticle.SetActive(true);
        GameManager.Instance.EndGame(orb);
    }

}
