using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

// Klasa zarządzająca wyborem folderu i ilości obrazów w aplikacji.
public class SelectedFileMenager : MonoBehaviour
{

    [SerializeField] TMP_Dropdown foldersDropdown;
    [SerializeField] TMP_Dropdown imagesCountDropdown;
    [SerializeField] RawImage rawImage;
    [SerializeField] SettingsMenager sM;
    public event Action<int> OnSelectedValueChanged;

    // Metoda wywoływana przy starcie aplikacji.
    void Start()
    {
        UpdateFoldersList();
        sM.UpdateFolderData();
        GetFoldersSampleImage();
        //UpdateImagesCountDropdownStart();
    }

    // Metoda aktualizująca listę dostępnych folderów.
    public void UpdateFoldersList()
    {
        string[] usedList = GetFoldersList();
        sM.UpdateFolderData();
        foldersDropdown.onValueChanged.AddListener(HandleSelectedValueChanged);
        InitValues(usedList);
    }

    // Metoda wywoływana podczas zniszczenia obiektu. Usuwa nasłuchiwanie zdarzeń zmiany wartości w rozwijanej liście folderów.
    void OnDestroy()
    {
        foldersDropdown.onValueChanged.RemoveListener(HandleSelectedValueChanged);
    }

    // Inicjalizuje wartości w rozwijanej liście folderów na podstawie dostępnych etykiet.
    public void InitValues(string[] optionLabels)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < optionLabels.Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(optionLabels[i]));
        }
        foldersDropdown.options = options;
        int selected = 0;


        for (int i = 0; i < foldersDropdown.options.Count; i++)
        {
            if (foldersDropdown.options[i].text == PlayerPrefs.GetString("images-folder-name"))
            {
                selected = i;
            }
        }

        foldersDropdown.value = selected;
    }

    // Aktualizuje listę folderów po dodaniu nowego folderu i ustawia wybrany folder w rozwijanej liście.
    public void UpdateFoldersListAfterNewFolderAdded(string newFolderName)
    {
        string[] optionLabels = GetFoldersList();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < optionLabels.Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(optionLabels[i]));
        }
        foldersDropdown.options = options;
        int selected = 0;


        for (int i = 0; i < foldersDropdown.options.Count; i++)
        {
            if (foldersDropdown.options[i].text == newFolderName)
            {
                selected = i;
            }
        }

        sM.UpdateFolderData();
        foldersDropdown.value = selected;
        GetFoldersSampleImage();
    }

    // Obsługuje zmianę wartości w rozwijanej liście folderów i wywołuje zdarzenie OnSelectedValueChanged.
    private void HandleSelectedValueChanged(int trash)
    {
        OnSelectedValueChanged?.Invoke(foldersDropdown.value);
    }

    // Pobiera listę folderów dostępnych w aplikacji.
    public string[] GetFoldersList()
    {
        string fullPath;
        string projectName;
        List<string> folderNames = new();
        string[] dir = Directory.GetDirectories(UnityEngine.Application.persistentDataPath + "/data");

        foreach (string d in dir)
        {
            fullPath = Path.GetFullPath(d).TrimEnd(Path.DirectorySeparatorChar);
            projectName = fullPath.Split(Path.DirectorySeparatorChar).Last();

            folderNames.Add(projectName);
        }
        string[] returnFolderList = folderNames.ToArray();

        return returnFolderList;
    }

    // Pobiera przykładowy obraz dla wybranego folderu i aktualizuje obraz w interfejsie.
    public void GetFoldersSampleImage()
    {        
        String folderName = foldersDropdown.options[foldersDropdown.value].text;

        FileDownloader fd = new FileDownloader();
        List<Texture2D> selectedFilesImages = fd.LoadTexturesFromFolder(folderName);

        if (selectedFilesImages.Count >= 6)
        {
            rawImage.texture = selectedFilesImages[0];
            UpdateImagesCountDropdown(selectedFilesImages);
        }
        else
        {
            Debug.Log("unable to load foders image");
            rawImage.texture = default(Texture2D);
            string[] usedList = GetFoldersList();
            InitValues(usedList);
        }
            
    }


    // Metoda do aktualizacji rozwijanego menu z ilością obrazów
    public void UpdateImagesCountDropdown(List<Texture2D> selectedFilesImages)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Dodaj opcje dla ilości obrazów, zaczynając od 6 i kończąc na ilości dostępnych obrazów
        for (int i = 6; i <= selectedFilesImages.Count; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        imagesCountDropdown.options = options;

        // Ustaw wartość rozwijanego menu na ilość dostępnych obrazów
        imagesCountDropdown.value = selectedFilesImages.Count;
    }

    // Metoda do początkowej aktualizacji rozwijanego menu z ilością obrazów na podstawie danych z PlayerPrefs
    public void UpdateImagesCountDropdownStart()
    {
        int selected=0;

        // Przeszukaj opcje rozwijanego menu, aby znaleźć indeks zapisanej liczby obrazów w PlayerPrefs
        for (int i = 0; i < imagesCountDropdown.options.Count; i++)
        {
            if (imagesCountDropdown.options[i].text == PlayerPrefs.GetString("images-number"))
            {
                selected = i;
            }
        }

        // Ustaw wartość rozwijanego menu na znaleziony indeks
        imagesCountDropdown.value = selected;
    }

}
