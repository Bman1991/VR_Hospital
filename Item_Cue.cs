using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum_PickUpItem;

public class Item_Cue : Visual_Cue
{

    public string item_name; // name of item
    public Item_Type item; // type of item, which may just be the literial name
    
    

    // fires at start
    public override void Initialize()
    {
     
    }

    
    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        if (!player.player_inventory.Contains(item))
        {
            camera.ReadObject("Pickup " + item_name); // read item name to player
            
        }

        else
        {
            camera.ReadObject("Drop " + item_name + " here");
            
        }
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }
    public override void OnPointerClick()
    {
        base.OnPointerClick();
        if (!player.player_inventory.Contains(item))
        {
            player.AddItem(item); // add item type to player inventory
            DisplayItem(false);
        }

        else
        {
            player.RemoveItem(item);
            DisplayItem(true);
        }
    }

    private void DisplayItem(bool result) 
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(result);
            }
        }
        
    }
}
