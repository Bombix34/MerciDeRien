using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CSVParsing : ScriptableObject
{
	public TextAsset csvFile; // Reference of CSV file

    public PnjManager.CharacterType characterConcerned;


    public string assetName = "";

	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter

	
	// Read data from CSV file
	public void readData()
	{
		string[] records = csvFile.text.Split (lineSeperater);
        int dialogueNb = 0;
        int indexLine = 0;
        int indexField = 0;
        Dialogue curDialogue= new Dialogue();
		foreach (string record in records)
		{
            //CHAQUE LIGNE
            if (indexLine % 2 == 0)
            {
                dialogueNb++;
                string dialogueName = assetName+"_"+(dialogueNb).ToString();
                curDialogue = ScriptableObjectUtility.CreateAsset<Dialogue>(dialogueName);
                curDialogue.Init(characterConcerned);
            }
            string[] fields = record.Split(fieldSeperator);
            indexField = 0;
			foreach(string field in fields)
			{
                //CHAQUE CASE DU EXCEL
                if (indexField == 0)
                {
                    curDialogue.dialoguePriority =int.Parse(field);
                }
                else if(indexField==1)
                {
                    curDialogue.IsUniqueSentence = int.Parse(field) > 0;
                }
                else
                {
                    if (indexLine == 0 || indexLine % 2 == 0)
                    {//FRANCAIS
                        if (field != "" && field != " ")
                            curDialogue.frenchSentences.Add(field);
                    }
                    else
                    {//ANGLAIS
                        if (field != ""&&field!=" ")
                            curDialogue.englishSentences.Add(field);
                    }
                }
                indexField++;
			}
            indexLine++;
		}
	}


}