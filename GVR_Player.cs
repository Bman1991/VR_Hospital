using UnityEngine;
using System.Collections;
using System.Collections.Generic; // to implement List 
using Enum_PickUpItem;

public class GVR_Player : MonoBehaviour
{
    // Public Class Variables
    public List<Item_Type> player_inventory; // inventory of items
    public List<Item_Type> player_discards;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // gets player's inventory
    public List<Item_Type> GetPlayerInventory() 
    {
        return player_inventory; 
    }

    // adds item to players inventory
    public void AddItem(Item_Type item) 
    {
        player_inventory.Add(item);
    }

    public void AddDiscardItem(Item_Type item)
    {
        player_discards.Add(item);
    }

    // removes an item from players inventory
    public void RemoveItem(Item_Type item)
    {
        if (player_inventory.Contains(item))
        {
            player_inventory.Remove(item);
        }
    }

    // clear out inventory
    public void ClearInventory()
    {
        player_inventory.Clear();
    }

    public void DiscardItems()
    {
        player_discards.Clear();
    }


}
