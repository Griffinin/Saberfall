using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class inventoryAdder : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i<5; i++)
        {
            MenuController.addInventoryItem(GameObject.Find("ItemList/Knife"));
        }

        for (int i = 0; i < 10; i++)
        {
            MenuController.addInventoryItem(GameObject.Find("ItemList/Sword"));
        }

        for(int i = 0; i< 5; i++)
        {
            MenuController.addInventoryItem(GameObject.Find("ItemList/Sword2"));
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) MenuController.escPressed(); // In-game menu
        if (Input.GetKeyDown(KeyCode.Tab)) MenuController.tabPressed();    // Inventory
    }
}
