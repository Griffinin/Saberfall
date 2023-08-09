using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckpoint2 : MonoBehaviour
{
    private bool levelCompleted = false;
    public bool currentLev()
    {
        return levelCompleted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.gameObject.name == "Player" && !levelCompleted)
        // {
        levelCompleted = true;
        //CompleteLevel();
        Invoke("CompleteLevel", 2f);

        Debug.Log("Player entered checkpoint");
        // }
    }




    private void CompleteLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //change to Find instead
        
        SceneManager.LoadScene("Menu");

    }
}
