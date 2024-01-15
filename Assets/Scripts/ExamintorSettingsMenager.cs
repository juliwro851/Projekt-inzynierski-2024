using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Class managing the settings for the examiner.
/// </summary>
public class ExaminatorSettingsMenager : Singleton<ExaminatorSettingsMenager>
{
    /// <summary>
    /// Loads the results scene.
    /// </summary>
    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
        //SceneManager.Instance.LoadScene(SceneEnum.MainMenu);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
