using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    private UnityEngine.UI.Button exitButton;

    private void Awake()
    {
        exitButton = GetComponent<UnityEngine.UI.Button>();
    }

    private void OnEnable()
    {
        // Tells MenuController to exit the game from the start menu
        exitButton.onClick.AddListener(() => MenuController.menuController.exitGame(ExitID.StartMenu));
    }

    private void OnDisable()
    {
        exitButton.onClick.RemoveAllListeners();
    }
}
