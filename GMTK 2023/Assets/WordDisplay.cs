using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordDisplay : MonoBehaviour
{
    public Text text;
    public float fallspeed = .25f;
    public bool isPaused;
    
    public void SetWord(string word)
    {
        text.text = word;
    }
    
    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isPaused)
        {
            transform.Translate(0f, -fallspeed * Time.deltaTime, 0f);
        }
    } 

    public void setPaused(bool _isPaused)
    {
        isPaused = _isPaused;
    }
}
