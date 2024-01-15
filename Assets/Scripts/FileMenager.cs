using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleFileBrowser;
using TMPro;
using System;
using Unity.VisualScripting;

/// <summary>
/// Class responsible for managing files in the application.
/// </summary>
public class FileMenager : MonoBehaviour
{
    /// <summary>
    /// References to other managers and components in the scene.
    /// </summary>    
    public SelectedFileMenager sFM;
    public ShowResults sR;

    /// <summary>
    /// Reference to the text input field for entering folder names.
    /// </summary>
    [SerializeField] TMP_InputField folderNameInput;

    /// <summary>
    /// Handles the attempt to open the file browser for a folder.
    /// </summary>
    [Obsolete]
    public void TryOpeningFileBrowser()
    {
        // Check if the folder name is not empty.
        if (folderNameInput.text == "")
        {
            Debug.Log("Can't upload an empty file");
            return;       
        }

        // Check if a folder with the given name exists.
        bool fountMatchingFolderName = false;
        foreach (string folderName in sFM.GetFoldersList())
        {
            if (folderName == folderNameInput.text)
                fountMatchingFolderName = true;
        }

        // If a folder with that name is found, display a message and exit the method.
        if (fountMatchingFolderName)
        {
            Debug.Log("Can't upload existing file name");
            return;
        }
        else
            OpenFileBrowser(folderNameInput.text);
    }


    /// <summary>
    /// Handles the attempt to open the file browser for results.
    /// </summary>
    [Obsolete]
    public void TryOpeningResultsFileBrowser()
    {
        OpenResultsBrowser();
    }

    /// <summary>
    /// Opens the file browser for a specific folder.
    /// </summary>
    /// <param name="folderName">Folder name.</param>
    [Obsolete]
    public void OpenFileBrowser(string folderName)
    {
        // Default file name.
        String fileName ="default";

        // Set filters for image files.
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");

        // Start coroutine responsible for displaying the file browser dialog.
        StartCoroutine(ShowLoadDialogCoroutine());


        /// <summary>
        /// Coroutine responsible for displaying the file browser dialog.
        /// </summary>
        IEnumerator ShowLoadDialogCoroutine()
        {
            // Wait for the selection of files and folders.
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

            // Loop forcing re-selection of files if the number of selected files is less than 6.
            while (FileBrowser.Result.Length < 6)
            {
                yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
            }

            // Check if the operation was successful.
            if (FileBrowser.Success)
            {
                // File downloader object.
                FileDownloader fd = new FileDownloader();

                // Loop processing each selected file.
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                {
                    // Replace forward slashes with backward slashes in the file path.
                    FileBrowser.Result[i] = FileBrowser.Result[i].Replace("\\", "/");

                    Console.ReadLine();

                    // Create a network request to download the texture.
                    UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + FileBrowser.Result[i]);
                    yield return www.SendWebRequest();

                    // Handle texture download errors.
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        // Convert the downloaded texture to a Texture2D object.
                        Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                        // Get the file name from the full path.
                        fileName = Path.GetFileName(FileBrowser.Result[i]);

                        // Save the texture in the specified folder.
                        fd.SaveTexture(myTexture, folderName, fileName);
                    }

                    // Update the folder list after adding a new folder.
                    sFM.UpdateFoldersListAfterNewFolderAdded(folderName);

                    // Clear the text field.
                    folderNameInput.text = "";
                }
            }
            
        }
        
    }

    /// <summary>
    /// Opens the file browser for results.
    /// </summary>
    [Obsolete]
    public void OpenResultsBrowser()
    {
        // Set the default filter to JSON files.
        FileBrowser.SetDefaultFilter(".json");

        // Start coroutine responsible for displaying the file browser dialog.
        SaveSimpleResultsToJson save = new();

        /// <summary>
        /// Coroutine responsible for displaying the file browser dialog.
        /// </summary>
        StartCoroutine(ShowLoadDialogCoroutine());
        
        IEnumerator ShowLoadDialogCoroutine()
        {
            // Ustawienie domyślnej ścieżki do katalogu zapisu.
            string path = UnityEngine.Application.persistentDataPath + "/saved/";

            // Oczekiwanie na wybór pliku.
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, path, null, "Load Files", "Load");

            // Sprawdzenie, czy operacja zakończyła się sukcesem.
            if (FileBrowser.Success)
            {
                // File downloader object.
                FileDownloader fd = new FileDownloader();

                // Load the JSON file and display the results.
                save = fd.LoadSimpleJson(FileBrowser.Result[0]);
                sR.DisplayResults(save);
            }
        }
    }
}
