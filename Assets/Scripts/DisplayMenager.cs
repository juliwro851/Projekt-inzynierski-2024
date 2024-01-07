using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class DisplayMenager : MonoBehaviour
{
    [SerializeField] Image resultsFrame;
    [SerializeField] Image noClicksFrame;
    [SerializeField] TMP_Text rightPercent;
    [SerializeField] TMP_Text wrongPercent;
    [SerializeField] TMP_Text reactionTime;
    [SerializeField] TMP_Text worstReactionTime;
    [SerializeField] TMP_Text bestReactionTime;
    [SerializeField] TMP_Text bestAppearence;
    [SerializeField] TMP_Text worstAppearence;
    [SerializeField] RawImage worstImage;
    [SerializeField] RawImage bestImage;
    //[SerializeField] ChangeImagesAtTime changedImages;


    private AnalyseCollectedData ACD;
    private SaveOverallResultsToJson SORTJ;
    private SaveSimpleResultsToJson SSRTJ;
    private bool displayed = false;

    void Start()
    {
        ACD = new AnalyseCollectedData();
        SSRTJ = new SaveSimpleResultsToJson();
        SORTJ = new SaveOverallResultsToJson();
        resultsFrame.gameObject.SetActive(false);
        noClicksFrame.gameObject.SetActive(false);
        displayed = false;
    }

    void Update()
    {
        if (ChangeImagesAtTime.finished)
        {
            resultsFrame.gameObject.SetActive(true);
            if(ACD.GetNumberOfClicks() == 0)
            {
                worstImage.texture = default(Texture2D);
                bestImage.texture = default(Texture2D);

                noClicksFrame.gameObject.SetActive(true);

                FileDownloader fd = new FileDownloader();

                string file = CreateFileName(false);
                fd.SaveSimpleJson(SSRTJ, file);


                file = CreateFileName(true);
                SORTJ.clicks = ChangeImagesAtTime.clicks;
                SORTJ.imageInfo = ChangeImagesAtTime.shownImagesInfo;
                fd.SaveOverallJson(SORTJ, file);

                displayed = true;
            }
            else if(ChangeImagesAtTime.finished && !displayed)
            {
                SSRTJ.folderName = PlayerPrefs.GetString("images-folder-name");
                SSRTJ.numberOfSavedAsPrev = ACD.GetNumberOfSavedAsPrev();


                string data = ACD.RightPercent().ToString() + "%   " + ACD.AccurateAnswers().ToString();
                rightPercent.text = data;
                SSRTJ.rightPercent = data;

                float fdata;

                if (ACD.RightPercent() != 0)
                {
                    fdata = ACD.BestReactionTime();
                    SSRTJ.bestReactionTime = fdata;
                    bestReactionTime.text = "czas: " + fdata.ToString() + " sekund";

                    int idata = ACD.BestReactionTimeAppeared();
                    SSRTJ.bestAppearence = idata;
                    bestAppearence.text = "przy " + idata.ToString() + " pojawieniu";

                    bestImage.texture = ACD.BestReactionTimeImage();



                    fdata = ACD.WorstReactionTime();
                    SSRTJ.worstReactionTime = fdata;
                    worstReactionTime.text = "czas: " + fdata.ToString() + " sekund";

                    idata = ACD.WorstReactionTimeAppeared();
                    SSRTJ.worstAppearence = idata;
                    worstAppearence.text = "przy " + idata.ToString() + " pojawieniu";

                    worstImage.texture = ACD.WorstReactionTimeImage();
                }
                else
                {
                    worstImage.texture = default(Texture2D);
                    bestImage.texture = default(Texture2D);
                }


                data = ACD.WrongPercent().ToString() + "%   " + ACD.WrongAnswers().ToString();
                wrongPercent.text = data;
                SSRTJ.wrongPercent = data;


                fdata = ACD.AverageReactionTime() ;
                SSRTJ.reactionTime = fdata;
                reactionTime.text = fdata.ToString() + " sekund";



                SSRTJ.date = DateTime.Now.ToString();

                FileDownloader fd = new FileDownloader();

                string file = CreateFileName(false);
                fd.SaveSimpleJson(SSRTJ, file);


                file = CreateFileName(true);
                SORTJ.clicks = ChangeImagesAtTime.clicks;
                SORTJ.imageInfo = ChangeImagesAtTime.shownImagesInfo;
                SORTJ.imagesOrder = ChangeImagesAtTime.imagesOrder;
                fd.SaveOverallJson(SORTJ, file);

                displayed = true;
            }

            ChangeImagesAtTime.finished = false;

        }
    }

    public string CreateFileName(bool isOverall)
    {
        string fileName = "new.json";
        DateTime date = DateTime.Now;
        if (isOverall)
        {
            fileName = date.Hour.ToString() + "-" + date.Minute.ToString() + "  " + date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString();

        }
        else
            fileName = date.Hour.ToString() + "-" + date.Minute.ToString()+ "  " + date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString() + ".json";

        return fileName;
    }
}
