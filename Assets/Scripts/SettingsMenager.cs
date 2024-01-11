using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Klasa zarządzająca ustawieniami gracza
public class SettingsMenager : MonoBehaviour
{
    #region Settings Component References

    // Referencje do komponentów UI
    public Toggle userTutorial;
    public Toggle userSettings;

    public Toggle normalBrightness;
    public Toggle highBrightness;
    public Toggle higherBrightness;

    public Toggle normalContrast;
    public Toggle highContrast;
    public Toggle higherContrast;

    public Toggle normalFontSize;
    public Toggle bigFontSize;
    public Toggle bigerFontSize;
    public TMP_Text fontSizeSampleText;

    public Toggle normalReactionTime;
    public Toggle slowReactionTime;
    public Toggle slowerReactionTime;

    public Toggle mouse;
    public Toggle keyboard;
    public Toggle voice;

    public TMP_Dropdown foldersDropdown;
    public TMP_Dropdown imagesCountDropdown;

    public TMP_Dropdown imagesMaxTime;
    public TMP_Dropdown imagesMinTime;
    public TMP_Dropdown imagesPercentTime;


    #endregion

    #region Player Pref Key Constance

    // Stałe klucze PlayerPrefs
    private const string USER_TUTORIAL_PREF = "user-tutorial";
    private const string USER_SETTINGS_PREF = "user-settings";
    
    private const string FONT_SIZE_PREF = "font-size";
    private const string DISLECTIC_PREF = "dislectic";

    private const string ALLOW_SAVE_CLICK_AS_PREV_PREF = "allow-save-click-as-prev";

    private const string MOUSE_PREF = "mouse";
    private const string KEYBOARD_PREF = "keyboard";
    private const string VOICE_PREF = "voice";

    private const string BRIGHTNESS_PREF = "brightness";
    private const string CONTRAST_PREF = "contrast";

    private const string IMAGES_FOLDER_NAME_PREF = "images-folder-name";
    private const string IMAGES_NUMBER_PREF = "images-number";
    private const string MAX_TIME_BETWEEN_IMAGES_PREF = "max-time-between-images";
    private const string MIN_TIME_BETWEEN_IMAGES_PREF = "min-time-between-images";
    private const string PART_REPEATED_PERCENT_PREF = "part-repeated-percent";

    #endregion

    #region Monobehavior API

    // Metoda wywoływana przy starcie obiektu
    void Start()
    {
        // Aktualizuj przełączniki, jeśli są dostępne
        if (userSettings && userTutorial)
            UpdateTutorialandSettingsToggles();

        if (normalBrightness)
            UpdateBrightnessToggles();

        if (normalContrast)
            UpdateContrastToggles();

        if (bigerFontSize)
            UpdateFontSizeToggles();

        if (normalReactionTime)
            UpdateAllowSaveClickAsPrevToggles();    

        if (mouse)
            UpdateControllsData();

        if (foldersDropdown)
            UpdateFolderData();

        if (imagesMaxTime)
            UpdateMaxTimeBetweenImages();

        if (imagesMinTime) 
            UpdateMinTimeBetweenImages();

        if (imagesPercentTime) 
            UpdatePartRepeatedPercent();
    }

    #endregion

    #region Allow Save Click As Prev

    // Metoda do aktualizacji przełączników czasu reakcji
    public void UpdateAllowSaveClickAsPrevToggles()
    {
        // Sprawdź zapisany czas reakcji i ustaw odpowiedni przełącznik
        switch (PlayerPrefs.GetFloat("allow-save-click-as-prev"))
        {
            case 0:
                normalReactionTime.isOn = true;
                slowReactionTime.isOn = false;
                slowerReactionTime.isOn = false;
                break;
            case 0.09f:
                normalReactionTime.isOn = false;
                slowReactionTime.isOn = true;
                slowerReactionTime.isOn = false;
                break;

            case 0.13f:
                normalReactionTime.isOn = false;
                slowReactionTime.isOn = false;
                slowerReactionTime.isOn = true;
                break;
        }
    }

