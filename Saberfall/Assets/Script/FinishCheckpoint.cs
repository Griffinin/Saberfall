using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckpoint : MonoBehaviour
{
    //private BoxCollider2D coll;
    //private AudioSource finishSound;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Invoke("CompleteLevel", 2f);
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    
}
