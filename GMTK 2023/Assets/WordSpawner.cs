using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;

    //public Transform wordCanvas;

    public WordDisplay SpawnWord()
    {
        Vector3 randomSpawn = new Vector3(Random.Range(-2, -9), 4, 0);

        GameObject wordObj = Instantiate(wordPrefab, randomSpawn, Quaternion.identity, gameObject.transform);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;

    }
}
