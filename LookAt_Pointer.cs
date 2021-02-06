using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class LookAt_Pointer : MonoBehaviour
{
    // Public Class variables
    public Image cross_hair_timer;
    public Text ui_text;
    public Text ui_text_back;
    public float gaze_time_limit = 2;
    

    // Private Class variables 
    private float max_distance = 4f; // Max distance to cast ray, scale / 10
    private GameObject target_object = null; // Game object that currently the target or gazing at
    private bool gaze_status;
    private float gaze_timer;
    private float gaze_timer_percentage;


    // Update is called once per frame
    void Update()
    {
        // Determine if something is in front of camera or not
        RaycastHit hit;
        Debug.DrawRay(transform.position,transform.forward * max_distance,Color.red);
        try // try to cast ray on object
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, max_distance))
            {
                // New game object detected
                if (target_object != hit.transform.gameObject)
                {
                    target_object?.SendMessage("OnPointerExit"); // send exit message to previous game object 
                    target_object = hit.transform.gameObject; // set target object 
                    target_object.SendMessage("OnPointerEnter"); // send enter message to new game object 
                    StartGaze(); // start gaze timer
                    

                }
            }

            // No object detected
            else
            {
                target_object?.SendMessage("OnPointerExit"); // send exit message to current game object 
                target_object = null; // clear target object of any game object 
                StopGaze(); // stop gaze timer

            }
        }
        
        // if missing reference error occurs 
        catch (MissingReferenceException)
        {
            target_object = null; // null out 
        }

        // if screen is tapped or viewer trigger pressed 
        if (Google.XR.Cardboard.Api.IsTriggerPressed == true || gaze_timer_percentage >= 1) 
        {
            target_object.SendMessage("OnPointerClick"); // send click message to current game object
            StopGaze(); // stop gaze timer
        }

        if (Google.XR.Cardboard.Api.IsCloseButtonPressed)
        {
            Application.Quit();
        }

        // when gaze timer starts 
        if (gaze_status) 
        {
            gaze_timer += Time.deltaTime; // set time spent
            gaze_timer_percentage = gaze_timer / gaze_time_limit; // set percentage of time spent gazing
            cross_hair_timer.fillAmount = gaze_timer_percentage; // fill cross hair timer by percentage
        }
        
    }

    // Start gaze timer 
    private void StartGaze() 
    {
        ui_text.enabled = true; // show text above crosshair
        ui_text_back.enabled = true;
        gaze_status = true; // set status to true
    }

    // Stop gaze timer and reset 
    private void StopGaze() 
    {
        ui_text.enabled = false; // hide text above crosshair
        ui_text_back.enabled = false;
        ui_text.text = ""; // clear text
        ui_text_back.text = "";
        gaze_status = false; // set status to false
        gaze_timer = 0; // reset timer
        gaze_timer_percentage = 0; // reset percentage 
        cross_hair_timer.fillAmount = 0; // reset fill amount 
    }

    // Reads Object when hovering over
    public void ReadObject(string object_name) 
    {
        ui_text.text = object_name; // set text 
        ui_text_back.text = object_name;
    }
 
}
