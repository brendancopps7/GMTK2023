using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSpawnerScript : MonoBehaviour
{
    //public GameObject FallingWordGameObject;
    // Start is called before the first frame update

    public List<Word> words;
    public WordSpawner wordSpawner;
    public GameManager gameManager;
    public Text errorText;
    public Text ruleText;
    public Text bonusText;

    private bool hasActiveWord = false;
    private Word activeWord;

    private char targetLetter;
    private char previousLetter;
    private bool hasTargetLetter = false;
    private bool hasPreviousLetter = false;
    public int remainingLetterPoints = 0;

    public int ruleType;
    public List<char> bonusLetters;
    public float timeBetweenWords = 2f;
    public List<string> previousWords;
    public List<string> wordQueue;
    public List<string> similarWords;
    private float timeSinceLastWord = 0;


    private static Dictionary<char, int> scrabblePointValues = new Dictionary<char, int>(){
        {'a', 1}, {'e', 1}, {'i', 1}, {'o', 1}, {'u', 1},
        {'l', 1}, {'n', 1}, {'r', 1}, {'s', 1}, {'t', 1},
        {'d', 2}, {'g', 2},
        {'b', 3}, {'c', 3}, {'m', 3}, {'p', 3},
        {'f', 4}, {'h', 4}, {'v', 4}, {'w', 4}, {'y', 4},
        {'k', 5},
        {'j', 8}, {'x', 8},
        {'q',10}, {'z',10},
    };

    private static Dictionary<char, int> distancePointValuesRow = new Dictionary<char, int>(){
        {'a', 1}, {'e', 1}, {'i', 1}, {'o', 1}, {'u', 1},
        {'l', 1}, {'n', 1}, {'r', 1}, {'s', 1}, {'t', 1},
        {'d', 1}, {'g', 1},
        {'b', 1}, {'c', 1}, {'m', 1}, {'p', 1},
        {'f', 1}, {'h', 1}, {'v', 1}, {'w', 1}, {'y', 1},
        {'k', 1},
        {'j', 1}, {'x', 1},
        {'q', 1}, {'z', 1},
    };

    private static Dictionary<char, int> distancePointValuesColumn = new Dictionary<char, int>(){
        {'a', 1}, {'e', 3}, {'i', 8}, {'o', 9}, {'u', 7},
        {'l', 9}, {'n', 6}, {'r', 4}, {'s', 2}, {'t', 5},
        {'d', 3}, {'g', 5},
        {'b', 5}, {'c', 3}, {'m', 7}, {'p',10},
        {'f', 4}, {'h', 6}, {'v', 4}, {'w', 2}, {'y', 6},
        {'k', 8},
        {'j', 7}, {'x', 2},
        {'q', 1}, {'z', 1},
    };

    private Dictionary<char, int> bonusPointValues = new Dictionary<char, int>(){
        {'a', 2}, {'e', 1}, {'i', 1}, {'o', 1}, {'u', 1},
        {'l', 2}, {'n', 3}, {'r', 1}, {'s', 2}, {'t', 1},
        {'d', 2}, {'g', 2},
        {'b', 3}, {'c', 3}, {'m', 3}, {'p', 1},
        {'f', 2}, {'h', 2}, {'v', 3}, {'w', 1}, {'y', 1},
        {'k', 2},
        {'j', 2}, {'x', 3},
        {'q', 1}, {'z', 3},
    };

    

    void Start()
    {
        ruleType = gameManager.getMainRule();
        bonusLetters = gameManager.getBonusLetters();
        
        ruleText.text = getRuleText(ruleType);
        bonusText.text = getBonusText(bonusLetters);

        errorText.gameObject.SetActive(false);

    }

    private void Update()
    {
        foreach (Word word in words)
        {
            word.setPaused(gameManager.isPaused);
        }

        if (!gameManager.isPaused)
        {
            timeSinceLastWord += Time.deltaTime;
        }

        if(timeSinceLastWord >= timeBetweenWords && wordQueue.Count >= 1)
        {
            addWord(wordQueue[0]);
            
        }
    }

    public void addWord(string _word)
    {
        Word word = new Word(_word, wordSpawner.SpawnWord());
        wordQueue.Remove(_word);
        timeSinceLastWord = 0;
        Debug.Log(word.word);   
        words.Add(word);
    }

    //True/False return to see if adding to queue was successful
    public void addWordToQueue(string _word)
    {
        if (_word == "")
        {
            //Empty Word Error
            errorMessage(0);
            return;
        }
        if (previousWords.Contains(_word))
        {
            //Previously Used Word Error;
            errorMessage(1);
            return;
        }
        if (similarWords.Contains(_word))
        {
            //Similar word Error
            errorMessage(2);
            return;
        }
        //errorText.gameObject.SetActive(false);
        wordQueue.Add(_word);
        previousWords.Add(_word);
    }

    public void addWordToSimilarWords(string _word)
    {
        if (!similarWords.Contains(_word))
        {
            similarWords.Add(_word);
        }
    }

    private void errorMessage(int errorId)
    {
        if(errorId == 0)
        {
            errorText.text = "You haven't typed a valid word.";
        }
        else if (errorId == 1)
        {
            errorText.text = "You can't reuse a word you've already used this round.";
        }
        else
        {
            errorText.text = "That word is too similar to a word you've already used this round.";
        }
        errorText.gameObject.SetActive(true);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.getNextLetter() == letter)
            {
                activeWord.typeLetter();
            }
        } else
        {

            foreach (Word word in words)
            {
                if (word.getNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    activeWord.typeLetter();
                    break; 
                }
            }
                
        }
        if (hasActiveWord && activeWord.isComplete())
        { 
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

    public void tickPoint()
    {
        //Debug.Log("TICK");
        Debug.Log(remainingLetterPoints);
        //Debug.Log(remainingLetterPoints > 0);
        if (remainingLetterPoints > 0)
        { 
            remainingLetterPoints--;
        }
        if (remainingLetterPoints <= 0 && hasTargetLetter)
        {
            TypeLetter(targetLetter);
            hasTargetLetter = false;
        }
        if (words.Count >= 1 && !hasTargetLetter)
        {
            targetLetter = words[0].getNextLetter();
            hasTargetLetter = true;
            remainingLetterPoints = getLetterPoints(targetLetter);
        }

        
    }

    public int getWordPoints(string word)
    {
        int Total = 0;
        foreach (char letter in word)
        {
            Total += getLetterPoints(letter);
        }
        return Total;
    }

    int getLetterPoints(char letter)
    {
        int bonusFactor = 0;
        if(letter == bonusLetters[0] || letter == bonusLetters[1] || letter == bonusLetters[2])
        {
            bonusFactor = 3;
        }
        if(ruleType == 1)
        {
            return(bonusFactor + getScrabbleLetterPoint(letter));
        }
        if(ruleType == 2)
        {
            return (bonusFactor + getKeyboardDistancePoint(letter));
        }
        //if we haven't hit anything, assume equality
        return bonusFactor + 1;
    }

    int getScrabbleLetterPoint(char letter)
    {
        return scrabblePointValues[letter];
    }

    int getKeyboardDistancePoint(char letter)
    {
        int distance = 0;
        if(hasPreviousLetter == false)
        {
            distance = 1;
            hasPreviousLetter = true;
        }
        else
        {
            Vector2Int oldCoord = new Vector2Int(distancePointValuesRow[previousLetter], distancePointValuesColumn[previousLetter]);
            Vector2Int newCoord = new Vector2Int(distancePointValuesRow[letter], distancePointValuesColumn[letter]);
            distance = (int) Vector2Int.Distance(oldCoord, newCoord);
        }

        previousLetter = letter;
        return distance;

    }

    public void Reset()
    {
        hasActiveWord = false;
        hasTargetLetter = false;
        hasPreviousLetter = false;
        remainingLetterPoints = 0;

        foreach(Word word in words)
        {
            word.Reset();
        }

        words.Clear();

        previousWords.Clear();
        similarWords.Clear();
        wordQueue.Clear();
        timeSinceLastWord = 0;
    }

    private string getRuleText(int ruleId)
    {
        if (ruleId == 1)
        {
            return "Scrabble Rule";
        }
        else if (ruleId == 2)
        {
            return "QWERTY Distance Rule";
        }
        else
        {
            return "Letter Equality Rule";
            
        }
    }

    private string getBonusText(List<char> bonusLetters)
    {
        return "Bonus Letters: \n" + bonusLetters[0] + ", " + bonusLetters[1] + ", and " + bonusLetters[2];
    }
}
