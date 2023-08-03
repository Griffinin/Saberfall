using System.Collections.Generic;

public sealed class PlayerInventoryModel<T>
{
    private List<KeyValuePair<T, int>> inventoryItems = new();

    internal void addInventoryItem(T item)
    {
        int itemIndex = findItem(ref inventoryItems, ref item);
        
        if (itemIndex != -1)
            inventoryItems[itemIndex] = new KeyValuePair<T, int>(inventoryItems[itemIndex].Key, inventoryItems[itemIndex].Value + 1);
        else inventoryItems.Add(new KeyValuePair<T, int>(item, 1));
    }

    internal void removeInventoryItem(T item)
    {
        int itemIndex = findItem(ref inventoryItems, ref item);

        if (itemIndex != -1)
        {
            inventoryItems[itemIndex] = new KeyValuePair<T, int>(inventoryItems[itemIndex].Key, inventoryItems[itemIndex].Value - 1);
            if (inventoryItems[itemIndex].Value == 0) inventoryItems.RemoveAt(itemIndex);
        }
    }

    internal void removeAllEmptyItems() => inventoryItems.RemoveAll(i => i.Value == 0);

    internal void removeAllOfItem(T item) => inventoryItems.Remove(inventoryItems[findItem(ref inventoryItems, ref item)]);

    internal void clearInventory() => inventoryItems.Clear();

    internal int itemCount(T item) => inventoryItems.Find(i => i.Key.Equals(item)).Value;

    private int findItem(ref List<KeyValuePair<T, int>> list, ref T item)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i].Key.Equals(item))
                return i;
        return -1; // Not found
    }
}
