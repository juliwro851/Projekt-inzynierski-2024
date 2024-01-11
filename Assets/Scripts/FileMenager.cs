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

// Klasa odpowiedzialna za zarządzanie plikami w aplikacji.
public class FileMenager : MonoBehaviour
{
    // Referencje do innych managerów i komponentów w scenie.
    public SelectedFileMenager sFM;
    public ShowResults sR;

    // Referencja do pola tekstowego do wprowadzania nazwy folderu.
    [SerializeField] TMP_InputField folderNameInput;

    // Metoda obsługująca próbę otwarcia przeglądarki plików dla folderu.
    [Obsolete]
    public void TryOpeningFileBrowser()
    {
        // Sprawdzenie, czy nazwa folderu nie jest pusta.
        if (folderNameInput.text == "")
        {
            Debug.Log("Can't upload an empty file");
            return;       
        }

        // Sprawdzenie, czy istnieje folder o podanej nazwie.
        bool fountMatchingFolderName = false;
        foreach (string folderName in sFM.GetFoldersList())
        {
            if (folderName == folderNameInput.text)
                fountMatchingFolderName = true;
        }

        // Jeśli znaleziono folder o takiej nazwie, wyświetl komunikat i przerwij działanie metody.
        if (fountMatchingFolderName)
        {
            Debug.Log("Can't upload existing file name");
            return;
        }
        else
            OpenFileBrowser(folderNameInput.text);
    }

    // Metoda obsługująca próbę otwarcia przeglądarki plików dla wyników.
    [Obsolete]
    public void TryOpeningResultsFileBrowser()
    {
        OpenResultsBrowser();
    }


    // Metoda otwierająca przeglądarkę plików dla konkretnego folderu.
    [Obsolete]
    public void OpenFileBrowser(string folderName)
    {
        // Domyślna nazwa pliku.
        String fileName ="default";

        // Ustawienie filtrów dla plików obrazów.
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");

        // Rozpoczęcie korutyny odpowiedzialnej za wyświetlenie okna dialogowego przeglądarki plików.
        StartCoroutine(ShowLoadDialogCoroutine());

        IEnumerator ShowLoadDialogCoroutine()
        {
            // Oczekiwanie na wybór plików i folderów.
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

            // Pętla wymuszająca ponowne wybranie plików, jeśli ilość wybranych plików jest mniejsza niż 6.
            while (FileBrowser.Result.Length < 6)
            {
                yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
            }

            // Sprawdzenie, czy operacja zakończyła się sukcesem.
            if (FileBrowser.Success)
            {
                // Obiekt do pobierania plików.
                FileDownloader fd = new FileDownloader();

                // Pętla przetwarzająca każdy wybrany plik.
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                {
                    // Zamiana ukośników w ścieżce pliku na odwrotne ukośniki.
                    FileBrowser.Result[i] = FileBrowser.Result[i].Replace("\\", "/");

                    Console.ReadLine();

                    // Tworzenie żądania sieciowego w celu pobrania tekstury.
                    UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + FileBrowser.Result[i]);
                    yield return www.SendWebRequest();

                    // Obsługa błędów pobierania tekstury.
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        // Konwersja pobranej tekstury na obiekt Texture2D.
                        Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                        // Pobranie nazwy pliku z pełnej ścieżki.
                        fileName = Path.GetFileName(FileBrowser.Result[i]);

                        // Zapisanie tekstury w podanym folderze.
                        fd.SaveTexture(myTexture, folderName, fileName);
                    }

                    // Aktualizacja listy folderów po dodaniu nowego folderu.
                    sFM.UpdateFoldersListAfterNewFolderAdded(folderName);

                    // Wyczyszczenie pola tekstowego.
                    folderNameInput.text = "";
                }
            }
            
        }
        
    }

    // Metoda otwierająca przeglądarkę plików dla wyników.
    [Obsolete]
    public void OpenResultsBrowser()
    {
        // Ustawienie domyślnego filtru na pliki JSON.
        FileBrowser.SetDefaultFilter(".json");

        // Obiekt do zapisu wyników do pliku JSON.
        SaveSimpleResultsToJson save = new();


        // Rozpoczęcie korutyny odpowiedzialnej za wyświetlenie okna dialogowego przeglądarki plików.
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
                // Obiekt do pobierania plików.
                FileDownloader fd = new FileDownloader();

                // Wczytanie pliku JSON i wyświetlenie wyników.
                save = fd.LoadSimpleJson(FileBrowser.Result[0]);
                sR.DisplayResults(save);
            }
        }
    }
}
