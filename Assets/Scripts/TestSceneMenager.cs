using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsSceneMenager : Singleton<MainMenuMenager>
{
    [SerializeField] Image countdownFrame;
    [SerializeField] ShowResults sR;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadResults()
    {
        sR.SelectNewProgram();

        //get toggle
    }

    private void Update()
    {
        //if (Input.GetKeyDown("escape"))
        //    Application.Quit();

        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu");
    }


}
