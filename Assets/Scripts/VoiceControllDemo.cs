using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControllDemo : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    private void Start()
    {

        Debug.Log("On");
        dictionary.Add("level", Left);
        dictionary.Add("problem", Right);
        dictionary.Add("ok", Right);
        dictionary.Add("talk", Right);
        dictionary.Add("dalei", Right);

        keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
        keywordRecognizer.Start();
    }

    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        dictionary[speech.text].Invoke();
    }

    private void Right()
    {
        transform.Translate(1, 0, 0);
    }
    private void Left()
    {
        transform.Translate(-1, 0, 0);
    }
    private void Foreward()
    {
        transform.Translate(1, 0, 0);
    }
    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }
    private void Up()
    {
        transform.Translate(0, 1, 0);
    }
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }


}
