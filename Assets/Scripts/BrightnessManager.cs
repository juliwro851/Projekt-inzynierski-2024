using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


// Klasa zarządzająca ustawieniami jasności i kontrastu za pomocą efektów post-processingu
public class BrightnessManager : MonoBehaviour
{
    // Referencje do profili post-processingu i warstwy post-processingu
    public PostProcessProfile brightness;
    public PostProcessProfile contrast;
    public PostProcessLayer layer;

    // Referencje do ustawień AutoExposure i ColorGrading
    AutoExposure exposure;
    ColorGrading contr;

    // Metoda wywoływana przy starcie obiektu
    void Start()
    {
        // Sprawdź, czy aktualna scena to "UserSettingsBrightness"
        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
        {
            // Jeśli tak, pobierz ustawienia AutoExposure
            brightness.TryGetSettings(out exposure);

            // Dostosuj jasność
            AdjustBrightness();
        }
        else
        {
            // Jeśli nie, pobierz ustawienia ColorGrading
            contrast.TryGetSettings(out contr);

            // Dostosuj kontrast
            AdjustContrast();
        }
    }

    // Metoda do dostosowywania jasności
    public void AdjustBrightness()
    {
        // Ustaw jasność na zapisaną wartość, jeśli jest większa lub równa 1; w przeciwnym razie ustaw na 1
        if (PlayerPrefs.GetFloat("brightness")>=1)
            exposure.keyValue.value = PlayerPrefs.GetFloat("brightness");
        else
            exposure.keyValue.value = 1;

    }

    // Metoda do dostosowywania kontrastu
    public void AdjustContrast()
    {
        // Pobierz ustawiony przez gracza kontrast z PlayerPrefs i staw kontrast na zapisaną wartość
        contr.contrast.value = PlayerPrefs.GetInt("contrast");
    }
}
