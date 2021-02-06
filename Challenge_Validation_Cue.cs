using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Challenge_Validation_Cue : Challenge_Cue
{
    public List<GameObject> target_object; // game object to check availability
    public string requires_message; // a message to display when something is required

    public override void OnPointerEnter()
    {
        // if target object is completed
        if (game_manager.IsCompleted(target_object))
        {
            base.OnPointerEnter(); // call parent class 
        }

        // if target object is available or is in the task list 
        else
        {
            camera.ReadObject(requires_message); // display message requiring 
        }
        
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }
    public override void OnPointerClick()
    {
        // if target object is completed
        if (game_manager.IsCompleted(target_object)) 
        {
            base.OnPointerClick(); // call parent and display challenge question
        }

    }
}
