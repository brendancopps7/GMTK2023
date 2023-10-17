using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRuleExplainer : MonoBehaviour
{
    public GameManager gameManager;
    bool hasMainRule = false;
    // Start is called before the first frame update
    void Update()
    {
        if (!hasMainRule)
        {
            setRule(gameManager.getMainRule());
            hasMainRule = true;
        }
    }

    // Update is called once per frame
    private void setRule(int ruleId)
    {
        if(ruleId == 1)
        {
            GetComponent<Text>().text = "Scrabble Rule: \n Letters with higher scrabble scores take longer to type. \n Karen gets a small typing boost on weaker letters.";
        }
        else if(ruleId == 2)
        {
            GetComponent<Text>().text = "QWERTY Distance Rule: \n Letters that are further apart on a QWERTY keyboard take longer to type. \n Karen gets a small typing boost on clustered letters";
        }
        else
        {
            GetComponent<Text>().text = "Letter Equality Rule: \n All letters take the same time to type.";
        }
    }
}
