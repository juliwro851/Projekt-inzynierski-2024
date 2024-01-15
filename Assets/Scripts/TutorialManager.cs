using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

/// <summary>
/// Class responsible for managing the tutorial.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // Mouse and keyboard images
    [SerializeField] Image mouse;
    [SerializeField] Image mouse2;
    [SerializeField] Image keyboard;
    [SerializeField] Image keyboard2;
    [SerializeField] Image mouseAndKeyboard;
    [SerializeField] Image mouseAndKeyboard2;
    [SerializeField] Image examFrame;

    [SerializeField] Button buttonNext; // Button to move to the next tutorial section

    [SerializeField] private Image imageFrame; // Image frame for the tutorial image

    public SettingsMenager sM;// Game settings manager

    // Mode toggles
    public Toggle normal;
    public Toggle high;
    public Toggle higher;

    private bool finished = false; // Flag indicating the completion of the first part of the tutorial
    private bool whiteToYellow = false;// Flag for changing the frame color from white to yellow
    float lerpValue = 0f; // Lerp interpolation value
    float lerpDuration = 0.5f; // Interpolation duration



    // Method called when the game starts
    void Start()
    {

        examFrame.gameObject.SetActive(false);// Disable the frame containing the tutorial text
        finished = false;// Set the flag to false

        // Choose the appropriate images based on mouse and keyboard settings
        if (sM.GetBoolPref("keyboard") && !sM.GetBoolPref("mouse"))
        {
            mouse.gameObject.SetActive(true);
            keyboard.gameObject.SetActive(false);
            mouseAndKeyboard.gameObject.SetActive(false);
            
            mouse2.gameObject.SetActive(true);
            keyboard2.gameObject.SetActive(false);
            mouseAndKeyboard2.gameObject.SetActive(false);
        }
        else if (sM.GetBoolPref("mouse") && !sM.GetBoolPref("keyboard"))
        {
            keyboard.gameObject.SetActive(true);
            mouse.gameObject.SetActive(false);
            mouseAndKeyboard.gameObject.SetActive(false);

            keyboard2.gameObject.SetActive(true);
            mouse2.gameObject.SetActive(false);
            mouseAndKeyboard2.gameObject.SetActive(false);
            
        }
        else
        {
            mouseAndKeyboard.gameObject.SetActive(true);
            mouse.gameObject.SetActive(false);
            keyboard.gameObject.SetActive(false);

            mouseAndKeyboard2.gameObject.SetActive(true);
            mouse2.gameObject.SetActive(false);
            keyboard2.gameObject.SetActive(false);

        }

    }

    // Method called every frame
    void Update()
    {
        if (finished)
        {
            examFrame.gameObject.SetActive(true);

            // User interaction handling based on mouse and keyboard settings
            if ((Input.GetMouseButtonDown(0) && sM.GetBoolPref("mouse")) || (Input.GetKeyDown(KeyCode.Space) && sM.GetBoolPref("keyboard")))
            {
                whiteToYellow = true;
            }

            // White to yellow color change effect
            if (whiteToYellow)
            {
                if (lerpValue < 1)
                {
                    lerpValue += Time.deltaTime / lerpDuration;
                    imageFrame.color = Color.Lerp(Color.yellow, Color.white, lerpValue);
                }
                else
                {
                    whiteToYellow = false;
                    lerpValue = 0f;
                }
            }
        }

        // Handling the Escape key - return to the main menu
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu");

        // Handling the Right Arrow key - move to the next toggle
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextToggle();
        }

        // Handling the Left Arrow key - move to the previous toggle
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevToggle();
        }

        // Handling Enter or Keypad Enter keys - move to the next scene
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GoToTheNextScene();
        }
    }

    // Method to move to the next toggle
    public void NextToggle()
    {
        if (normal.isOn)
        {
            high.isOn = true;
            OnToggleHigh();
        }
        else if (high.isOn)
        {
            higher.isOn = true;
            OnToggleHigher();
        }
    }

    // Method to move to the previous toggle
    public void PrevToggle()
    {
        if (higher.isOn)
        {
            high.isOn = true;
            OnToggleHigh();
        }
        else if (high.isOn)
        {
            normal.isOn = true;
            OnToggleNormal();
        }
    }

    // Toggle change handling methods
    public void OnToggleNormal()
    {
        if (normal.isOn)
        {
            high.isOn = false;
            higher.isOn = false;
        }
    }
    public void OnToggleHigh()
    {
        if (high.isOn)
        {
            normal.isOn = false;
            higher.isOn = false;
        }
    }
    public void OnToggleHigher()
    {
        if (higher.isOn)
        {
            high.isOn = false;
            normal.isOn = false;
        }
    }

    // Handles moving to the next scene depending on the tutorial completion.
    public void GoToTheNextScene()
    {
        if (!finished)
        {
            finished = true;
        }
        else
        {
            if (sM.GetBoolPref("user-settings"))
            {
                SceneManager.LoadScene("UserSettingsFontSize");
            }
            else
                SceneManager.LoadScene("Countdown");

        }

    }
}

