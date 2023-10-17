using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    //private static string[] wordList = { "alphabet", "banana", "cheese", "dog", "element" };
    private static string[] wordList = File.ReadAllLines("Assets/Dictionary.csv");

    private void Start()
    {
        //wordList = File.ReadAllLines("Assets/Dictionary.csv");
    }
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        return wordList[randomIndex];
    }
}
