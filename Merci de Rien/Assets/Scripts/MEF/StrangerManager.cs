using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerManager : PnjManager
{
    [SerializeField]
    GameObject model;

    [SerializeField]
    Renderer render;


    [SerializeField]
    Material baseMaterial;
    [SerializeField]
    Material dissolveMaterial;

    //DEBUG
    PlayerInputManager playerInput;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        InitInteractScript();
        ChangeState(new StrangerWaitState(this));
        playerInput = EventManager.Instance.GetPlayer().GetComponent<PlayerInputManager>();
    }

    protected override void InitInteractScript()
    {
        base.InitInteractScript();
        interactionManager.CanInteract = false;
    }

    protected override void Update()
    {
        base.Update();

        //DEBUG
        if(playerInput.GetCancelInput())
        {
            ChangeState(new StrangerApparitionState(this));
        }
    }

    public override void ChangeState(State newState)
    {
        Debug.Log(newState.stateName);
        if (newState.stateName == "WANDER_AROUND_STATE")
            newState = new StrangerWaitState(this);
        base.ChangeState(newState);
    }

    public void Enable(bool isOn)
    {
        if (model != null)
            model.SetActive(isOn);
    }

    public Material GetMaterial(bool isBase)
    {
        if (isBase)
            return baseMaterial;
        else
            return dissolveMaterial;
    }

    public Renderer GetRenderer()
    {
        return render;
    }
}
