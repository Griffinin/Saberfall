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

        levelCompleted = true;
        Invoke("CompleteLevel", 2f); //invokes the completelevel function

        Debug.Log("Player entered checkpoint");

    }




    private void CompleteLevel()
    {
        //transitions to menu when level is complete
        
        SceneManager.LoadScene("Menu");

    }
}
