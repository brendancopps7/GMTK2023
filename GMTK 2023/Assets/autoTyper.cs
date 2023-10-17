using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoTyper : MonoBehaviour
{
    public WordSpawnerScript wordSpawnerScript;
    public float pointsPerSecond;
    public float difficultyWeight;
    public float timer = 0f;
    bool hasDifficultyWeight = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDifficultyWeight)
        {
            difficultyWeight = wordSpawnerScript.getWordPoints("abcdefghijklmnopqrstuvwxyz") / 26f;
            hasDifficultyWeight = true;
        }

        timer += Time.deltaTime;
        if(timer > 1f / (difficultyWeight * pointsPerSecond))
        {
            wordSpawnerScript.tickPoint();
            timer = 0;
        }
    }

    public float getPointsPerSecond()
    {
        return difficultyWeight * pointsPerSecond;
    }
}
