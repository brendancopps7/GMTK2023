using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningTrigger : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if(collision.gameObject.layer == 3)
        {
            gameManager.winGame();
        }
        
    }
}
