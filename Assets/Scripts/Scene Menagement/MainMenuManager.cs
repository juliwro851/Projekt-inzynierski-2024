using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class managing the main menu, inheriting from the Singleton<MainMenuMenager> class.
/// </summary>
public class MainMenuMenager : Singleton<MainMenuMenager>
{
    [SerializeField] public Toggle settingsToggle; // Toggle for settings
    [SerializeField] public Toggle tutorialToggle; // Toggle for tutorial

    /// <summary>
    /// Loads the start of the exam based on user settings.
    /// </summary>
    public void LoadStartExam()
    {
        if (tutorialToggle.isOn)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (settingsToggle.isOn)
        {
            SceneManager.LoadScene("UserSettingsFontSize");

        }
        else
            SceneManager.LoadScene("Countdown");
    }

    /// <summary>
    /// Loads the examiner settings.
    /// </summary>
    public void LoadExaminatorSettings()
    {
        SceneManager.LoadScene("ExaminatorSettings");
    }

    /// <summary>
    /// Loads the results.
    /// </summary>
    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
    }

    /// <summary>
    /// Exits the program.
    /// </summary>
    public void ExitProgram()
    {
        Application.Quit();
    }

}
