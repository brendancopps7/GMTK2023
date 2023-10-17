using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex = 0;
    private bool isPaused = false;

    private WordDisplay display;

    public Word(string _word, WordDisplay _display)
    {
        word = _word;
        display = _display;
        typeIndex = 0;
        display.SetWord(word);
    }

    public char getNextLetter()
    {
        return word[typeIndex];
    }

    public void typeLetter()
    {
        typeIndex++;
        display.RemoveLetter();
    }

    public bool isComplete()
    {
        bool wordIsComplete = typeIndex >= word.Length;
        if(wordIsComplete)
        {
            display.RemoveWord();
        }
        return wordIsComplete;
    }

    public void setPaused(bool _isPaused)
    {
        isPaused = _isPaused;
        display.setPaused(_isPaused);
    }

    public void Reset()
    {
        display.RemoveWord();
    }
}
