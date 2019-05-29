using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteleTrigger : MonoBehaviour
{
    [SerializeField]
    Transform orbPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
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
        BringObject orbBringing = orb.GetComponent<BringObject>();
        orb.transform.parent = orbPosition;
        orb.transform.position = orbPosition.position;
        orbBringing.CanInteract = false;
        orb.GetComponent<Rigidbody>().useGravity = false;
        orb.GetComponent<Rigidbody>().mass = 1;
        orb.GetComponent<Rigidbody>().isKinematic = true;
       // orb.GetComponent<S>
        GetComponentInParent<Animator>().SetBool("Glow", true);
    }

}
