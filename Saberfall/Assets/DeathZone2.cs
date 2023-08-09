using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.gameObject.name == "Player" && !levelCompleted)
        // {

        Invoke("CompleteLevel", 0.1f);

        // }
    }



    private void CompleteLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //change to Find instead
        SceneManager.UnloadSceneAsync("MainChar-scene");
        SceneManager.LoadScene("MainChar-scene", LoadSceneMode.Additive);

    }
}
