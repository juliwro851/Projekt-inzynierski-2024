using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class UserSettingsMenager : MonoBehaviour
{
    [SerializeField] SettingsMenager Sm;
    [SerializeField] Button buttonNext;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "UserSettingsFontSize" && Sm.GetBoolPref("voice"))
        {
            dictionary.Add("level", Left);
            dictionary.Add("left", Left);
            dictionary.Add("problem", Right); 
            dictionary.Add("right", Right); 
            dictionary.Add("dalei", Ok);
            dictionary.Add("next", Ok);

            Debug.Log("voice on");

            keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
            keywordRecognizer.Start();
        }


        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
        {
            Sm.UpdateBrightnessToggles();
        }
        else if (SceneManager.GetActiveScene().name == "UserSettingsContrast")
        {
            Sm.UpdateContrastToggles();

        }
        else
            Sm.UpdateFontSizeToggles();

    }

    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        dictionary[speech.text].Invoke();
    }

    private void Right()
    {
        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
            NextBrightness();
        else if (SceneManager.GetActiveScene().name == "UserSettingsContrast")
            NextContrast();
        else
            NextFontSize();
    }
    private void Left()
    {
        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
            PrevBrightness();
        else if (SceneManager.GetActiveScene().name == "UserSettingsContrast")
            PrevContrast();
        else
            PrevFontSize();
    }
    private void Ok()
    {
        GoToTheNextScene();
    }


    void Update()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
                NextBrightness();
            else if (SceneManager.GetActiveScene().name == "UserSettingsContrast")
                NextContrast();
            else
                NextFontSize();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
                PrevBrightness();
            else if (SceneManager.GetActiveScene().name == "UserSettingsContrast")
                PrevContrast();
            else
                PrevFontSize();
        }


        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || (Input.GetKeyDown(KeyCode.Space) && Sm.GetBoolPref("keyboard")))
        {
            GoToTheNextScene();
        }
    }

    private void NextBrightness()
    {
        if (Sm.normalBrightness.isOn)
        {
            Sm.highBrightness.isOn = true;
            Sm.OnToggleHighBrightness();
        }
        else if (Sm.highBrightness.isOn)
        {
            Sm.higherBrightness.isOn = true;
            Sm.OnToggleHigherlBrightness();
        }

    }

    private void NextContrast()
    {
        if (Sm.normalContrast.isOn)
        {
            Sm.highContrast.isOn = true;
            Sm.OnToggleHighContrast();
        }
        else if (Sm.highContrast.isOn)
        {
            Sm.higherContrast.isOn = true;
            Sm.OnToggleHigherlContrast();
        }
    }

    private void NextFontSize()
    {
        if (Sm.normalFontSize.isOn)
        {
            Sm.bigFontSize.isOn = true;
            Sm.OnToggleFontSizeBig();
        }
        else if (Sm.bigFontSize.isOn)
        {
            Sm.bigerFontSize.isOn = true;
            Sm.OnToggleFontSizeBigger();
        }
    }

    private void PrevBrightness()
    {
        if (Sm.higherBrightness.isOn)
        {
            Sm.highBrightness.isOn = true;
            Sm.OnToggleHighBrightness();
        }
        else if (Sm.highBrightness.isOn)
        {
            Sm.normalBrightness.isOn = true;
            Sm.OnToggleNormalBrightness();
        }
    }

    private void PrevContrast()
    {
        if (Sm.higherContrast.isOn)
        {
            Sm.highContrast.isOn = true;
            Sm.OnToggleHighContrast();
        }
        else if (Sm.highContrast.isOn)
        {
            Sm.normalContrast.isOn = true;
            Sm.OnToggleNormalContrast();
        }
    }

    private void PrevFontSize()
    {
        if (Sm.bigerFontSize.isOn)
        {
            Sm.bigFontSize.isOn = true;
            Sm.OnToggleFontSizeBig();
        }
        else if (Sm.bigFontSize.isOn)
        {
            Sm.normalFontSize.isOn = true;
            Sm.OnToggleFontSizeNormal();
        }
    }


    public void GoToTheNextScene()
    {
        if (SceneManager.GetActiveScene().name == "UserSettingsFontSize")
        {
            SceneManager.LoadScene("UserSettingsBrightness");
        }
        else if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
        {
            SceneManager.LoadScene("UserSettingsContrast");
        }
        else
        {
            SceneManager.LoadScene("Countdown");
        }
    }
}
