using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_Cue : MonoBehaviour
{

    // Public Class Variables
    public Material inactive_mat;
    public Material active_mat;
    public Material selected_mat;
    

    // Private Class Variables
    private Renderer my_renderer;

    // Protected Class Variables
    protected GVR_Player player = null;
    protected LookAt_Pointer camera = null;
    protected GameManager game_manager = null;

    // Start is called before the first frame update
    void Start()
    {
        // get following Game Objects
        GameObject player_game_object = GameObject.FindGameObjectWithTag("Player"); // get Player 
        GameObject camera_game_object = GameObject.FindGameObjectWithTag("MainCamera"); // get Player camera
        GameObject game_manager_game_object = GameObject.FindGameObjectWithTag("GameManager");

        // Cast to respective classes
        player = player_game_object.GetComponent<GVR_Player>();
        camera = camera_game_object.GetComponent<LookAt_Pointer>();
        game_manager = game_manager_game_object.GetComponent<GameManager>();

        my_renderer = GetComponent<Renderer>(); // get Renderer
        SetMaterial(inactive_mat); // set material to inactive material

        Initialize();

    }

    public void UpdateMaterial(Material inactive, Material active, Material selected) 
    {
        inactive_mat = inactive;
        active_mat = active;
        selected_mat = selected;
        SetMaterial(inactive);
    }

    // Called when LookAt_Pointer selects game object 
    public virtual void OnPointerEnter() 
    {
        SetMaterial(active_mat); // set material to active
        if (game_manager.transform.childCount > 0)
        {
            Destroy(game_manager.transform.GetChild(0).gameObject);
        }
        
    }

    // Called when LookAt_Pointer unselects game object 
    public virtual void OnPointerExit() 
    {
        SetMaterial(inactive_mat); // set material to inactive 
    }

    // Called when LookAt_Pointer clicks on game object 
    public virtual void OnPointerClick() 
    {
        SetMaterial(selected_mat); // set material to selected 
    }
    public virtual void Initialize() 
    {
        // overridable method
    }

    // Sets material 
    protected void SetMaterial(Material material) 
    {
        if (inactive_mat != null && active_mat != null && selected_mat != null) // check all materials are defined
        {
            my_renderer.material = material; // set provided material
        } 
    }
}