    // Metody obsługi zmiany przełączników czasu reakcji
    public void OnToggleNormalReactionTime()
    {
        if (normalReactionTime.isOn)
        {
            SetAllowSaveClickAsPrev(0);
            slowReactionTime.isOn = false;
            slowerReactionTime.isOn = false;
        }
    }
    public void OnToggleSlowReactionTime()
    {
        if (slowReactionTime.isOn)
        {
            SetAllowSaveClickAsPrev(0.9f);
            normalReactionTime.isOn = false;
            slowerReactionTime.isOn = false;
        }

    }
    public void OnToggleSlowerReactionTime()
    {
        if (slowerReactionTime.isOn)
        {
            SetAllowSaveClickAsPrev(1.3f);
            slowReactionTime.isOn = false;
            normalReactionTime.isOn = false;
        }

    }

    // Metoda do ustawiania czasu reakcji w PlayerPrefs
    private void SetAllowSaveClickAsPrev(float reactionTime)
    {
        SetPref(ALLOW_SAVE_CLICK_AS_PREV_PREF, reactionTime);
    }

    #endregion

    #region User Tutorial and Settings

    // Metoda do aktualizacji przełączników tutorialu i ustawień
    public void UpdateTutorialandSettingsToggles()
    {
        userSettings.isOn = GetBoolPref("user-settings");
        userTutorial.isOn = GetBoolPref("user-tutorial");
    }

    // Metody obsługi zmiany przełączników tutorialu i ustawień
    public void OnToggleUserTutorial()
    {
         SetUserTutorial(userTutorial.isOn);
    }
    public void OnToggleUserSettings()
    {
        SetUserSettings(userSettings.isOn);
    }

    // Metoda do ustawiania tutorialu w PlayerPrefs
    private void SetUserTutorial(bool status)
    {
        SetPref(USER_TUTORIAL_PREF, status);

    }

    // Metoda do ustawiania ustawień w PlayerPrefs
    private void SetUserSettings(bool status)
    {
        SetPref(USER_SETTINGS_PREF, status);
    }

    #endregion

    #region Controlls

    // Metoda do aktualizacji danych dotyczących sterowania
    public void UpdateControllsData()
    {
        mouse.isOn = GetBoolPref("mouse");

        keyboard.isOn = GetBoolPref("keyboard");

        voice.isOn = GetBoolPref("voice");

    }

    // Metody obsługi zmiany przełączników sterowania
    public void OnToggleMouse()
    {
        if (mouse.isOn)
        {
            SetMouseControll(true);
        }
        else if (!mouse.isOn && !keyboard.isOn)
        {
            mouse.isOn = true;
        }
        else
        {
            SetMouseControll(false);
        }
    }
    public void OnToggleKeyboard()
    {
        if (keyboard.isOn)
        {
            SetKeyboardControll(true);
        }
        else if (!keyboard.isOn && !mouse.isOn)
        {
            keyboard.isOn = true;
        }
        else
        {
            SetKeyboardControll(false);
        }
    }
    public void OnToggleVoice()
    {
        SetVoiceControll(voice.isOn);
    }

    // Metody do ustawiania sterowania w PlayerPrefs
    private void SetMouseControll(bool status)
    {
        SetPref(MOUSE_PREF, status);
    }
    private void SetKeyboardControll(bool status)
    {
        SetPref(KEYBOARD_PREF, status);
    }
    private void SetVoiceControll(bool status)
    {
        SetPref(VOICE_PREF, status);
    }

    #endregion

    #region Contrast
    // Enumeracja do reprezentacji poziomów kontrastu
    enum Contrast { normal, high, higher }

    // Metoda do aktualizacji przełączników kontrastu
    public void UpdateContrastToggles()
    {
        switch (PlayerPrefs.GetInt("contrast"))
        {
            case 1:
                normalContrast.isOn = true;
                highContrast.isOn = false;
                higherContrast.isOn = false;
                break;
            case 35:
                normalContrast.isOn = false;
                highContrast.isOn = true;
                higherContrast.isOn = false;
                break;

            case 70:
                normalContrast.isOn = false;
                highContrast.isOn = false;
                higherContrast.isOn = true;
                break;
        }
    }

    // Metody obsługi zmiany przełączników kontrastu
    public void OnToggleNormalContrast()
    {
        if (normalContrast.isOn == true)
        {
            SetContrast(Contrast.normal);
            highContrast.isOn = false;
            higherContrast.isOn = false;
        }

    }
    public void OnToggleHighContrast()
    {
        if (highContrast.isOn)
        {
            SetContrast(Contrast.high);
            normalContrast.isOn = false;
            higherContrast.isOn = false;
        }

    }
    public void OnToggleHigherlContrast()
    {
        if (higherContrast.isOn == true)
        {
            SetContrast(Contrast.higher);
            highContrast.isOn = false;
            normalContrast.isOn = false;
        }


    }

