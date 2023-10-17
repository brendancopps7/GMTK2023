using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int ruleType = 0;
    public List<char> bonusLetters;
    public bool isPaused;
    public bool gameStarted = false;
    //0 for Equality
    //1 for Scrabble
    //2 for Keyboard distance
    
    public GameObject gameOverScreen;
    public GameObject ruleScreen;
    public GameObject pauseScreen;
    public GameObject mainGame;
    public WordSpawnerScript wordSpawnerScript;
    public SearchScript searchScript;

    private char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    // Start is called before the first frame update
    void Start()
    {

        int bonusLetter1;
        int bonusLetter2;
        int bonusLetter3;

        ruleType = Random.Range(0, 3);
        bonusLetter1 = Random.Range(0, 26);
        bonusLetter2 = Random.Range(0, 25);
        bonusLetter3 = Random.Range(0, 24);

        //Debug.Log("Original Rolls: " + bonusLetter1.ToString() + " " + bonusLetter2.ToString() + " " + bonusLetter3.ToString());
        if (bonusLetter2 >= bonusLetter1)
        {
            bonusLetter2++;
        }

        if(bonusLetter3 >= bonusLetter1)
        {
            bonusLetter3++;
        }
        if(bonusLetter3 >= bonusLetter2)
        {
            bonusLetter3++;
        }

        //Debug.Log("After Fix: " + bonusLetter1.ToString() + " " + bonusLetter2.ToString() + " " + bonusLetter3.ToString());

        bonusLetters.Add(alphabet[bonusLetter1]);
        bonusLetters.Add(alphabet[bonusLetter2]);
        bonusLetters.Add(alphabet[bonusLetter3]);

        showRules();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            togglePauseScreen(!isPaused);
        }
    }

    public int getMainRule()
    {
        return ruleType;
    }

    public List<char> getBonusLetters()
    {
        return bonusLetters;
    }

    public void winGame()
    {
        isPaused = true;
        mainGame.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void showRules()
    {
        isPaused = true;
        mainGame.SetActive(false);
        ruleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }
    public void pauseGame()
    {
        isPaused = true;
        mainGame.SetActive(false);
    }

    public void  restartGame()
    {
        //Need to restart the game while keeping the same rules
        searchScript.Reset();
        gameOverScreen.SetActive(false);
        wordSpawnerScript.Reset();

        //Reset learning progress


        startGame();
    }

    public void rerollRules()
    {
        //Since the rules are randomly generated, restarting the scene should reroll the rules.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        ruleScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        mainGame.SetActive(true);
        isPaused = false;
        gameStarted = true;

    }

    public void togglePauseScreen(bool pausing)
    {
        if (pausing)
        {
            pauseGame();
            pauseScreen.SetActive(true);
        }
        else
        {
            startGame();

        }
    }

    public void mainMenu()
    {
        //Main Menu has scene index 0
        SceneManager.LoadScene(0);
    }
}
