using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSceneMenager : Singleton<MainMenuMenager>
{
    [SerializeField] Image resultsFrame;
    [SerializeField] Image countdownFrame;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

        resultsFrame.gameObject.SetActive(true);
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
