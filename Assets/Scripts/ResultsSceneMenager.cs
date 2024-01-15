using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the test scene.
/// </summary>
public class TestSceneMenager : Singleton<MainMenuMenager>
{
    /// <summary>
    /// Reference to the Image object in the user interface for results.
    /// </summary>
    [SerializeField] Image resultsFrame;
    [SerializeField] Image countdownFrame;

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        // Load the MainMenu scene.
        SceneManager.LoadScene("MainMenu");

        // Activate the results frame.
        resultsFrame.gameObject.SetActive(true);

    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Check if the 'escape' key is pressed and load the MainMenu scene.
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu");
    }


}
