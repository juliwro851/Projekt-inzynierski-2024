using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class FileDownloader
{

    public void SaveOverallJson(SaveOverallResultsToJson clickData, string file)
    {

        //string json = JsonUtility.ToJson(clickData);
        string clicksTxt = clickData.ClicksToString();
        string imInfoTxt = clickData.ImageInfoToString();
        string imOrderTxt = clickData.ImageOrderToString();

        var dir = UnityEngine.Application.persistentDataPath + "/saved/overall/";

        if (!Directory.Exists(Path.GetDirectoryName(dir)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dir));
        }

        string filePath = Path.Combine(dir, file + "CLICKS.txt");

        File.WriteAllText(filePath, clicksTxt);

        filePath = Path.Combine(dir, file + "IMAGEINFO.txt");

        File.WriteAllText(filePath, imInfoTxt);

        filePath = Path.Combine(dir, file + "IMAGEORDER.txt");

        File.WriteAllText(filePath, imOrderTxt);

    }

    public void SaveSimpleJson(SaveSimpleResultsToJson data, string file)
    {

        string json = JsonUtility.ToJson(data);

        var dir = UnityEngine.Application.persistentDataPath + "/saved/";

        if (!Directory.Exists(Path.GetDirectoryName(dir)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dir));
        }

        string filePath = Path.Combine(dir, file);

        File.WriteAllText(filePath, json);

    }
    public SaveSimpleResultsToJson LoadSimpleJson(string path)
    {
        if (System.IO.File.Exists(path))
        {
            string loadedJson = System.IO.File.ReadAllText(path);
            SaveSimpleResultsToJson loadedData = JsonUtility.FromJson<SaveSimpleResultsToJson>(loadedJson);

            return loadedData;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    public void SaveTexture(Texture2D data, string folder, string file)
    {
        byte[] jpgBytes = data.EncodeToJPG(); data.EncodeToPNG();
        string dataPath = UnityEngine.Application.persistentDataPath + "/data/"+ folder + "/";

        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }

        File.WriteAllBytes(dataPath + file, jpgBytes);


    }


    public List<Texture2D> LoadTexturesFromFolder(string folderName)
    {
        List<Texture2D> TexturestoReturn = new List<Texture2D>();
        string dataPath = UnityEngine.Application.persistentDataPath + "/data/" + folderName + "/";

        string[] files = Directory.GetFiles(dataPath);

        foreach (string fi in files)
        {
           string file = fi.Replace("\\", "/");

            if (File.Exists(file))
            {
                byte[] fileData = File.ReadAllBytes(file);

                Texture2D texture = new Texture2D(2, 2);

                // Load the image data into the Texture2D
                if (texture.LoadImage(fileData))
                {
                    TexturestoReturn.Add(texture);

                    //Debug.Log("Image loaded successfully!");
                }
                else
                {
                    Debug.LogError("Failed to load image from file.");
                }
            }
            else
            {
                Debug.LogError("File not found: " + file);
            }

        }

        return TexturestoReturn;
    }


    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            // mac
            filePath = Path.Combine(Application.streamingAssetsPath, ("data/" + FolderName));

            if (FileName != "")
                filePath = Path.Combine(filePath, (FileName + ".txt"));
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows
        filePath = Path.Combine(UnityEngine.Application.persistentDataPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".txt"));
#elif UNITY_ANDROID
            // android
            filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

            if(FileName != "")
                filePath = Path.Combine(filePath, (FileName + ".txt"));
#elif UNITY_IOS
            // ios
            filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

            if(FileName != "")
                filePath = Path.Combine(filePath, (FileName + ".txt"));
#endif
        return filePath;
    }


}