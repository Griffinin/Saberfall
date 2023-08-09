using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryAdder : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        MenuController.addInventoryItem(GameObject.Find("ItemList/Knife"), true);
        MenuController.addInventoryItem(GameObject.Find("ItemList/Knife"), true);
        MenuController.addInventoryItem(GameObject.Find("ItemList/Knife"), true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) MenuController.escPressed(); // In-game menu
        if (Input.GetKeyDown(KeyCode.Tab)) MenuController.tabPressed();    // Inventory
    }
}