    // Metoda do ustawiania kontrastu w PlayerPrefs
    private void SetContrast(Contrast contrast)
    {
        if (contrast == Contrast.normal)
        {
            SetPref(CONTRAST_PREF, 1);
        }
        else if (contrast == Contrast.high)
        {
            SetPref(CONTRAST_PREF, 35);
        }
        else
        {
            SetPref(CONTRAST_PREF, 70);
        }

    }
    #endregion

    #region Brightness

    // Metoda do aktualizacji przełączników jasności
    public void UpdateBrightnessToggles()
    {
        switch (PlayerPrefs.GetFloat("brightness"))
        {
            case 1:
                normalBrightness.isOn = true;
                highBrightness.isOn = false;
                higherBrightness.isOn = false;
                break;
            case 1.4f:
                normalBrightness.isOn = false;
                highBrightness.isOn = true;
                higherBrightness.isOn = false;
                break;

            case 1.9f:
                normalBrightness.isOn = false;
                highBrightness.isOn = false;
                higherBrightness.isOn = true;
                break;
        }
    }

    // Metody obsługi zmiany przełączników jasności
    public void OnToggleNormalBrightness()
    {
        if (normalBrightness.isOn)
        {
            SetBrightness(1);
            highBrightness.isOn = false;
            higherBrightness.isOn = false;
        }
    }
    public void OnToggleHighBrightness()
    {
        if (highBrightness.isOn)
        {
            SetBrightness(1.4f);
            normalBrightness.isOn = false;
            higherBrightness.isOn = false;
        }

    }
    public void OnToggleHigherlBrightness()
    {
        if (higherBrightness.isOn)
        {
            SetBrightness(1.9f);
            highBrightness.isOn = false;
            normalBrightness.isOn = false;
        }

    }

    // Metoda do ustawiania jasności w PlayerPrefs
    private void SetBrightness(float brightness)
    {
        SetPref(BRIGHTNESS_PREF, brightness);
    }
    #endregion

    #region FontSize

    // Enumeracja do reprezentacji rozmiarów czcionki
    enum FontSize { normal, big, bigger }

    // Metoda do aktualizacji przełączników rozmiaru czcionki
    public void UpdateFontSizeToggles()
    {
        switch (PlayerPrefs.GetInt("font-size"))
        {
            case 0:
                normalFontSize.isOn = true;
                bigFontSize.isOn = false;
                bigerFontSize.isOn = false;
                break;
            case 10:
                normalFontSize.isOn = false;
                bigFontSize.isOn = true;
                bigerFontSize.isOn = false;
                break;

            case 25:
                normalFontSize.isOn = false;
                bigFontSize.isOn = false;
                bigerFontSize.isOn = true;
                break;
        }
    }

    // Metody obsługi zmiany przełączników rozmiaru czcionki
    public void OnToggleFontSizeNormal()
    {
        if (normalFontSize.isOn == true)
        {
            SetFontSize(FontSize.normal);
            bigFontSize.isOn = false;
            bigerFontSize.isOn = false;
        }
    }
    public void OnToggleFontSizeBig()
    {
        if (bigFontSize.isOn == true)
        {
            SetFontSize(FontSize.big);
            normalFontSize.isOn = false;
            bigerFontSize.isOn = false;
        }

    }
    public void OnToggleFontSizeBigger()
    {
        if (bigerFontSize.isOn == true)
        {
            SetFontSize(FontSize.bigger);
            normalFontSize.isOn = false;
            bigFontSize.isOn = false;
        }

    }

    // Metoda do ustawiania rozmiaru czcionki w PlayerPrefs
    private void SetFontSize(FontSize fontSize)
    {
        if (fontSize == FontSize.normal)
        {
            if (PlayerPrefs.GetInt("font-size") == 10)
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize - 10;
            else
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize - 25;

            SetPref(FONT_SIZE_PREF, 0);
        }
        else if (fontSize == FontSize.big)
        {
            if (PlayerPrefs.GetInt("font-size") == 25)
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize - 15;
            else
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize + 10;

            SetPref(FONT_SIZE_PREF, 10);
        }
        else
        {
            if (PlayerPrefs.GetInt("font-size") == 10)
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize + 15;
            else
                fontSizeSampleText.fontSize = fontSizeSampleText.fontSize + 25;

            SetPref(FONT_SIZE_PREF, 25);
        }

    }

