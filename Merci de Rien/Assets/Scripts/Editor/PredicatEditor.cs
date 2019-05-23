using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Predicat))]
public class PredicatEditor : Editor
{
    Predicat predicat;
    SerializedObject GetTarget;
    SerializedProperty conditionList;
    SerializedProperty consequenceList;
    int conditionSize;
    int consequenceSize;

    EventDatabase.EventType test;

    void OnEnable()
    {
        predicat = (Predicat)target;
        GetTarget = new SerializedObject(predicat);
        conditionList = GetTarget.FindProperty("conditions"); 
        consequenceList = GetTarget.FindProperty("consequences");
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //CONDITION_____________________________________________________________________________________________
        EditorGUILayout.LabelField("CONDITION(S)", EditorStyles.boldLabel);

        if (GUILayout.Button("Add new condition"))
        {
            if (predicat.conditions == null)
                predicat.conditions = new List<Condition>();
            predicat.conditions.Add(new Condition());
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        for (int i = 0; i < conditionList.arraySize; i++)
        {
            SerializedProperty conditionListRef = conditionList.GetArrayElementAtIndex(i);
            SerializedProperty eventTypeRef = conditionListRef.FindPropertyRelative("concernedEvent");
            SerializedProperty conditionTypeRef = conditionListRef.FindPropertyRelative("comparaison");
            SerializedProperty valToTestRef = conditionListRef.FindPropertyRelative("valToTest");
            SerializedProperty comparisonCumulRef = conditionListRef.FindPropertyRelative("comparisonCumul");

            AddPopup(ref eventTypeRef, "Event type :", typeof(EventDatabase.EventType));

            if (eventTypeRef.enumValueIndex == (int)EventDatabase.EventType.DialogueHasBeenSaid)
            {
                SerializedProperty dialogueConcernedRef = conditionListRef.FindPropertyRelative("concernedDialogue");
                dialogueConcernedRef.objectReferenceValue = EditorGUILayout.ObjectField("Dialogue à tester :", dialogueConcernedRef.objectReferenceValue, typeof(Dialogue), true);
            }

            AddPopup(ref conditionTypeRef, "Comparaison symbol :", typeof(Condition.ComparisonType));
            valToTestRef.intValue = EditorGUILayout.IntField("Value to test :", valToTestRef.intValue);


            if (eventTypeRef.enumValueIndex == (int)EventDatabase.EventType.DialogueHasBeenSaid)
            {
                if (conditionTypeRef.enumValueIndex != (int)Condition.ComparisonType.equal)
                    conditionTypeRef.enumValueIndex = (int)Condition.ComparisonType.equal;
                if(valToTestRef.intValue==0)
                    EditorGUILayout.LabelField("Le dialogue n'a jamais été dit",EditorStyles.boldLabel);
                else
                    EditorGUILayout.LabelField("Le dialogue a déjà été dit", EditorStyles.boldLabel);
            }
            AddPopup(ref comparisonCumulRef, "Comparaison type :", typeof(Condition.MultipleComparison));

            EditorGUILayout.LabelField("Remove a condition");
            if (GUILayout.Button("Remove condition (" + i.ToString() + ")"))
            {
                conditionList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //CONSEQUENCE_______________________________________________________________________________________
        EditorGUILayout.LabelField("CONSEQUENCE(S)", EditorStyles.boldLabel);
        if (GUILayout.Button("Add new consequence"))
        {
            if (predicat.consequences == null)
                predicat.consequences = new List<Consequence>();
            predicat.consequences.Add(new Consequence());
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        for (int j = 0; j < consequenceList.arraySize; j++)
        {
            SerializedProperty consequenceListRef = consequenceList.GetArrayElementAtIndex(j);
            SerializedProperty consequenceTypeRef = consequenceListRef.FindPropertyRelative("consequence");

            //PNJ BEHAVIOR
            SerializedProperty characterTypeRef = consequenceListRef.FindPropertyRelative("characterConcerned");
            SerializedProperty actionChoiceRef = consequenceListRef.FindPropertyRelative("actionChoice");

            //PNJ MOOD
            SerializedProperty characterMoodRef = consequenceListRef.FindPropertyRelative("mood");

            //DIALOGUE
            SerializedProperty dialogueChoiceRef = consequenceListRef.FindPropertyRelative("dialogueConcerned");

            //EVENT DATABASE
            SerializedProperty databaseRef = consequenceListRef.FindPropertyRelative("eventDatabase");
            SerializedProperty eventTypeRef = consequenceListRef.FindPropertyRelative("eventType");

            //INTERACTIVE OBJECTS
            SerializedProperty interactObjectRef = consequenceListRef.FindPropertyRelative("objectConcerned");

            //GENERAL (INT,FLOAT...)
            SerializedProperty intValRef = consequenceListRef.FindPropertyRelative("intModificator");

            AddPopup(ref consequenceTypeRef, "Consequence type :", typeof(Consequence.ConsequenceType));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Réglages conséquence _________");
            switch (consequenceTypeRef.enumValueIndex)
            {
                case (int)Consequence.ConsequenceType.PnjChangeBehavior:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    AddPopup(ref actionChoiceRef, "Action du pnj :", typeof(Consequence.CharacterAction));
                    break;
                case (int)Consequence.ConsequenceType.PnjChangeMood:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    AddPopup(ref characterMoodRef, "Nouveau mood :", typeof(PnjManager.Mood));
                    break;
                case (int)Consequence.ConsequenceType.AddDialogue:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    dialogueChoiceRef.objectReferenceValue = EditorGUILayout.ObjectField("Dialogue à ajouter :", dialogueChoiceRef.objectReferenceValue, typeof(Dialogue), true);
                    break;
                case (int)Consequence.ConsequenceType.RemoveDialogue:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    dialogueChoiceRef.objectReferenceValue = EditorGUILayout.ObjectField("Dialogue à enlever :", dialogueChoiceRef.objectReferenceValue, typeof(Dialogue), true);
                    break;
                case (int)Consequence.ConsequenceType.AutorisationTakeObject:
                case (int)Consequence.ConsequenceType.RemoveAutorisationTakeObject:
                case (int)Consequence.ConsequenceType.AutorisationInteractionObject:
                case (int)Consequence.ConsequenceType.RemoveAutorisationInteractionObject:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    AddPopup(ref interactObjectRef, "Objet concerné :", typeof(InteractObject.ObjectType));
                    break;
                case (int)Consequence.ConsequenceType.GainKey:
                case (int)Consequence.ConsequenceType.RemoveKey:
                    intValRef.intValue = EditorGUILayout.IntField("Key ID :", intValRef.intValue);
                    break;
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Remove a consequence");
            if (GUILayout.Button("Remove consequence (" + j.ToString() + ")"))
            {
                consequenceList.DeleteArrayElementAtIndex(j);
            }
            EditorGUILayout.Space();
        }

        GetTarget.ApplyModifiedProperties();
        
      // consequenceList.
    }

    void AddPopup(ref SerializedProperty ourSerializedProperty, string nameOfLabel, System.Type typeOfEnum)
    {
        //ENUM POPUP

        Rect ourRect = EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginProperty(ourRect, GUIContent.none, ourSerializedProperty);
        EditorGUI.BeginChangeCheck();

        int actualSelected = 1;
        int selectionFromInspector = ourSerializedProperty.intValue;
        string[] enumNamesList = System.Enum.GetNames(typeOfEnum);
        actualSelected = EditorGUILayout.Popup(nameOfLabel, selectionFromInspector, enumNamesList);
        ourSerializedProperty.intValue = actualSelected;

        EditorGUI.EndProperty();
        EditorGUILayout.EndHorizontal();
    }

}
