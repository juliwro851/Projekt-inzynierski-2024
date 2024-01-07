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

public class FileMenager : MonoBehaviour
{
    public SelectedFileMenager sFM;
    public ShowResults sR;

    [SerializeField] TMP_InputField folderNameInput;

    [Obsolete]
    public void TryOpeningFileBrowser()
    {
        if (folderNameInput.text == "")
        {
            Debug.Log("Can't upload an empty file");
            return;       
        }

        bool fountMatchingFolderName = false;
        foreach (string folderName in sFM.GetFoldersList())
        {
            if (folderName == folderNameInput.text)
                fountMatchingFolderName = true;
        }

        if (fountMatchingFolderName)
        {
            Debug.Log("Can't upload existing file name");
            return;
        }
        else
            OpenFileBrowser(folderNameInput.text);
    }

    [Obsolete]
    public void TryOpeningResultsFileBrowser()
    {
        OpenResultsBrowser();
    }

    [Obsolete]
    public void OpenFileBrowser(string folderName)
    {
        String fileName="default";
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");

        StartCoroutine(ShowLoadDialogCoroutine());

        IEnumerator ShowLoadDialogCoroutine()
        {
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

            while (FileBrowser.Result.Length < 6)
            {
                yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
            }

            //Debug.Log(FileBrowser.Success);

            if (FileBrowser.Success)
            {
                FileDownloader fd = new FileDownloader();
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                {
                    //Debug.Log(FileBrowser.Result[i]);

                    FileBrowser.Result[i] = FileBrowser.Result[i].Replace("\\", "/");

                    Console.ReadLine();

                    UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + FileBrowser.Result[i]);
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                        fileName = Path.GetFileName(FileBrowser.Result[i]);
                        fd.SaveTexture(myTexture, folderName, fileName);
                    }

                    sFM.UpdateFoldersListAfterNewFolderAdded(folderName);
                    folderNameInput.text = "";
                }
            }
            
        }
        
    }

    [Obsolete]
    public void OpenResultsBrowser()
    {
        FileBrowser.SetDefaultFilter(".json");
        SaveSimpleResultsToJson save = new();

        StartCoroutine(ShowLoadDialogCoroutine());

        IEnumerator ShowLoadDialogCoroutine()
        {
            string path = UnityEngine.Application.persistentDataPath + "/saved/";

            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, path, null, "Load Files", "Load");

            if (FileBrowser.Success)
            {
                FileDownloader fd = new FileDownloader();

                //FileBrowser.Result[0] = FileBrowser.Result[0].Replace("\\", "/");
                save = fd.LoadSimpleJson(FileBrowser.Result[0]);
                sR.DisplayResults(save);
            }
        }
    }
}
