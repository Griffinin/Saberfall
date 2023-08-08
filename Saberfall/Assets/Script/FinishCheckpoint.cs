using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckpoint : MonoBehaviour
{
    //private BoxCollider2D coll;
    private AudioSource finishSound;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>;
    }


    private void OnTriggerEnter2D(collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            finishSound.Play();
            Invoke("CompleteLevel", 2f);
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        SceneManagement.LoadScene(SceneManagement.GetActivityScene().buildIndex + 1);

    }
    
}
