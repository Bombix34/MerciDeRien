﻿using UnityEngine;
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
            AddPopup(ref conditionTypeRef, "Comparaison symbol :", typeof(Condition.ComparisonType));
            valToTestRef.intValue = EditorGUILayout.IntField("Value to test :", valToTestRef.intValue);
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
            predicat.consequences.Add(new Consequence());
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        for (int j = 0; j < consequenceList.arraySize; j++)
        {
            SerializedProperty consequenceListRef = consequenceList.GetArrayElementAtIndex(j);
            SerializedProperty consequenceTypeRef = consequenceListRef.FindPropertyRelative("consequence");

            //PNJG BEHAVIOR
            SerializedProperty characterTypeRef = consequenceListRef.FindPropertyRelative("characterConcerned");
            SerializedProperty actionChoiceRef = consequenceListRef.FindPropertyRelative("actionChoice");

            AddPopup(ref consequenceTypeRef, "Consequence type :", typeof(Consequence.ConsequenceType));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Réglages conséquence _________");
            switch (consequenceTypeRef.enumValueIndex)
            {
                case (int)Consequence.ConsequenceType.PnjChangeBehavior:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
                    AddPopup(ref actionChoiceRef, "Action du pnj :", typeof(Consequence.CharacterAction));
                    break;
                case (int)Consequence.ConsequenceType.AddDialogue:
                    AddPopup(ref characterTypeRef, "Character concerné :", typeof(PnjManager.CharacterType));
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
