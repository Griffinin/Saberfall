using UnityEngine;

public sealed class MenuController : SuperController
{
    public static MenuController menuController { get; private set; }

    // Vars
    private static int selectedInventorySlot;

    // Models ------
    private static PlayerInventoryModel<GameObject> playerInventoryModel = new(); // <T> is set to GameObject as GameItem is not set!!

    // Controllers ------
    [SerializeField] private GameObject menuScripts;    // Menu Controller
    [SerializeField] private GameObject levelScripts;   // Level Controller

    // Views ------
    [SerializeField] private GameObject menuUI;     // Group that contains all other menu Views

    // Views/Starter Menu
    private static readonly string STARTER_MENU_PATH = "StarterMenuUI/";
    private static GameObject startMenuUI;                 // Group that contains all start menu Views
    private GameObject startMenu;
    private GameObject startLoadMenu;
    private GameObject startSettingsMenu;
    private GameObject startCreditsMenu;

    // Views/In-Game Menu
    private static readonly string IN_GAME_MENU_PATH = "In-GameMenuUI/";
    private static GameObject inGameMenuUI;                // Group that contains all other in-game menu Views
    private GameObject inGameMenu;
    private GameObject inGameSaveMenu;
    private GameObject inGameLoadMenu;
    private GameObject inGameSettingsMenu;
    private GameObject inGameInteractionBackground;

    // Views/Inventory
    private static GameObject inventoryUI;                 // Group that contains all other inventory Views
    private static GameObject inventorySlots;
    private static GameObject inventoryAction;
    private static GameObject inventoryInteractions;
    private GameObject inventorySpritePlaceholder;

