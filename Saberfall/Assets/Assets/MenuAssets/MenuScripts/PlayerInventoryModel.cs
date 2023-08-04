using System.Collections.Generic;

public sealed class PlayerInventoryModel<T>
{
    private List<KeyValuePair<T, int>> inventoryItems = new();

    // Adds item to inventory
    internal void addInventoryItem(T item, bool isSingular = false)
    {
        int itemIndex = findItem(ref inventoryItems, ref item); // Get item index
        
        if (itemIndex != -1 && !isSingular) // If found and not exclusive, increase item count by one
            inventoryItems[itemIndex] = new KeyValuePair<T, int>(inventoryItems[itemIndex].Key, inventoryItems[itemIndex].Value + 1);
        else inventoryItems.Add(new KeyValuePair<T, int>(item, 1)); // Otherwise, make a new one
    }

    internal void removeInventoryItem(T item)
    {
        int itemIndex = findItem(ref inventoryItems, ref item); // Get item index

        if (itemIndex != -1) // If found, decrease frequency by one
        {
            inventoryItems[itemIndex] = new KeyValuePair<T, int>(inventoryItems[itemIndex].Key, inventoryItems[itemIndex].Value - 1);
            if (inventoryItems[itemIndex].Value == 0) inventoryItems.RemoveAt(itemIndex);   // If by chance item was the last one, delete it
        }
    }

    // Remove any items that have an empty value
    internal void removeAllEmptyItems() => inventoryItems.RemoveAll(i => i.Value == 0);

    // Clears inventory of a specific item
    internal void removeAllOfItem(T item) => inventoryItems.Remove(inventoryItems[findItem(ref inventoryItems, ref item)]);

    // Clears inventory
    internal void clearInventory() => inventoryItems.Clear();

    // Get a specific item's frequency
    internal int itemCount(T item) => inventoryItems.Find(i => i.Key.Equals(item)).Value;

    // Returns first instance index of item
    private int findItem(ref List<KeyValuePair<T, int>> list, ref T item)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i].Key.Equals(item))
                return i;
        return -1; // Not found
    }
}
