using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum_PickUpItem;

public class Criteria_Visual_Cue : Visual_Cue
{
    public List<Item_Type> criteria; // criteria player must meet

    // checks if player meets criteria
    public bool MeetsCriteria() 
    {
        int success_counter = 0; // counts met criteria

        // if there is criteria to meet 
        if (criteria.Count > 0)
        {
            // compare items in both item lists
            foreach (Item_Type item in criteria)
            {
                // if a criteria is met
                if (player.player_inventory.Contains(item))
                {
                    success_counter += 1; // count it as met
                }
            }

            // if everything is met 
            if (success_counter == criteria.Count)
            {
                return true; // return ture
            }

            // if criteria is not met 
            else
            {
                return false; // return false 
            }
        }

        // if there is not criteria to meet
        else 
        {
            return true; // return true
        }
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }

    public override void OnPointerClick()
    {
        base.OnPointerClick();
    }

}
