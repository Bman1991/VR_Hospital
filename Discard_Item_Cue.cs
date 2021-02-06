using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum_PickUpItem;

public class Discard_Item_Cue : Visual_Cue
{
    public List<Item_Type> discard_list;

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();

        if (player.player_discards.Count > 0) 
        {
            camera.ReadObject("Discard " + player.player_discards.Count + " items");
        }

        else
        {
            camera.ReadObject("No Item to discard");
        }
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }

    public override void OnPointerClick()
    {
        base.OnPointerClick();
        player.DiscardItems();
    }
}
