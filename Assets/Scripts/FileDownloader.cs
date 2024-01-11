using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;


// Klasa odpowiedzialna za pobieranie, zapisywanie i manipulowanie plikami w aplikacji.
public class FileDownloader
{
    // Metoda zapisująca dane z obiektu SaveOverallResultsToJson do plików tekstowych.
    public void SaveOverallJson(SaveOverallResultsToJson clickData, string file)
    {
        // Konwersja danych z obiektu clickData na tekstowe reprezentacje.
        string clicksTxt = clickData.ClicksToString();
        string imInfoTxt = clickData.ImageInfoToString();
        string imOrderTxt = clickData.ImageOrderToString();

        // Ustalenie ścieżki do katalogu zapisu.
        var dir = UnityEngine.Application.persistentDataPath + "/saved/overall/";

        // Sprawdzenie, czy katalog istnieje, i ewentualne utworzenie go.
        if (!Directory.Exists(Path.GetDirectoryName(dir)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dir));
        }

        // Zapisywanie do plików tekstowych.
        string filePath = Path.Combine(dir, file + "CLICKS.txt");

        File.WriteAllText(filePath, clicksTxt);

        filePath = Path.Combine(dir, file + "IMAGEINFO.txt");

        File.WriteAllText(filePath, imInfoTxt);

        filePath = Path.Combine(dir, file + "IMAGEORDER.txt");

        File.WriteAllText(filePath, imOrderTxt);

    }

    // Metoda zapisująca dane z obiektu SaveSimpleResultsToJson do pliku JSON.
    public void SaveSimpleJson(SaveSimpleResultsToJson data, string file)
    {
        // Konwersja obiektu SaveSimpleResultsToJson do formatu JSON.
        string json = JsonUtility.ToJson(data);

        // Ustalenie ścieżki do katalogu zapisu.
        var dir = UnityEngine.Application.persistentDataPath + "/saved/";

        // Sprawdzenie, czy katalog istnieje, i ewentualne utworzenie go.
        if (!Directory.Exists(Path.GetDirectoryName(dir)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dir));
        }

        // Zapisanie do pliku JSON.
        string filePath = Path.Combine(dir, file);

        File.WriteAllText(filePath, json);

    }

    // Metoda wczytująca dane z pliku JSON do obiektu SaveSimpleResultsToJson.
    public SaveSimpleResultsToJson LoadSimpleJson(string path)
    {
        // Sprawdzenie, czy plik istnieje.
        if (System.IO.File.Exists(path))
        {
            // Odczytanie zawartości pliku JSON.
            string loadedJson = System.IO.File.ReadAllText(path);

            // Konwersja JSON na obiekt SaveSimpleResultsToJson.
            SaveSimpleResultsToJson loadedData = JsonUtility.FromJson<SaveSimpleResultsToJson>(loadedJson);

            return loadedData;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    // Metoda zapisująca teksturę do pliku w formacie JPG w określonym folderze.
    public void SaveTexture(Texture2D data, string folder, string file)
    {
        // Konwersja tekstury do tablicy bajtów w formacie JPG.
        byte[] jpgBytes = data.EncodeToJPG(); data.EncodeToPNG();

        // Ustalenie ścieżki do katalogu zapisu.
        string dataPath = UnityEngine.Application.persistentDataPath + "/data/"+ folder + "/";

        // Sprawdzenie, czy katalog istnieje, i ewentualne utworzenie go.
        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }

        // Zapisanie tekstury do pliku.
        File.WriteAllBytes(dataPath + file, jpgBytes);
    }

    // Metoda wczytująca tekstury z folderu o określonej nazwie.
    public List<Texture2D> LoadTexturesFromFolder(string folderName)
    {
        // Lista do przechowywania wczytanych tekstur.
        List<Texture2D> TexturestoReturn = new List<Texture2D>();

        // Ustalenie ścieżki do folderu z teksturami.
        string dataPath = UnityEngine.Application.persistentDataPath + "/data/" + folderName + "/";

        // Pobranie listy plików z folderu.
        string[] files = Directory.GetFiles(dataPath);

        // Pętla przetwarzająca każdy plik w folderze.
        foreach (string fi in files)
        {
            // Zamiana ukośników w ścieżce pliku na odwrotne ukośniki.
            string file = fi.Replace("\\", "/");

            // Sprawdzenie, czy plik istnieje.
            if (File.Exists(file))
            {
                // Odczytanie zawartości pliku jako tablicy bajtów.
                byte[] fileData = File.ReadAllBytes(file);

                // Utworzenie nowego obiektu Texture2D.
                Texture2D texture = new Texture2D(2, 2);

                // Wczytanie danych obrazu do obiektu Texture2D.
                if (texture.LoadImage(fileData))
                {
                    // Dodanie wczytanej tekstury do listy.
                    TexturestoReturn.Add(texture);
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
}