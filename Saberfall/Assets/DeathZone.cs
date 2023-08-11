using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    //[SerializeField]PlayerMovement player;

    private void Start()
    {
       // player = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //invokes the completelevel function to reload the scene on death
        Invoke("CompleteLevel", 0.1f);


    }



    private void CompleteLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //change to Find instead
        SceneManager.UnloadSceneAsync("RemoveThisLev1");
        SceneManager.LoadScene("RemoveThisLev1", LoadSceneMode.Additive);

    }
}