    void Awake()
    {
        // Make singleton class
        if (menuController != null && menuController != this) Destroy(this);
        else menuController = this;
        DontDestroyOnLoad(this.gameObject);

        // Get Starter Menu Views from menuUI parent
        startMenuUI = findGameObjectInParent(ref menuUI, STARTER_MENU_PATH);
        startMenu = findGameObjectInParent(ref startMenuUI, "StartMenu");
        startLoadMenu = findGameObjectInParent(ref startMenuUI, "LoadMenu");
        startSettingsMenu = findGameObjectInParent(ref startMenuUI, "SettingsMenu");
        startCreditsMenu = findGameObjectInParent(ref startMenuUI, "Credits");

        // Get In-Game Menu Views from menuUI parent
        inGameMenuUI = findGameObjectInParent(ref menuUI, IN_GAME_MENU_PATH);
        inGameMenu = findGameObjectInParent(ref inGameMenuUI, "In-GameMenu");
        inGameSaveMenu = findGameObjectInParent(ref inGameMenuUI, "In-GameSaveMenu");
        inGameLoadMenu = findGameObjectInParent(ref inGameMenuUI, "In-GameLoadMenu");
        inGameSettingsMenu = findGameObjectInParent(ref inGameMenuUI, "In-GameSettingsMenu");
        inGameInteractionBackground = findGameObjectInParent(ref inGameMenuUI, "InteractionBackground");

        // Get Inventory Views from menuUI parent
        inventoryUI = findGameObjectInParent(ref menuUI, "InventoryUI");
        inventorySlots = findGameObjectInParent(ref inventoryUI, "InventorySlots");
        inventoryAction = findGameObjectInParent(ref inventoryUI, "InventoryAction");
        inventorySpritePlaceholder = findGameObjectInParent(ref inventoryAction, "SpritePlaceholder");
        inventoryInteractions = findGameObjectInParent(ref inventoryAction, "Interactions");
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("RemoveThisLev1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        startMenuUI.transform.Find("Background").gameObject.SetActive(false);
        startMenu.SetActive(false);
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

    // Return game master volume value
    // Rounded to 2 decimal places
    public float getMusicVolume() => MainAudioMixer.GetFloat("MasterVolume", out float volume) ? Mathf.Round(volume * 100f) * .01f : 0f;

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

    public static void viewInGameMenu()
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
        if (inGameSettingsMenu.activeSelf)
        {
            inGameInteractionBackground.SetActive(false);
            inGameSettingsMenu.SetActive(false);
        }
        else
        {
            activateInteractionBackground();
            inGameSettingsMenu.SetActive(true);
        }
    }

    public static void hideInGameMenu()
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

    public static void viewInventory()
    {
        inventoryUI.SetActive(true);
    }

    public static void hideInventory()
    {
        inventoryUI.SetActive(false);
    }

    private void viewInventoryAction() => inventoryAction.SetActive(true);

    private static void hideInventoryAction() => inventoryAction.SetActive(false);

    private void viewInventoryInteractions() => inventoryInteractions.SetActive(true);

    private static void hideInventoryInteractions() => inventoryInteractions.SetActive(false);

    public void inventorySlotSelected(GameObject slot)
    {
        selectedInventorySlot = int.Parse(slot.name[13..]);

        if (slot.transform.childCount > 0)
        {
            viewInventoryAction();
            updateSpritePlaceholder(slot.transform.GetChild(0).gameObject);
            viewInventoryInteractions();
            updateInventoryItemFrequency();
            inventorySpritePlaceholder.transform.GetChild(0).name = slot.transform.GetChild(0).name;    // Update Placeholder sprite name
        }
        else hideInventoryAction();
    }

    private void updateSpritePlaceholder(GameObject item)
    {
        if (inventorySpritePlaceholder.transform.childCount > 0)
        {
            replaceSpritePlaceholder(ref item);
        }
        else
        {
            createSpriteforSpritePlaceholder(ref item);
        }
    }

    private GameObject createSpriteforSpritePlaceholder(ref GameObject item)
    {
        GameObject newSprite = new GameObject(item.name, typeof(UnityEngine.UI.Image));
        newSprite.transform.SetParent(inventorySpritePlaceholder.transform, false);
        newSprite.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<UnityEngine.UI.Image>().sprite;
        newSprite.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;

        RectTransform spriteRectTransform = newSprite.GetComponent<RectTransform>();
        spriteRectTransform.anchorMin = new Vector2(.1f, .1f);
        spriteRectTransform.anchorMax = new Vector2(.9f, .9f);
        spriteRectTransform.offsetMin = new Vector2(0, 0);
        spriteRectTransform.offsetMax = new Vector2(0, 0);
        spriteRectTransform.sizeDelta = new Vector2(0, 0);

        return newSprite;
    }

    private void replaceSpritePlaceholder(ref GameObject item)
    {
        inventorySpritePlaceholder.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    private void destroySpritePlaceholder() => Destroy(inventorySpritePlaceholder.transform.GetChild(0).gameObject);

    public void inventoryUseItem()
    {
        // IDK, USE_ITEM IS FOR EQUIPING ITEMS SO CAN'T MAKE RN
    }

    public void inventoryDropItem()
    {
        // Get TextMeshPro from ItemAmount in Interactions and decrease amount
        GameObject inventoryItemAmount = findGameObjectInParent(ref inventoryInteractions, "ItemAmount");
        TMPro.TextMeshProUGUI itemAmountTMP = inventoryItemAmount.GetComponent<TMPro.TextMeshProUGUI>();

        playerInventoryModel.removeInventoryItem(GameObject.Find("ItemList/" + inventorySlots.transform.GetChild(selectedInventorySlot - 1).GetChild(0).name));
        itemAmountTMP.SetText("x" + (int.Parse(itemAmountTMP.text[1..]) - 1));

        // Otherwise, delete it as the amount is 0
        if (int.Parse(itemAmountTMP.text[1..]) < 1)
        {
            hideInventoryInteractions();
            hideInventoryAction();
            Destroy(inventorySlots.transform.GetChild(selectedInventorySlot - 1).GetChild(0).gameObject);
            inventorySlots.transform.GetChild(selectedInventorySlot - 1).DetachChildren();  // Because Destroy() executes at end of frame
            if (selectedInventorySlot - 1 != getUsedInventorySlotsLength())
                sortInventorySlots();
        }
    }

    private static GameObject getInventorySlot(int index)
    {
        return inventorySlots.transform.GetChild(index).gameObject;
    }

    private static int getUsedInventorySlotsLength()
    {
        int count = 0;
        for (int i = 0; i < inventorySlots.transform.childCount; i++)
            if (inventorySlots.transform.GetChild(i).childCount != 0) count++;
        return count;
    }

    public static bool addInventoryItem(GameObject item, bool exclusive = false)
    {
        if (playerInventoryModel.itemCount(item) >= 1 && !exclusive)
        {
            playerInventoryModel.addInventoryItem(item);
            return true;
        }
        else
        {
            int usedInventorySlotsLength = getUsedInventorySlotsLength();

            if (usedInventorySlotsLength >= 0 && usedInventorySlotsLength < 54)
            {
                GameObject itemInventory = new GameObject(item.name, typeof(UnityEngine.UI.Image));
                itemInventory.transform.SetParent(inventorySlots.transform.GetChild(usedInventorySlotsLength), false);
                itemInventory.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                itemInventory.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;   // Looks goofy otherwise

                playerInventoryModel.addInventoryItem(item, exclusive);

                return true;
            }
            else return false;
        }
    }

    private static void sortInventorySlots()
    {
        if (getUsedInventorySlotsLength() + 1 > 1)   // Prior length is needed
        {
            GameObject lastItem = inventorySlots.transform.GetChild(getUsedInventorySlotsLength()).GetChild(0).gameObject;
            lastItem.transform.SetParent(inventorySlots.transform.GetChild(selectedInventorySlot - 1), false);
        }
    }

    private void updateInventoryItemFrequency()
    {
        findGameObjectInParent(ref inventoryInteractions, "ItemAmount").GetComponent<TMPro.TextMeshProUGUI>().SetText(
            "x" + playerInventoryModel.itemCount(GameObject.Find("ItemList/" + inventorySlots.transform.GetChild(selectedInventorySlot - 1).GetChild(0).name)));
    }

    public static int getItemCount(GameObject item) => playerInventoryModel.itemCount(GameObject.Find("ItemList/" + item.name));

    public static bool hasItem(GameObject item) => getItemCount(item) >= 1;

    public static bool removeItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.transform.childCount && getInventorySlot(i).transform.childCount > 0; i++)
            if (getInventorySlot(i).transform.GetChild(0).name.Equals(item.name))
            {
                selectedInventorySlot = i + 1; // Based on non-array count

                playerInventoryModel.removeInventoryItem(GameObject.Find("ItemList/" + item.name));

                // Delete it as the amount is 0
                if (getItemCount(GameObject.Find("ItemList/" + item.name)) < 1)
                {
                    hideInventoryInteractions();
                    hideInventoryAction();
                    Destroy(inventorySlots.transform.GetChild(selectedInventorySlot - 1).GetChild(0).gameObject);
                    inventorySlots.transform.GetChild(selectedInventorySlot - 1).DetachChildren();  // Because Destroy() executes at end of frame
                    if (selectedInventorySlot - 1 != getUsedInventorySlotsLength())
                        sortInventorySlots();
                }

                return true;
            }
        return false;
    }

