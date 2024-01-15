using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class managing the countdown before transitioning to the main test scene.
/// </summary>
public class CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;// Reference to the TextMeshPro text component displaying the countdown.
    public bool countdownFinished = false; // Flag indicating whether the countdown has finished.

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        countdownFinished = false;
        StartCoroutine(ShowNewImage());
    }
    /// <summary>
    /// Coroutine to handle the countdown and transition logic.
    /// </summary>
    /// <returns>IEnumerator for yielding WaitForSeconds.</returns>
    IEnumerator ShowNewImage()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            counter.text = countdown.ToString();// Update the text with the current countdown value.
            yield return new WaitForSeconds(1.3f); // Wait for 1.3 seconds.
            countdown--;
        }

        countdownFinished = true; // Set the countdownFinished flag to true after the countdown is complete.
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if (countdownFinished)
        { 
            SceneManager.LoadScene("Test");// Transition to the main test scene when the countdown is finished.
        }
        if (Input.GetKeyDown("escape"))
            Application.Quit(); // Quit the application if the Escape key is pressed.
    }
}
