using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MDR/new dialogue")]
public class Dialogue : ScriptableObject
{
    public PnjManager.CharacterType dialogueOwner;

    public int dialoguePriority = 0;
    public bool IsUniqueSentence = false;

    public MoodRequired moodRequired = MoodRequired.none;

    [SerializeField]
    List<Predicat> eventsTriggered;

    [TextArea(3,10)]
    public List<string> frenchSentences;
    [TextArea(3, 10)]
    public List<string> englishSentences;

    public void Init(PnjManager.CharacterType owner)
    {
        dialogueOwner = owner;
        frenchSentences = new List<string>();
        englishSentences = new List<string>();
    }

    public List<Predicat> GetPredicats()
    {
        return eventsTriggered;
    }

    public enum MoodRequired
    {
        none,
        neutral,
        agressive
    }
}
