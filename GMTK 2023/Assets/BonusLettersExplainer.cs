using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusLettersExplainer : MonoBehaviour
{
    public GameManager gameManager;
    bool hasBonusLetters = false;
    
    void Update()
    {
        if (!hasBonusLetters)
        {
            setRule(gameManager.getBonusLetters());
            hasBonusLetters = true;
        }
    }

    private void setRule(List<char> bonusLetters)
    {

        GetComponent<Text>().text = "Bonus Letters: " + bonusLetters[0].ToString().ToUpper() + ", " + bonusLetters[1].ToString().ToUpper() + ", and " + bonusLetters[2].ToString().ToUpper() + "\n " +
                "Karen struggles with letters " + bonusLetters[0].ToString().ToUpper() + ", " + bonusLetters[1].ToString().ToUpper() + ", and " + bonusLetters[2].ToString().ToUpper() + ".\n" +
                "These letters will take slightly longer for her to type.";
       
    }
}
