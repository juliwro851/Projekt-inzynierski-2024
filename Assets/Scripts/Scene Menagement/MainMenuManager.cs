using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMenager : Singleton<MainMenuMenager>
{
    [SerializeField] public Toggle settingsToggle;
    [SerializeField] public Toggle tutorialToggle;
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
        //get toggle
    }
    public void LoadExaminatorSettings()
    {
        SceneManager.LoadScene("ExaminatorSettings");
        //SceneManager.Instance.LoadScene(SceneEnum.ExaminatorSettings);
    }

    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
        //SceneManager.Instance.LoadScene(SceneEnum.MainMenu);
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

}
