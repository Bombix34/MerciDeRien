using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : InteractObject
{
    Animator animator;

    [SerializeField]
    Dialogue closedPortailText;

    [SerializeField]
    int keyNeeded = -1;

    BoxCollider collider;

    public bool IsOpen { get; set; } = false;
    
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        objectType = ObjectType.Portail;
        collider = GetComponent<BoxCollider>();
        CanTakeObject = true;
    }
    
    public override void StartInteraction()
    {
        base.StartInteraction();
        PlayerManager player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        if (IsOpen)
        {
            animator.SetBool("IsOpen", false);
            IsOpen = false;
        }
        else
        {
            bool hasKey = false;
            if (keyNeeded == -1)
                hasKey = true;
            else
                hasKey = GameManager.Instance.HasKey(keyNeeded);
            if(hasKey)
            {
                animator.SetBool("IsOpen", true);
                collider.isTrigger = true;
                IsOpen = true;
            }
            else
            {
                animator.SetTrigger("Closed");
            }
        }
        StartCoroutine(IsMovingCoroutine());
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        PlayerManager player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        PlayerTransitionState statePlayer = (PlayerTransitionState)player.GetCurrentState();
        if(statePlayer!=null)
        {
            statePlayer.ReturnBackToPrevState();
        }
    }

    public override void EndInteraction()
    {
    }

    public override string GetInteractText(bool isStealing)
    {
        string returnVal = "";
        SettingsManager settings = GameManager.Instance.settings;
        if (settings.currentLanguage == SettingsManager.Language.francais)
        {
            if (IsOpen)
                returnVal = closedPortailText.frenchSentences[0];
            else
                returnVal = interactText.frenchSentences[0];
        }
        else if (settings.currentLanguage == SettingsManager.Language.english)
        {
            if (IsOpen)
                returnVal = closedPortailText.englishSentences[0];
            else
                returnVal = interactText.englishSentences[0];
        }
        return returnVal;
    }

    IEnumerator IsMovingCoroutine()
    {
        CanInteract = false;
        yield return new WaitForSeconds(0.75f);
        CanInteract = true;
        if(!IsOpen)
            collider.isTrigger = false;
    }

    public void SetKeyNeeded(int val)
    {
        keyNeeded = val;
    }

    public int GetKeyNeeded()
    {
        return keyNeeded;
    }

    void PlayBlockingSFX()
    {
        AkSoundEngine.PostEvent("ENV_door_locked_play", gameObject);
    }

    void PlayOpeningSFX()
    {
        AkSoundEngine.PostEvent("ENV_door_open_play", gameObject);
        Debug.Log("Rappeler à Abdoul de remplacer le SFX placeholder d'ouverture du portail s'il a oublié");
    }

    void PlayClosingSFX()
    {
        //Pour l'instant y'a rien, c'est normal :9
        AkSoundEngine.PostEvent("ENV_door_close_play", gameObject);
        Debug.Log("Rappeler à Abdoul de faire un SFX pour la fermeture du portail s'il a oublié lel");
    }

}