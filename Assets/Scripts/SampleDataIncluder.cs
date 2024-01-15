using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class responsible for adding sample data to the project.
/// </summary>
public class SampleDataIncluder : MonoBehaviour
{
    /// <summary>
    /// List of textures for different categories (fruits).
    /// </summary>
    [SerializeField] public List<Texture2D> fruitTextures = new List<Texture2D>();

    /// <summary>
    /// List of textures for different categories (animals).
    /// </summary>
    [SerializeField] public List<Texture2D> animalTextures;

    /// <summary>
    /// Reference to the settings manager.
    /// </summary>
    public SettingsMenager sM;

    /// <summary>
    /// Awake method called before Start, initializes the directory for storing data.
    /// </summary>
    private void Awake()
    {
        if (!Directory.Exists(Path.GetDirectoryName(UnityEngine.Application.persistentDataPath + "/data/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(UnityEngine.Application.persistentDataPath + "/data/"));
        }
    }

    /// <summary>
    /// Start method called at the beginning of the script's execution.
    /// </summary>
    void Start()
    {
        // Check the existence of folders for different texture categories.
        bool fruitsFolderExists = false;
        bool animalsFolderExists = false;

        // Check if folders "Owoce" and "Zwierzaki" exist.
        foreach (string folderName in GetFoldersList())
        {
            if (folderName == "Owoce")
                fruitsFolderExists = true;
            if (folderName == "Zwierzaki")
                animalsFolderExists = true;
        }

        // If the "Owoce" folder is missing, create it and add sample textures to it.
        if (!fruitsFolderExists)
            CreateSampleFolder("Owoce", fruitTextures);

        // If the "Zwierzaki" folder is missing, create it and add sample textures to it.
        if (!animalsFolderExists)
            CreateSampleFolder("Zwierzaki", animalTextures);
    }

    /// <summary>
    /// Method for creating a folder with sample textures.
    /// </summary>
    /// <param name="folderName">Name of the folder.</param>
    /// <param name="textures">List of textures to be added.</param>
    private void CreateSampleFolder(string folderName, List<Texture2D> textures)
    {
        Debug.Log("Adding sample folder " + folderName);

        // Object for downloading files from the internet.
        FileDownloader fd = new FileDownloader();

        // Save each texture in the folder.
        foreach (Texture2D tex in textures)
        {
            fd.SaveTexture(tex, folderName, tex.name+".jpg");
        }

        // Set the active folder as the last added one.
        sM.SetFolderName(folderName);

    }

    /// <summary>
    /// Method for retrieving the list of folders with data.
    /// </summary>
    /// <returns>Array of folder names.</returns>
    private string[] GetFoldersList()
    {
        string fullPath;
        string projectName;
        List<string> folderNames = new();
        string[] dir = null ;
        string[] returnFolderList = null;

        // Get the list of folders in the data directory.
        dir = Directory.GetDirectories(UnityEngine.Application.persistentDataPath + "/data/");
        foreach (string d in dir)
        {
            fullPath = Path.GetFullPath(d).TrimEnd(Path.DirectorySeparatorChar);
            projectName = fullPath.Split(Path.DirectorySeparatorChar).Last();

            folderNames.Add(projectName);
        }
        returnFolderList = folderNames.ToArray();

        

        return returnFolderList;
    }
}
