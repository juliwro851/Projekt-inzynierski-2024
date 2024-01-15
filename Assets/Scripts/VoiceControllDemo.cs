using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Class created to test voice control.
/// </summary>
public class VoiceControllDemo : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer; // Keyword recognizer for voice commands
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    /// <summary>
    /// Method called when the game starts.
    /// </summary>
    private void Start()
    {
        // Adding voice commands and their associated actions to the dictionary
        Debug.Log("On");
        dictionary.Add("level", Left);
        dictionary.Add("problem", Right);
        dictionary.Add("ok", Right);
        dictionary.Add("talk", Right);
        dictionary.Add("dalei", Right);

        // Initializing the keyword recognizer
        keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());

        // Assigning the OnPhraseRecognized method to the OnPhraseRecognized event of the keyword recognizer
        keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;

        // Starting voice command recognition
        keywordRecognizer.Start();
    }

    /// <summary>
    /// Method called after a voice command is recognized.
    /// </summary>
    /// <param name="speech">Information about the recognized phrase.</param>
    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text); // Displaying the recognized voice command in the console
        dictionary[speech.text].Invoke(); // Calling the associated action based on the recognized command
    }

    /// <summary>
    /// Moves the object to the right.
    /// </summary>
    private void Right()
    {
        transform.Translate(1, 0, 0);
    }

    /// <summary>
    /// Moves the object to the left.
    /// </summary>
    private void Left() 
    {
        transform.Translate(-1, 0, 0);
    }

    /// <summary>
    /// Moves the object forward.
    /// </summary>
    private void Foreward()
    {
        transform.Translate(1, 0, 0);
    }

    /// <summary>
    /// Moves the object backward.
    /// </summary>
    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }

    /// <summary>
    /// Raises the object.
    /// </summary>
    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    /// <summary>
    /// Lowers the object.
    /// </summary>
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }


}
