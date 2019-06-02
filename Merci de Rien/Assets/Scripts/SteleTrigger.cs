using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteleTrigger : MonoBehaviour
{
    [SerializeField]
    Transform orbPosition;

    PlayerManager player;

    [SerializeField]
    GameObject triggerCameraOnStele;

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
                TriggerEffect(orb);
                player.ChangeState(new PlayerBaseState(player));
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

        GameManager.Instance.EndGame(orb);
    }

}
