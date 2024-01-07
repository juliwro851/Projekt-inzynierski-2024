using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{
    [SerializeField] TMP_Text rightPercent;
    [SerializeField] TMP_Text wrongPercent;
    [SerializeField] TMP_Text reactionTime;
    [SerializeField] TMP_Text worstReactionTime;
    [SerializeField] TMP_Text bestReactionTime;
    [SerializeField] TMP_Text bestAppearence;
    [SerializeField] TMP_Text worstAppearence;
    [SerializeField] TMP_Text date;
    [SerializeField] TMP_Text folderName;
    [SerializeField] TMP_Text AmountAsPrev;

    public FileMenager fM;

    [Obsolete]
    void Start()
    {
        SelectNewProgram();

    }

    [Obsolete]
    public void SelectNewProgram()
    {
        fM.TryOpeningResultsFileBrowser();
    }

    public void DisplayResults(SaveSimpleResultsToJson SSRTJ)
    {
        string data = SSRTJ.rightPercent;
        rightPercent.text = data;

        float fdata;

        fdata = SSRTJ.bestReactionTime;
        bestReactionTime.text = "czas: " + fdata.ToString() + " sekund";

        int idata = SSRTJ.bestAppearence;
        bestAppearence.text = "przy " + idata.ToString() + " pojawieniu";


        fdata = SSRTJ.worstReactionTime;
        worstReactionTime.text = "czas: " + fdata.ToString() + " sekund";

        idata = SSRTJ.worstAppearence;
        worstAppearence.text = "przy " + idata.ToString() + " pojawieniu";


        data = SSRTJ.wrongPercent;
        wrongPercent.text = data;

        fdata = SSRTJ.reactionTime;
        reactionTime.text = fdata.ToString() + " sekund";

        data = SSRTJ.date;
        date.text = "Rezultat z: " + data;

        data = SSRTJ.folderName;
        folderName.text = data;

        fdata = SSRTJ.numberOfSavedAsPrev;
        AmountAsPrev.text = fdata.ToString();

    }

}
