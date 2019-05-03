using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Consequence)), CanEditMultipleObjects]
public class ConsequenceEditor : Editor
{
    Consequence concern;

    SerializedProperty consequenceProp;

    SerializedProperty pnjConcernedProp, pnjActionProp;


    private void OnEnable()
    {
        concern = target as Consequence;

       consequenceProp = serializedObject.FindProperty("consequence");

        pnjConcernedProp = serializedObject.FindProperty("characterConcerned");
        pnjActionProp = serializedObject.FindProperty("actionChoice");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Type de conséquence", EditorStyles.boldLabel);
        concern.consequence = (Consequence.ConsequenceType)EditorGUILayout.EnumPopup(concern.consequence);

        switch(concern.consequence)
        {
            case Consequence.ConsequenceType.PnjChangeBehavior:
                DisplayPnjChangeBehavior();
                break;
            case Consequence.ConsequenceType.AddDialogue:
                break;
        }
    }

    public void DisplayPnjChangeBehavior()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("PNJ concerné :", EditorStyles.boldLabel);
        concern.characterConcerned = (PnjManager.CharacterType)EditorGUILayout.EnumPopup(concern.characterConcerned);
        EditorGUILayout.LabelField("Action du PNJ :", EditorStyles.boldLabel);
        concern.actionChoice = (Consequence.CharacterAction)EditorGUILayout.EnumPopup(concern.actionChoice);
    }
}
