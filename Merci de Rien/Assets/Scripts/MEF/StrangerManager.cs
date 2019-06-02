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

    [SerializeField]
    GameObject hairParticles;
    [SerializeField]
    GameObject apparitionParticles;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        InitInteractScript();
        ChangeState(new StrangerWaitState(this));
        ActiveParticle(false);
        ActiveHairParticle(false);
    }

    protected override void InitInteractScript()
    {
        base.InitInteractScript();
        interactionManager.CanInteract = false;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ChangeState(State newState)
    {
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

    public void ActiveParticle(bool isOn)
    {
        apparitionParticles.SetActive(isOn);
    }

    public void ActiveHairParticle(bool isOn)
    {
        if (isOn && hairParticles.activeInHierarchy == false)
            hairParticles.SetActive(true);
        if (!isOn)
            hairParticles.GetComponent<ParticleSystem>().Stop();
        else
            hairParticles.GetComponent<ParticleSystem>().Play();
    }

    public void DisableHairParticle()
    {
        hairParticles.SetActive(false);
    }
}
