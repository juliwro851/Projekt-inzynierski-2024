using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Class demonstrating voice control functionality using the KeywordRecognizer.
/// </summary>
public class VoiceControllDemo : MonoBehaviour
{
    /// <summary>
    /// KeywordRecognizer for recognizing voice commands.
    /// </summary>
    private KeywordRecognizer keywordRecognizer;

    /// <summary>
    /// Dictionary to map voice commands to corresponding action methods.
    /// </summary>
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    /// <summary>
    /// Method called when the script starts.
    /// </summary>
    private void Start()
    {
        // Populate the dictionary with voice commands and corresponding actions
        Debug.Log("On");
        dictionary.Add("level", Left);
        dictionary.Add("problem", Right);
        dictionary.Add("ok", Right);
        dictionary.Add("talk", Right);
        dictionary.Add("dalei", Right);

        // Initialize the KeywordRecognizer with the dictionary keys (voice commands)
        keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
        keywordRecognizer.Start();
    }

    /// <summary>
    /// Method called when a voice command is recognized.
    /// </summary>
    /// <param name="speech">Information about the recognized phrase.</param>
    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        // Log the recognized phrase
        Debug.Log(speech.text);

        // Invoke the corresponding action based on the recognized phrase
        dictionary[speech.text].Invoke();
    }

    /// <summary>
    /// Action method to move the GameObject to the right.
    /// </summary>
    private void Right()
    {
        transform.Translate(1, 0, 0);
    }

    /// <summary>
    /// Action method to move the GameObject to the left.
    /// </summary>
    private void Left()
    {
        transform.Translate(-1, 0, 0);
    }


}
