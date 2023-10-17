using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class SearchScript : MonoBehaviour
{
    //Assumes that I have a game object for each item in my dictionary -- need to pull words from the dictionary and then fill the gameObject
    public GameObject contentHolder; //GameObject that holds all the visible entries. Set in unity
    public TMP_InputField SearchBar; //GameObject that is typable and holds the search. Set in unity
    public int numVisibleWords = 5;
    public GameObject[] visibleEntries; //List of the buttons that hold the dictionary entries. Set in start
    
    public WordSpawnerScript wordSpawner;
    public autoTyper typer;

    public int totalElements;
    private static string[] wordList = File.ReadAllLines(Application.streamingAssetsPath + "/Dictionary.csv"); //File.ReadAllLines("Assets/Dictionary.csv");


    // Start is called before the first frame update
    void Start()
    {
        SearchBar.Select();

        visibleEntries = new GameObject[numVisibleWords];
        
        for(int i = 0; i < numVisibleWords; i++)
        {
            visibleEntries[i] = contentHolder.transform.GetChild(i).gameObject;
        }

        clearEntries();
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendWord(0);
        }
    }

    private void clearEntries()
    {
        foreach (GameObject entry in visibleEntries)
        {
            //Set button text
            entry.transform.GetChild(0).GetComponent<Text>().text = "";
            //Set point value text
            entry.transform.GetChild(1).GetComponent<Text>().text = "";
        }
    }

   public void Search()
    {
        string searchText = SearchBar.GetComponent<TMP_InputField>().text;
        int searchTxtLen = searchText.Length;
        int foundElements = 0;
        int wordPointScore = 0;
        int millisecondsToType = 0;
        float secondsToType = 0;

        clearEntries();

        foreach (string dictionaryWord in wordList)
        {
            if(foundElements == visibleEntries.Length)
            {
                break;
            }
            if(dictionaryWord.Length >= searchTxtLen)
            {
                if (searchText.ToLower() == dictionaryWord.Substring(0, searchTxtLen).ToLower())
                {
                    visibleEntries[foundElements].transform.GetChild(0).GetComponent<Text>().text = dictionaryWord.ToLower();
                    wordPointScore = wordSpawner.getWordPoints(dictionaryWord.ToLower());
                    millisecondsToType = (int) (1000f * wordPointScore / typer.getPointsPerSecond());
                    secondsToType = ((int) millisecondsToType) / 1000;
                    visibleEntries[foundElements].transform.GetChild(1).GetComponent<Text>().text = secondsToType.ToString() + " Seconds";
                    foundElements++;
                }
            }
        }



    }

    public void SendWord(int id)
    {
        SearchBar.ActivateInputField();
        SearchBar.Select();
        string wordToSend = visibleEntries[id].transform.GetChild(0).GetComponent<Text>().text;
        wordSpawner.addWordToQueue(wordToSend);
        
        foreach (GameObject entry in visibleEntries)
        {
            wordSpawner.addWordToSimilarWords(entry.transform.GetChild(0).GetComponent<Text>().text);
        }

        Reset();
    }

    public void Reset()
    {
        SearchBar.GetComponent<TMP_InputField>().text = "";
        clearEntries();
    }
}
