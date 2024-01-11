using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Klasa zarządzająca głównym menu, dziedziczy z klasy Singleton<MainMenuMenager>
public class MainMenuMenager : Singleton<MainMenuMenager>
{
    [SerializeField] public Toggle settingsToggle; // Przełącznik dla ustawień
    [SerializeField] public Toggle tutorialToggle; // Przełącznik dla tutorialu

    // Metoda do ładowania egzaminu na podstawie ustawień użytkownika
    public void LoadStartExam()
    {
        if (tutorialToggle.isOn)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (settingsToggle.isOn)
        {
            SceneManager.LoadScene("UserSettingsFontSize");

        }
        else
            SceneManager.LoadScene("Countdown");
    }

    // Metoda do ładowania ustawień egzaminatora
    public void LoadExaminatorSettings()
    {
        SceneManager.LoadScene("ExaminatorSettings");
    }

    // Metoda do ładowania wyników
    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
    }

    // Metoda do wyjścia z programu
    public void ExitProgram()
    {
        Application.Quit();
    }

}