    // -----------------
    // | Miscellaneous |
    // -----------------

    public static void escPressed()
    {
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

    public static void tabPressed()
    {
        if (startMenuUI.activeSelf)
        {
            startMenuUI.SetActive(false);
            viewInventory();
        }
        else if (inventoryUI.activeSelf)
        {
            hideInventoryAction();
            hideInventory();
            startMenuUI.SetActive(true);
        }
    }

    private void qPressed()
    {
        switch (Random.Range(1, 3 + 1))
        {
            case 1:
                if (!addInventoryItem(GameObject.Find("ItemList/Knife"), true)) print("INVEN FULL");
                break;
            case 2:
                if (!addInventoryItem(GameObject.Find("ItemList/Sword"))) print("INVEN FULL");
                break;
            case 3:
                if (!addInventoryItem(GameObject.Find("ItemList/Sword2"))) print("INVEN FULL");
                break;
            default:
                Debug.Log("Some how we're outside the switch range.");
                break;
        }
    }

    private void pPressed()
    {
        removeItem(GameObject.Find("ItemList/Sword"));
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape)) escPressed(); // In-game menu
        //if (Input.GetKeyDown(KeyCode.Tab)) tabPressed();    // Inventory
        // if (Input.GetKeyDown(KeyCode.Q)) qPressed();    // Testing for in-game inventory
        // if (Input.GetKeyDown(KeyCode.P)) pPressed();   // Removing Items
    }

    private GameObject findGameObjectInParent(ref GameObject parent, string path) => parent.transform.Find(path).gameObject;
}