    #endregion

    #region FolderName
    // Metoda do aktualizacji danych dotyczących folderu
    public void UpdateFolderData()
    {
        for (int i = 0; i < foldersDropdown.options.Count; i++)
        {
            if (foldersDropdown.options[i].text == PlayerPrefs.GetString("images-folder-name"))
            {
                foldersDropdown.value = i;
            }
        }
        SetImagesCount(PlayerPrefs.GetInt("images-number"));

    }

    // Metoda obsługi zmiany wartości w rozwijanym menu dotyczącym folderu
    public void OnDropdownFolderName()
    {
        SetFolderName(foldersDropdown.options[foldersDropdown.value].text);
    }
    // Metoda do ustawiania nazwy folderu w PlayerPrefs
    public void SetFolderName(string folderName)
    {
        SetPref(IMAGES_FOLDER_NAME_PREF, folderName);

    }
    #endregion

    #region ImagesCount

    // Metoda obsługi zmiany wartości w rozwijanym menu dotyczącym liczby obrazów
    public void OnDropdownImagesCount()
    {
        SetImagesCount(int.Parse(imagesCountDropdown.options[imagesCountDropdown.value].text));
    }

    // Metoda do ustawiania liczby obrazów w PlayerPrefs
    private void SetImagesCount(int imagesCount)
    {
        SetPref(IMAGES_NUMBER_PREF, imagesCount);
    }
    #endregion

    #region TimeBetweenPhotos

    // Metoda do aktualizacji maksymalnego czasu między obrazami
    public void UpdateMaxTimeBetweenImages()
    {
        if (float.Parse(imagesMaxTime.options[imagesMaxTime.value].text) >= float.Parse(imagesMinTime.options[imagesMinTime.value].text))
        {
            SetMaxTimeBetweenImages(float.Parse(imagesMaxTime.options[imagesMaxTime.value].text));
        }
        else
        {
            imagesMaxTime.value = 5;
        }
        
    }

    // Metoda do aktualizacji minimalnego czasu między obrazami
    public void UpdateMinTimeBetweenImages()
    {
        if (float.Parse(imagesMaxTime.options[imagesMaxTime.value].text) >= float.Parse(imagesMinTime.options[imagesMinTime.value].text))
        {
            SetMinTimeBetweenImages(float.Parse(imagesMinTime.options[imagesMinTime.value].text));
        }
        else
        {
            imagesMinTime.value = 1;
        }
    }

    // Metoda do ustawiania maksymalnego czasu między obrazami w PlayerPrefs
    private void SetMaxTimeBetweenImages(float maxTime)
    {
        SetPref(MAX_TIME_BETWEEN_IMAGES_PREF, maxTime);

    }

    // Metoda do ustawiania minimalnego czasu między obrazami w PlayerPrefs
    private void SetMinTimeBetweenImages(float minTime)
    {
        SetPref(MIN_TIME_BETWEEN_IMAGES_PREF, minTime);

    }
    #endregion

    #region ImagesRepeatedPercent

    // Metoda do aktualizacji procentu czasu powtarzania obrazów
    public void UpdatePartRepeatedPercent()
    {
        SetPref(PART_REPEATED_PERCENT_PREF, float.Parse(imagesPercentTime.options[imagesPercentTime.value].text));
    }
    #endregion

    #region Pref Setters

    // Metoda do ustawiania preferencji liczbowych w PlayerPrefs
    private void SetPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key,value);
    }

    // Metoda do ustawiania preferencji tekstowych w PlayerPrefs
    private void SetPref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    // Metoda do ustawiania preferencji liczbowych całkowitoliczbowych w PlayerPrefs
    private void SetPref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    // Metoda do ustawiania preferencji boolowskich w PlayerPrefs
    private void SetPref(string key, bool value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    // Metoda do pobierania preferencji boolowskich z PlayerPrefs
    public bool GetBoolPref(string key, bool defaultValue = true)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }

    #endregion
}
