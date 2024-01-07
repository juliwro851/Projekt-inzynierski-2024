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

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Image examFrame;
    [SerializeField] Image mouse;
    [SerializeField] Image mouse2;
    [SerializeField] Image keyboard;
    [SerializeField] Image keyboard2;
    [SerializeField] Image mouseAndKeyboard;
    [SerializeField] Image mouseAndKeyboard2;

    [SerializeField] Button buttonNext;

    [SerializeField] private Image imageFrame;

    public SettingsMenager sM;

    public Toggle normal;
    public Toggle high;
    public Toggle higher;

    private bool finished = false;
    private bool whiteToYellow = false;
    float lerpValue = 0f;
    float lerpDuration = 0.5f;



    void Start()
    {

        examFrame.gameObject.SetActive(false);
        finished = false;

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

    void Update()
    {
        if (finished)
        {
            examFrame.gameObject.SetActive(true);


            if ((Input.GetMouseButtonDown(0) && sM.GetBoolPref("mouse")) || (Input.GetKeyDown(KeyCode.Space) && sM.GetBoolPref("keyboard")))
            {
                whiteToYellow = true;
            }

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

        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu");

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextToggle();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevToggle();
        }


        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GoToTheNextScene();
        }
    }


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

