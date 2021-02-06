using UnityEngine;
using System.Collections;
using TMPro;

public class Display_Cue : Visual_Cue
{
    public GameObject game_object; // related game object 
    public GameObject hidden_location; // hidding position 
    public GameObject display_location; // display possiton 
    public GameObject position_object; // position to move display cue 

    private Vector3 starting_position; // starting position 
    private TextMeshPro text_mesh_pro; // text component 
    private string start_string; // start message 

    // fires at start 
    public override void Initialize()
    {
        starting_position = transform.position; // get starting position 
        text_mesh_pro = GetComponentInChildren<TextMeshPro>(); // get text component 
        start_string = text_mesh_pro.text; // get starting message 
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

        // if related object is in display position 
        if(game_object.transform.position == display_location.transform.position)
        {
            game_object.transform.position = hidden_location.transform.position; // relocate to hidding position 
            transform.position = starting_position; // relocate display cue to staring position
            text_mesh_pro.text = start_string; // set text to starting message 
        }

        // if not in display position 
        else
        {
            game_object.transform.position = display_location.transform.position; // relocate to display position
            transform.position = position_object.transform.position; // relocate display cue to choosen position
            text_mesh_pro.text = "Close"; // set text to "close"
            
            
        }
        
    }
}
