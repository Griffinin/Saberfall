using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Quit game on exit button interaction
    public void Quit()
    {
        // DELETE AFTER GAME IS FINALIZED !!
        #if UNITY_EDITOR // Preprocessor directive to simulate game exit
            Debug.Log("ExitGame running Preprocessor Directive.");
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
