using UnityEngine;

public sealed class MenuController : SuperController
{
    public static MenuController menuController { get; private set; }

    // Models ------
    private PlayerInventoryModel<GameObject> playerInventoryModel = new(); // <T> is set to GameObject as GameItem is not set!!

    // Controllers ------
    [SerializeField] private GameObject menuScripts;    // Menu Controller
    [SerializeField] private GameObject levelScripts;   // Level Controller

    // Views ------
    [SerializeField] private GameObject menuUI;     // Group that contains all other menu Views

    // Views/Starter Menu
    private static readonly string STARTER_MENU_PATH = "StarterMenuUI/";
    private GameObject startMenuUI;                 // Group that contains all start menu Views
    private GameObject startMenu;
    private GameObject startLoadMenu;
    private GameObject startSettingsMenu;
    private GameObject startCreditsMenu;

    // Views/In-Game Menu
    private static readonly string IN_GAME_MENU_PATH = "In-GameMenuUI/";
    private GameObject inGameMenuUI;                // Group that contains all other in-game menu Views
    private GameObject inGameMenu;
    private GameObject inGameSaveMenu;
    private GameObject inGameLoadMenu;
    private GameObject inGameSettingsMenu;
    private GameObject inGameInteractionBackground;

    // Views/Inventory
    private GameObject inventoryUI;

    void Awake()
    {
        // Make singleton class
        if (menuController != null && menuController != this) Destroy(this);
        else menuController = this;

        // Get Starter Menu Views from menuUI parent
        startMenuUI                     = findGameObjectInParent(ref menuUI, STARTER_MENU_PATH);
        startMenu                       = findGameObjectInParent(ref startMenuUI, "StartMenu");
        startLoadMenu                   = findGameObjectInParent(ref startMenuUI, "LoadMenu");
        startSettingsMenu               = findGameObjectInParent(ref startMenuUI, "SettingsMenu");
        startCreditsMenu                = findGameObjectInParent(ref startMenuUI, "Credits");
        
        // Get In-Game Menu Views from menuUI parent
        inGameMenuUI                    = findGameObjectInParent(ref menuUI, IN_GAME_MENU_PATH);
        inGameMenu                      = findGameObjectInParent(ref inGameMenuUI, "In-GameMenu");
        inGameSaveMenu                  = findGameObjectInParent(ref inGameMenuUI, "In-GameSaveMenu");
        inGameLoadMenu                  = findGameObjectInParent(ref inGameMenuUI, "In-GameLoadMenu");
        inGameSettingsMenu              = findGameObjectInParent(ref inGameMenuUI, "In-GameSettingsMenu");
        inGameInteractionBackground     = findGameObjectInParent(ref inGameMenuUI, "InteractionBackground");

        // Get Inventory Views from menuUI parent
        inventoryUI                       = menuUI.transform.Find("InventoryUI").gameObject;
    }

    public void startGame()
    {
        // Shows the Start Menu
        menuScripts.SetActive(true); // Controller
        menuUI.SetActive(true); // View
    }

    // -------------
    // | StartMenu |
    // -------------

    public void viewNewGameMenu()
    {
        print("New Game Scene NOT Set!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("SCENE_NAME");
        menuUI.SetActive(false);
    }

    public void viewLoadMenu()
    {
        startMenu.SetActive(false);
        startLoadMenu.SetActive(true);
    }

    public void viewSetingsMenu()
    {
        startMenu.SetActive(false);
        startCreditsMenu.SetActive(false);
        startSettingsMenu.SetActive(true);
    }

    public void setMusicVolume(float volume)
    {
        MainAudioMixer.SetFloat("MasterVolume", volume);
    }

    public void setFullscreenMode(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void viewCreditsMenu()
    {
        startMenu.SetActive(false);
        startSettingsMenu.SetActive(false);
        startCreditsMenu.SetActive(true);
    }

    // Quit game on exit button listener interaction
    public void exitGame(ExitID exitID)
    {
        // DELETE AFTER GAME IS FINALIZED !!
        #if UNITY_EDITOR // Preprocessor directive to simulate game exit
            Debug.Log("ExitGame running Preprocessor Directive.");
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    // ----------------
    // | In-Game Menu |
    // ----------------

    public void viewInGameMenu()
    {
        inGameMenuUI.SetActive(true);
    }

    public void viewInGameSaveMenu()
    {
        inGameLoadMenu.SetActive(false);
        inGameSettingsMenu.SetActive(false);
        activateInteractionBackground();
        inGameSaveMenu.SetActive(true);
    }

    public void viewInGameLoadMenu()
    {
        inGameSaveMenu.SetActive(false);
        inGameSettingsMenu.SetActive(false);
        activateInteractionBackground();
        inGameLoadMenu.SetActive(true);
    }

    public void viewInGameSettings()
    {
        inGameSaveMenu.SetActive(false);
        inGameLoadMenu.SetActive(false);
        activateInteractionBackground();
        inGameSettingsMenu.SetActive(true);
    }

    public void hideInGameMenu()
    {
        inGameMenuUI.SetActive(false);
    }

    private void activateInteractionBackground()
    {
        if (!inGameInteractionBackground.activeSelf)
            inGameInteractionBackground.SetActive(true);
    }

    // -------------
    // | Inventory |
    // -------------

    public void viewInventory()
    {
        inventoryUI.SetActive(true);
    }

    public void hideInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void inventorySlotSelected(GameObject slot)
    {
        Debug.Log(slot.name.Substring("inventoryslot".Length));
    }

    // -----------------
    // | Miscellaneous |
    // -----------------

    public void escPressed()
    {
        Debug.Log("ESCAPE PRESSED!!");
        Debug.Log("TESTING IN-GAME MENU");

        if (startMenuUI.activeSelf)
        {
            startMenuUI.SetActive(false);
            viewInGameMenu();
        }
        else if (inGameMenuUI.activeSelf)
        {
            hideInGameMenu();
            startMenuUI.SetActive(true);
        }
    }

    public void tabPressed()
    {
        Debug.Log("TAB PRESSED!!");
        Debug.Log("TESTING INVENTORY");

        if (startMenuUI.activeSelf)
        {
            startMenuUI.SetActive(false);
            viewInventory();
        }
        else if (inventoryUI.activeSelf)
        {
            hideInventory();
            startMenuUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) escPressed();
        if (Input.GetKeyDown(KeyCode.Tab)) tabPressed();
    }

    private GameObject findGameObjectInParent(ref GameObject parent, string path) => parent.transform.Find(path).gameObject;

    /// <summary>
    /// <b>GAMEOBJECT NEEDS TO BE CHANGED TO GAMEITEM ONCE IMPLEMENTED!!</b>
    /// </summary>
    public void useItem(GameObject item)
    {
        playerInventoryModel.removeInventoryItem(item);
    }

    /// <summary>
    /// <b>GAMEOBJECT NEEDS TO BE CHANGED TO GAMEITEM ONCE IMPLEMENTED!!</b>
    /// </summary>
    public void addItem(GameObject item)
    {
        playerInventoryModel.addInventoryItem(item);
    }

    /// <summary>
    /// <b>GAMEOBJECT NEEDS TO BE CHANGED TO GAMEITEM ONCE IMPLEMENTED!!</b>
    /// </summary>
    public void dropItem(GameObject item)
    {
        playerInventoryModel.removeAllOfItem(item);
    }
}