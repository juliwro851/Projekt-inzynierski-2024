using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class managing the selection of a folder and the number of images in the application.
/// </summary>
public class SelectedFileMenager : MonoBehaviour
{

    [SerializeField] TMP_Dropdown foldersDropdown;
    [SerializeField] TMP_Dropdown imagesCountDropdown;
    [SerializeField] RawImage rawImage;
    [SerializeField] SettingsMenager sM;
    public event Action<int> OnSelectedValueChanged;

    /// <summary>
    /// Method called on application start.
    /// </summary>
    void Start()
    {
        UpdateFoldersList();
        sM.UpdateFolderData();
        GetFoldersSampleImage();
        //UpdateImagesCountDropdownStart();
    }

    /// <summary>
    /// Method updating the list of available folders.
    /// </summary>
    public void UpdateFoldersList()
    {
        string[] usedList = GetFoldersList();
        sM.UpdateFolderData();
        foldersDropdown.onValueChanged.AddListener(HandleSelectedValueChanged);
        InitValues(usedList);
    }

    /// <summary>
    /// Method called when the object is destroyed. Removes the event listener for value changes in the folder dropdown.
    /// </summary>
    void OnDestroy()
    {
        foldersDropdown.onValueChanged.RemoveListener(HandleSelectedValueChanged);
    }

    /// <summary>
    /// Initializes values in the folder dropdown based on the available labels.
    /// </summary>
    /// <param name="optionLabels">Array of folder names.</param>
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

    /// <summary>
    /// Updates the folder list after adding a new folder and sets the selected folder in the dropdown list.
    /// </summary>
    /// <param name="newFolderName">Name of the newly added folder.</param>
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

    /// <summary>
    /// Handles the value change in the folder dropdown and invokes the OnSelectedValueChanged event.
    /// </summary>
    /// <param name="trash">Unused parameter.</param>
    private void HandleSelectedValueChanged(int trash)
    {
        OnSelectedValueChanged?.Invoke(foldersDropdown.value);
    }

    /// <summary>
    /// Gets the list of available folders.
    /// </summary>
    /// <returns>Array of folder names.</returns>
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

    /// <summary>
    /// Gets a sample image for the selected folder and updates the image in the interface.
    /// </summary>
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


    /// <summary>
    /// Updates the dropdown menu with the number of images.
    /// </summary>
    /// <param name="selectedFilesImages">List of selected images.</param>
    public void UpdateImagesCountDropdown(List<Texture2D> selectedFilesImages)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Add options for the number of images, starting from 6 and ending at the available number of images
        for (int i = 6; i <= selectedFilesImages.Count; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        imagesCountDropdown.options = options;

        // Set the dropdown value to the available number of images
        imagesCountDropdown.value = selectedFilesImages.Count;
    }

    /// <summary>
    /// Initial update of the dropdown menu with the number of images based on PlayerPrefs data.
    /// </summary>
    public void UpdateImagesCountDropdownStart()
    {
        int selected=0;

        // Search dropdown options to find the index of the saved number of images in PlayerPrefs
        for (int i = 0; i < imagesCountDropdown.options.Count; i++)
        {
            if (imagesCountDropdown.options[i].text == PlayerPrefs.GetString("images-number"))
            {
                selected = i;
            }
        }

        // Set the dropdown value to the found index
        imagesCountDropdown.value = selected;
    }

}
