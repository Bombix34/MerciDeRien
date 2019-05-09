using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCreateVase : MonoBehaviour
{
    public GameObject vasePrefab;
    PlayerInputManager inputs;
    PlayerManager curPlayer;

    void Start()
    {
        inputs = GetComponent<PlayerInputManager>();
        curPlayer = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if(inputs.GetCancelInput()&&curPlayer.GetCurrentState().stateName== "PLAYER_BASE_STATE")
        {
            InstantiateVase();
        }
    }

    public void InstantiateVase()
    {
        GameObject vase = Instantiate(vasePrefab, transform.position, Quaternion.identity) as GameObject;

        if(curPlayer.GetNearInteractObject()!=null)
            curPlayer.GetNearInteractObject().GetComponent<InteractObject>().UpdateFeedback(false);

        curPlayer.ChangeState(new PlayerBringObjectState(curPlayer, vase));
        vase.GetComponent<InteractObject>().UpdateFeedback(false);
        curPlayer.SetNearInteractObject(null);
    }
}
