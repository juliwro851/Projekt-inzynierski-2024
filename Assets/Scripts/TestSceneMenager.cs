using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the results scene, inherits from the Singleton<MainMenuMenager> class.
/// </summary>
public class ResultsSceneMenager : Singleton<MainMenuMenager>
{
    [SerializeField] Image countdownFrame; // Image of the countdown frame
    [SerializeField] ShowResults sR;  // Object of the ShowResults class for displaying results

    /// <summary>
    /// Method to load the main menu.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    /// <summary>
    /// Method to load results (marked as obsolete).
    /// </summary>
    [System.Obsolete]
    public void LoadResults()
    {
        sR.SelectNewProgram(); // Call the SelectNewProgram() method on the sR object
}

    /// <summary>
    /// Method called every frame.
    /// </summary>
    private void Update()
    {
        // Check if the "Escape" key has been pressed
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu"); // Load the main menu
    }


}
