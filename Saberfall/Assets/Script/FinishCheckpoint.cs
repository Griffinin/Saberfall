using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class FinishCheckpoint : MonoBehaviour
{
    private bool levelCompleted = false;
    //private AudioSource finishSound;
    // Start is called before the first frame update
    
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
        SceneManager.UnloadSceneAsync("RemoveThisLev1");
        SceneManager.LoadScene("MainChar-scene", LoadSceneMode.Additive);

    }
    
}
