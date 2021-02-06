using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Challenge_Cue : Criteria_Visual_Cue
{
    public GameObject challenge_question_prefab; // prefab to clone 
    public List<Challenge_Questions> challenge_questions; // question to pass 
    public string cue_name; // name of the cue or description
    public Material pass_inactive_mat, pass_active_mat, pass_selected_mat; // pass materials 
    public Material fail_inactive_mat, fail_active_mat, fail_selected_mat; // fail materials 
    public bool repeatable = false; // whether cue can fire after being activated the first to
    public bool discard_items = false; // whether to discard items after used
    

    private bool has_been_challenged = false; // whether or not cue has been activated or challenged by player

    // submits score once player answers question 
    public void SubmitScore(float score) 
    {
        Debug.Log(score);
        // if cue has not been challenged 
        if (!has_been_challenged)
        {
            // if score is greater than 50%
            if (score > 0.5)
            {
                try
                {
                    game_manager.TaskCompleted(this.transform.parent.gameObject); // let game manager know task is completed
                    DiscardItems();
                    has_been_challenged = true; // cue has been challenged 
                    ChangeMaterial(pass_inactive_mat, pass_active_mat, pass_selected_mat); // update material to passing material
                }
                catch (MissingReferenceException) 
                {
                    Debug.Log("Missing Reference Exception: Needs to have a parent game object to send to Game Manager");
                }
            }

            // score less than 50%
            else
            {
                ChangeMaterial(fail_inactive_mat, fail_active_mat, fail_selected_mat); // update material to fail material 
            }

            ShowObject(true); // show cue 
        }
        
    }

    // when no score is to be submited 
    public void EndTask() 
    {
        try
        {
            game_manager.TaskCompleted(this.transform.parent.gameObject); // let game manager know task is completed
            DiscardItems();
            has_been_challenged = true; // cue has been challeged
            ShowObject(true); // shoe cue 
            ChangeMaterial(pass_inactive_mat, pass_active_mat, pass_selected_mat); // update material to passing material
        }
        catch (MissingReferenceException) 
        {
            Debug.Log("Missing Reference Exception: Needs to have a parent game object to send to Game Manager");
        }

    }

    private void DiscardItems()
    {
        if (discard_items)
        {
            foreach (Enum_PickUpItem.Item_Type item in criteria)
            {
                if (player.player_inventory.Contains(item))
                {
                    player.RemoveItem(item);
                    player.AddDiscardItem(item);
                }
            }
        }
    }

    public void ChangeMaterial(Material inactive, Material active, Material selected) 
    {
        if (transform.parent.childCount > 0) 
        {
            for (int i = 0; i < transform.parent.childCount; i ++) 
            {
                transform.parent.GetChild(i).GetComponent<Challenge_Cue>().UpdateMaterial(inactive,active,selected); 
            }
        }
    }

    // show or hid cue 
    private void ShowObject(bool result) 
    {
        this.gameObject.SetActive(result);
    }

    // fires at start 
    public override void Initialize()
    {
        // gather challenge questions and add them to list 
        foreach (Challenge_Questions question in GetComponents<Challenge_Questions>()) 
        {
            challenge_questions.Add(question);
        }
    }
    public override void OnPointerEnter()
    {
        
        base.OnPointerEnter();

        // if there is criteria 
        if(criteria.Count > 0)
        {
            // check if player meets criteria
            if (MeetsCriteria()) 
            {
              camera.ReadObject(cue_name); // read cue name 
            }
            
            // if player does not meet criteria
            else
            {
              camera.ReadObject(cue_name + " Needs: " + string.Join(", ",criteria)); // display what is needed 
            }
        }

        // if no criteria to meet 
        else 
        {
            camera.ReadObject(cue_name); // read cue name 
        }
           
        
        
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
        
    }

    public override void OnPointerClick()
    {
        base.OnPointerClick();

        // check of cue has been challenged or if is repeatable 
        if (has_been_challenged == false || repeatable == true)
        {
            // check if player meets criteria
            if (MeetsCriteria())
            {
                GameObject current_prefab = Instantiate(challenge_question_prefab); // clone prefab
                current_prefab.transform.position = player.transform.position;
                current_prefab.transform.rotation = Quaternion.LookRotation(this.transform.position - camera.transform.position, Vector3.up); // rotate to face player 
                current_prefab.GetComponentInChildren<Challenge_Question_Sign>().Initialize(this, challenge_questions); // initialize prefab
                ShowObject(false); // hide cue 
            }
        }
    }

}
