using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExaminatorSettingsMenager : Singleton<ExaminatorSettingsMenager>
{

    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
        //SceneManager.Instance.LoadScene(SceneEnum.MainMenu);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
