using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Order_Selection_Cue : Visual_Cue
{
    public GameObject order_object = null; // objective to point to 

    public List<GameObject> task_list; // list of tasks in objective 

    // remove task from task list 
    public void RemoveTask(GameObject gameObject)
    {
        task_list.Remove(gameObject); // removes from list 

        // if no task left 
        if (task_list.Count == 0)
        {
            game_manager.ObjectiveComplete(this.gameObject); // let game manager know the object is completed
            
          
        }
    }

    // fires at start 
    public override void Initialize()
    {
        // if related object is not null 
        if (order_object != null) 
        {
            // gather task and add them to list 
            for(int i = 0; i < order_object.transform.childCount; i++) 
            {
                if (order_object.transform.GetChild(i).gameObject.CompareTag("Task"))
                {
                    task_list.Add(order_object.transform.GetChild(i).gameObject);
                }
            }
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

    // when clicked on 
    public override void OnPointerClick()
    {
        base.OnPointerClick();

        // if related object is not null 
        if (order_object != null)
        {
            game_manager.SetObjective(order_object, this.gameObject); // tell game manager to set objective to this objective.
        }
    }
}
