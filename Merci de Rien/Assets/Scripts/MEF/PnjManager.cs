using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PnjDialogueManager))]
public class PnjManager : ObjectManager
{
    protected NavMeshAgent navAgent;
    Animator anim;

    [SerializeField]
    CharacterType character;

    [SerializeField]
    public Mood CurrentMood { get; set; } = Mood.neutral;

    public PnjDialogueManager dialogueManager{get;set;}

    protected InteractObject interactionManager;

    public bool IsWaitingObject { get; set; } = false;

    protected virtual void Awake()
    {
        dialogueManager = GetComponent<PnjDialogueManager>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        interactionManager = GetComponent<InteractObject>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        ChangeState(new WanderAroundState(this,this.transform.position));
        InitInteractScript();
    }

    protected virtual void Update()
    {
        currentState.Execute();
        UpdateAnim();
    }

    protected virtual void InitInteractScript()
    {
        interactionManager.CanTakeObject = true;
        interactionManager.CanInteract = true;
        interactionManager.characterOwner = character;
        interactionManager.objectType = InteractObject.ObjectType.PNJ;
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    protected virtual void OnDrawGizmos()
    {
        //TOOL DEBUG
        if (!Application.isPlaying)
            return;
        if (currentState.stateName == "WANDER_AROUND_STATE")
        {
            Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
            WanderAroundState curtState = (WanderAroundState)currentState;
            Gizmos.DrawSphere(curtState.InitPosition, curtState.Radius);
        }
    }

    //ANIM_______________________________________________________

    public Animator GetAnimator()
    {
        return anim;
    }

    public void UpdateAnim()
    {
        anim.SetFloat("MoveSpeed", (navAgent.velocity.magnitude / 0.1f)*0.03f);
    }

    //RAYCAST________________________________________________________

    public bool RaycastPlayer()
    {
        bool result = false;
        Vector3 testPosition = GetFrontPosition();

        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.55f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Player")
            {
                result = true;
                i = hitColliders.Length;
            }
            i++;
        }
        return result;
    }

    public Vector3 GetFrontPosition()
    {
        //FONCTION POUR OBTENIR LA POSITION DEVANT LE PERSONNAGE
        //POSITION OU INTERAGIR ET POSER LES OBJETS
        Vector3 forwardPos = transform.TransformDirection(Vector3.forward) * 0.5f;
        Vector3 testPosition = new Vector3(transform.position.x + forwardPos.x,
            transform.position.y + forwardPos.y,
            transform.position.z + forwardPos.z);
        return testPosition;
    }

    //EVENT_____________________________________________________________

    public void HurtingEvent()
    {
        anim.SetTrigger("Hit");
        EventManager.Instance.UpdateCharacterEvent(EventDatabase.EventType.violenceTotal, GetCharacterType(), 1);
    }

    public void TalkingEvent()
    {
        EventManager.Instance.UpdateCharacterEvent(EventDatabase.EventType.conversationTotal, GetCharacterType(), 1);
    }

    public bool SpecialObjectEvent(InteractObject obj)
    {
        if (!IsWaitingObject)
        {
            return false;
        }
        bool returnVal = false;
        switch (obj.objectType)
        {
            case InteractObject.ObjectType.Baton:
                if (character == PnjManager.CharacterType.Artisan)
                {
                    returnVal = true;
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectBoisToArtisan, 1);
                }
                break;
            case InteractObject.ObjectType.Plante:
                if (character == PnjManager.CharacterType.Healer)
                {
                    returnVal = true;
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectPlanteToShaman, 1);
                }
                break;
            case InteractObject.ObjectType.Fourche:
                if (character == PnjManager.CharacterType.Paysan)
                {
                    returnVal = true;
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectFourcheToPaysan, 1);
                }
                break;
            case InteractObject.ObjectType.Potion:
                if (character == PnjManager.CharacterType.Pecheur)
                {
                    returnVal = true;
                    EventManager.Instance.UpdateEvent(EventDatabase.EventType.ObjectPotionToPecheur, 1);
                }
                break;
            default:
                break;
        }
        if(returnVal)
        {
            //PLAY SONG "VICTORY"
        }
        return returnVal;
    }

    //AGENT_________________________________________________________

    public NavMeshAgent GetAgent()
    {
        return navAgent;
    }

    public InteractObject GetInteractionManager()
    {
        return interactionManager;
    }

    //SOUND_______________________________________________________
    
    public void PlayOnomatope()
    {
        switch(character)
        {
            case CharacterType.Artisan:
                if(CurrentMood==Mood.neutral)
                {
                    //artisan neutre 
                }
                else
                {
                    //artisan agressif
                }
                break;
            case CharacterType.Paysan:
                if (CurrentMood == Mood.neutral)
                {
                    //paysan neutre
                }
                else
                {
                    //paysan agressif
                }
                break;
            case CharacterType.Pecheur:
                if (CurrentMood == Mood.neutral)
                {
                    //pecheur neutre
                }
                else
                {
                    //pecheur agressif
                }
                break;
            case CharacterType.Responsable:
                if (CurrentMood == Mood.neutral)
                {
                    //responsable neutre
                }
                else
                {
                    //responsable agressif
                }
                break;
            case CharacterType.Healer:
                if (CurrentMood == Mood.neutral)
                {
                    //healer neutre
                }
                else
                {
                    //healer agressif
                }
                break;
            case CharacterType.Troubadour:
                if (CurrentMood == Mood.neutral)
                {
                    //troubadour neutre
                }
                else
                {
                    //troubadour agressif
                }
                break;
            case CharacterType.Etranger:
                if (CurrentMood == Mood.neutral)
                {
                    //etranger neutre 
                }
                else
                {
                    //etranger agressif
                    //ATTENTION: l'étranger ne passe jamais en agressif
                }
                break;
        }
    }


    //CHARACTER____________________________________________________

    public CharacterType GetCharacterType()
    {
        return character;
    }

    public enum CharacterType
    {
       Artisan,
       Paysan,
       Pecheur,
       Responsable,
       Healer,
       Troubadour,
       Etranger,
       none
    }

    public enum Mood
    {
        neutral,
        aggressive
    }
}
