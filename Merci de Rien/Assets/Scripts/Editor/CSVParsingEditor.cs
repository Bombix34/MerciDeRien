using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CSVParsing))]

public class CSVParsingEditor : Editor
{
    CSVParsing parsing;
    SerializedObject GetTarget;
    SerializedProperty csvFile;
    SerializedProperty assetName;
    SerializedProperty characterConcerned;

    void OnEnable()
    {
        parsing = (CSVParsing)target;
        GetTarget = new SerializedObject(parsing);
        csvFile = GetTarget.FindProperty("csvFile");
        characterConcerned = GetTarget.FindProperty("characterConcerned");
        assetName = GetTarget.FindProperty("assetName");
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        EditorGUILayout.PropertyField(csvFile);

        if(parsing.csvFile!=null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Import settings",EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(assetName);

            AddPopup(ref characterConcerned, "Character :", typeof(PnjManager.CharacterType));

            if ((parsing.characterConcerned != PnjManager.CharacterType.none)&&(parsing.assetName!=""))
            {
                if (GUILayout.Button("IMPORT", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    parsing.readData();
                }
            }
        }

        //Apply the changes to our list
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
