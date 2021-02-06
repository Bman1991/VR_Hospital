using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject target_game_object; // target game object to activate 
    public GameObject objective_game_object; // objective game object that holds task info
    public GameObject exit_object; // for displaying and hiding exits 

    public List<GameObject> objectives; // list of total objectives to finish

    // fires at start 
    private void Start()
    {
        // gather objectives and add them to list
        foreach (GameObject objective in GameObject.FindGameObjectsWithTag("Objective"))
        {
            objectives.Add(objective);
        }
    }

    // set objective to be current object 
    public void SetObjective(GameObject gameobject, GameObject objective_object) 
    {
        try
        {
            target_game_object?.SetActive(false);
            target_game_object = gameobject;
            target_game_object.SetActive(true);

            objective_game_object = objective_object;
        }

        catch (UnassignedReferenceException) 
        {
            target_game_object = gameobject;
            objective_game_object = objective_object;
        }
    }

    // when task is completed 
    public void TaskCompleted(GameObject gameobject) 
    {
        objective_game_object.SendMessage("RemoveTask", gameobject); // remove task from objective's task list 
    }

    // when an objective is completed 
    public void ObjectiveComplete(GameObject gameObject) 
    {
        objectives.Remove(gameObject); // remove objective from list 
        Destroy(gameObject); // destory objective

        // if all objectives are completed 
        if (objectives.Count == 0) 
        {
            Debug.Log("End of Simulation");
            exit_object.SetActive(true);
            
        }
    }

    public bool IsCompleted(GameObject gameObject)
    {
        
        if (gameObject.CompareTag("Task"))
        {
            // if gameobject is not listed in objective's task list 
            if (!objective_game_object.GetComponent<Order_Selection_Cue>().task_list.Contains(gameObject))
            {
                return true;
            }

            return false;
        }

        // if game object is a Objective
        if (gameObject.CompareTag("Objective"))
        {
            // if gameobject is not listed in objective list 
            if (!objectives.Contains(gameObject))
            {
                return true;
            }

            return false;
        }

        else
        {
            Debug.LogError(gameObject.name + " game object is neither Task or Objective");
            return false;
        }



    }
    // check of task or objective is completed 
    public bool IsCompleted(List<GameObject> list) 
    {
        int counter = 0; // counts completed 

        // cycle through list of gameobjects 
        foreach (GameObject gameObject in list) 
        {
            if (IsCompleted(gameObject)) 
            {
                counter += 1;
            }
        }

        if(counter == list.Count)
        {
            return true; // all is completed
        }

        else 
        {
            return false; // not all is completed
        }
        
    }
}
