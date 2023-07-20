using UnityEngine;

public sealed class MenuController : SuperController
{
    public static MenuController menuController { get; private set; }

    // Models
    private PlayerInventoryModel<GameObject> playerInventoryModel = new(); // <T> is set to GameObject as GameItem is not set!!

    // Controllers
    [SerializeField] private GameObject menuScripts;    // Menu Controller
    [SerializeField] private GameObject levelScripts;   // Level Controller

    // Views
    [SerializeField] private GameObject menuUI;     // Group that contains all other menu Views

    // Views/Starter Menu
    private GameObject startMenu;
    private GameObject startSettingsMenu;
    private GameObject startCreditsMenu;

    // Views/In-Game Menu
    private GameObject inGameMenu;
    private GameObject inGameSaveGameMenu;
    private GameObject inGameLoadGameMenu;
    private GameObject inGameSettingsMenu;

    void Awake()
    {
        // Make singleton class
        if (menuController != null && menuController != this) Destroy(this);
        else menuController = this;

        // Get Starter Menu Views from menuUI parent
        startMenu               = menuUI.transform.Find("StartMenu").gameObject;
        startSettingsMenu       = menuUI.transform.Find("SettingsMenu").gameObject;
        startCreditsMenu        = menuUI.transform.Find("Credits").gameObject;
        
        // Get In-Game Menu Views from menuUI parent
        inGameMenu              = menuUI.transform.Find("In-Game Menu").gameObject;
        //inGameSaveGameMenu      = menuUI.transform.Find("").gameObject;
    }

    public void startGame()
    {
        // Shows the Start Menu
        menuScripts.SetActive(true); // Controller
        menuUI.SetActive(true); // View
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

    public void escPressed()
    {
        Debug.Log("ESCAPE PRESSED!!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) escPressed();
    }

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