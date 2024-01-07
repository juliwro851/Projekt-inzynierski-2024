using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class SampleDataIncluder : MonoBehaviour
{
    [SerializeField] public List<Texture2D> fruitTextures = new List<Texture2D>();
    [SerializeField] public List<Texture2D> animalTextures;

    public SettingsMenager sM;

    private void Awake()
    {
        if (!Directory.Exists(Path.GetDirectoryName(UnityEngine.Application.persistentDataPath + "/data/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(UnityEngine.Application.persistentDataPath + "/data/"));
        }
    }

    void Start()
    {
        bool fruitsFolderExists = false;
        bool animalsFolderExists = false;


        foreach (string folderName in GetFoldersList())
        {
            if (folderName == "Owoce")
                fruitsFolderExists = true;
            if (folderName == "Zwierzaki")
                animalsFolderExists = true;
        }

        if (!fruitsFolderExists)
            CreateSampleFolder("Owoce", fruitTextures);
        if (!animalsFolderExists)
            CreateSampleFolder("Zwierzaki", animalTextures);
    }

    private void CreateSampleFolder(string folderName, List<Texture2D> textures)
    {
        Debug.Log("Adding sample folder " + folderName);

        FileDownloader fd = new FileDownloader();
        foreach (Texture2D tex in textures)
        {
            fd.SaveTexture(tex, folderName, tex.name+".jpg");
        }

        //set active folder as the last one added
        sM.SetFolderName(folderName);

    }

    private string[] GetFoldersList()
    {
        string fullPath;
        string projectName;
        List<string> folderNames = new();
        string[] dir = null ;
        string[] returnFolderList = null;

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
