using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a finish checkpoint in a level. When the player collides with this checkpoint,
/// it marks the level as completed and progresses to the next level after a short delay.
/// </summary>
public class FinishCheckpoint : MonoBehaviour
{
    private bool levelCompleted = false;
    //private AudioSource finishSound;

    /// <summary>
    /// Returns whether the current level has been completed or not.
    /// </summary>
    /// <returns>True if the level is completed, false otherwise.</returns>
    public bool currentLev()
    {
        return levelCompleted;
    }

    /// <summary>
    /// Handles the event when an object enters the 2D collider of this checkpoint.
    /// If the player enters the checkpoint, it marks the level as completed.
    /// </summary>
    /// <param name="collision">The collider of the object that entered.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelCompleted = true;
        // Invokes the CompleteLevel method after a 2-second delay to transition to the next level.
        Invoke("CompleteLevel", 2f);
        Debug.Log("Player entered checkpoint");
    }

    /// <summary>
    /// Completes the current level by unloading it and then loading the main character's scene.
    /// Note: The scene name "RemoveThisLev1" might need to be updated.
    /// </summary>
    private void CompleteLevel()
    {
        SceneManager.UnloadSceneAsync("RemoveThisLev1"); // Consider updating the scene name if necessary.
        SceneManager.LoadScene("MainChar-scene", LoadSceneMode.Additive);
    }
}
