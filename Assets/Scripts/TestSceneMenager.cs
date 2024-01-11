using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Klasa zarządzająca sceną wyników, dziedziczy z klasy Singleton<MainMenuMenager>
public class ResultsSceneMenager : Singleton<MainMenuMenager>
{
    [SerializeField] Image countdownFrame; // Obrazek ramki odliczania
    [SerializeField] ShowResults sR;  // Obiekt klasy ShowResults do wyświetlania wyników

    // Metoda do ładowania głównego menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    // Metoda do ładowania wyników (metoda oznaczona jako przestarzała)
    [System.Obsolete]
    public void LoadResults()
    {
        sR.SelectNewProgram();  // Wywołanie metody SelectNewProgram() na obiekcie sR

    }

    // Metoda wywoływana co klatkę
    private void Update()
    {
        // Sprawdzenie, czy został naciśnięty klawisz "Escape"
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu"); // Wczytanie głównego menu
    }


}
