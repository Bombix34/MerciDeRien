using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public SettingsManager settings;

    public EventManager EventManager{ set; get; }

    [SerializeField]
    List<GameObject> patounePrefab;

    public Dialogue PLACEHOLDER_PNJ_DIALOGUE;

    List<Dialogue> playerHistoricDialogues;

    int patounes  = 0;
    List<int> keyIDs;

    InterestPointManager placeManager;

    GameObject uiMainGame;

    private void Start()
    {
        keyIDs = new List<int>();
        EventManager = GetComponent<EventManager>();
        playerHistoricDialogues = new List<Dialogue>();
        placeManager = GetComponent<InterestPointManager>();
        uiMainGame = DialogueUiManager.Instance.gameObject;
        Cursor.visible = false;
    }

    public void EndGame(GameObject orb)
    {
        StartCoroutine(End(orb));
    }

    IEnumerator End(GameObject orb)
    {
        FlashFXManager.Instance.Flash(Color.white);
        CameraManager cameraManager = Camera.main.GetComponent<CameraManager>(); 
        cameraManager.SetNewCamera(CameraManager.CameraType.Dezoom);
        yield return new WaitForSeconds(3f);
        AkSoundEngine.PostEvent("GAME_end", gameObject);
        cameraManager.SetDialogueCamera(orb);
        FlashFXManager.Instance.Fondu(Color.black);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Ceremonie");
    }

    public void AddToHistoric(Dialogue newDialogue)
    {
        foreach (var item in playerHistoricDialogues)
        {
            if (item == newDialogue)
                return;
        }
        playerHistoricDialogues.Add(newDialogue);
    }

    public bool IsDialogue(Dialogue concerned)
    {
        bool result = false;
        foreach (var item in playerHistoricDialogues)
        {
            if (item == concerned)
                result = true;
        }
        return result;
    }

    public bool HasKey(int id)
    {
        bool result = false;
        foreach (var item in keyIDs)
        {
            if (id == item)
                result = true;
        }
        return result;
    }

    public void AddKey(int id)
    {
        keyIDs.Add(id);
        uiMainGame.GetComponent<InventoryManager>().AddInventoryKey(id);
    }

    public void RemoveKey(int id)
    {
        if(HasKey(id))
            keyIDs.Remove(id);
    }

    public void AddPatoune(int nb)
    {
        patounes += nb;
        uiMainGame.GetComponent<InventoryManager>().UpdateShellsNb(patounes);
    }

    public void RemovePatoune(int nb)
    {
        patounes -= nb;
        uiMainGame.GetComponent<InventoryManager>().UpdateShellsNb(patounes);
    }

    public GameObject GetPatounePrefab()
    {
        return patounePrefab[(int)Random.Range(0f, patounePrefab.Count)];
    }

    public InterestPointManager GetInterestPointManager()
    {
        return placeManager;
    }
}
