using UnityEngine;
using System.Collections;

public class Nav_Cue : Visual_Cue
{
    // public Class Variables 
    public GameObject nav_point;
    public string destination;

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        camera.ReadObject(destination);
    }
    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }
    // When Clicked on 
    public override void OnPointerClick()
    {
        base.OnPointerClick(); // fire off parent method first
        player.transform.position = nav_point.transform.position; // teleport player to nav point 
        
    }
}
