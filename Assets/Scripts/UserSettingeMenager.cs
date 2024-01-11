using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

// Klasa zarządzająca ustawieniami użytkownika
public class UserSettingsMenager : MonoBehaviour
{
    // Referencja do menedżera ustawień (SettingsManager) i przycisku w interfejsie użytkownika.
    [SerializeField] SettingsMenager Sm;
    [SerializeField] Button buttonNext;

    // Obiekt do rozpoznawania słów kluczowych dla sterowania głosowego.
    private KeywordRecognizer keywordRecognizer;

    // Słownik przechowujący słowa kluczowe i odpowiadające im akcje.
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    // Metoda wywoływana przy starcie sceny.
    void Start()
    {
        // Inicjalizacja rozpoznawania głosu, jeśli jest włączone w ustawieniach i scena to "UserSettingsFontSize"
        if (SceneManager.GetActiveScene().name == "UserSettingsFontSize" && Sm.GetBoolPref("voice"))
        {
            dictionary.Add("level", Left);
            dictionary.Add("left", Left);
            dictionary.Add("pravel", Right); 
            dictionary.Add("right", Right); 
            dictionary.Add("dalei", Ok);
            dictionary.Add("next", Ok);

            Debug.Log("voice on");

            // Inicjalizacja rozpoznawania słów kluczowych
            keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
            keywordRecognizer.Start();
        }

        // Aktualizacja przełączników jasności, kontrastu i rozmiaru czcionki w zależności od aktywnej sceny
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

    // Obsługa zdarzenia rozpoznania słowa kluczowego
    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        dictionary[speech.text].Invoke();
    }

    // Funkcje do nawigacji w ustawieniach
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

    // Funkcje obsługujące ruch w prawo, lewo i potwierdzenie
    // ...

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

    // Funkcje obsługujące zmiany jasności, kontrastu i rozmiaru czcionki
    // ...
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

    // Funkcja do przejścia do następnej sceny
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
