using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class SelectedFileMenager : MonoBehaviour
{

    [SerializeField] TMP_Dropdown foldersDropdown;
    [SerializeField] TMP_Dropdown imagesCountDropdown;
    [SerializeField] RawImage rawImage;
    [SerializeField] SettingsMenager sM;
    public event Action<int> OnSelectedValueChanged;

    void Start()
    {
        UpdateFoldersList();
        sM.UpdateFolderData();
        GetFoldersSampleImage();
    }
    public void UpdateFoldersList()
    {
        string[] usedList = GetFoldersList();
        sM.UpdateFolderData();
        foldersDropdown.onValueChanged.AddListener(HandleSelectedValueChanged);
        InitValues(usedList);
    }


    void OnDestroy()
    {
        foldersDropdown.onValueChanged.RemoveListener(HandleSelectedValueChanged);
    }

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

    private void HandleSelectedValueChanged(int trash)
    {
        OnSelectedValueChanged?.Invoke(foldersDropdown.value);
    }
   

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

    public void UpdateImagesCountDropdown(List<Texture2D> selectedFilesImages)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 6; i <= selectedFilesImages.Count; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        imagesCountDropdown.options = options;

        imagesCountDropdown.value = selectedFilesImages.Count;
    }

}
