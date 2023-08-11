using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //upon the player hitting the death zone in the second level invode ht CompleteLevel function to reload the scene

        Invoke("CompleteLevel", 0.1f);

    }



    private void CompleteLevel()
    {
        SceneManager.UnloadSceneAsync("MainChar-scene");
        SceneManager.LoadScene("MainChar-scene", LoadSceneMode.Additive);

    }
}
