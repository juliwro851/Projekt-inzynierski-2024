using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class responsible for displaying and managing results in the user interface.
/// </summary>
public class ShowResults : MonoBehaviour
{
    // Serialized fields to bind with user interface (UI) elements
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

    // Reference to the FileMenager script
    public FileMenager fM;


    /// <summary>
    /// Obsolete method called at the start of the script execution.
    /// </summary>
    [Obsolete]
    void Start()
    {
        // Call the method for selecting a new program
        SelectNewProgram();
    }

    /// <summary>
    /// Obsolete method opening a file browser to select a new program.
    /// </summary>
    [Obsolete]
    public void SelectNewProgram()
    {
        fM.TryOpeningResultsFileBrowser();
    }

    /// <summary>
    /// Method to display results based on data saved in SaveSimpleResultsToJson.
    /// </summary>
    /// <param name="SSRTJ">Instance of SaveSimpleResultsToJson containing result data.</param>
    public void DisplayResults(SaveSimpleResultsToJson SSRTJ)
    {
        // Display percentage of correct answers
        string data = SSRTJ.rightPercent;
        rightPercent.text = data;

        float fdata;

        // Display best reaction time
        fdata = SSRTJ.bestReactionTime;
        bestReactionTime.text = "czas: " + fdata.ToString() + " sekund";

        // Display best appearance
        int idata = SSRTJ.bestAppearence;
        bestAppearence.text = "przy " + idata.ToString() + " pojawieniu";

        // Display worst reaction time
        fdata = SSRTJ.worstReactionTime;
        worstReactionTime.text = "czas: " + fdata.ToString() + " sekund";

        // Display worst appearance
        idata = SSRTJ.worstAppearence;
        worstAppearence.text = "przy " + idata.ToString() + " pojawieniu";

        // Display percentage of wrong answers
        data = SSRTJ.wrongPercent;
        wrongPercent.text = data;

        // Display reaction time
        fdata = SSRTJ.reactionTime;
        reactionTime.text = fdata.ToString() + " sekund";

        // Display the date when the result was obtained
        data = SSRTJ.date;
        date.text = "Rezultat z: " + data;

        // Display the name of the folder
        data = SSRTJ.folderName;
        folderName.text = data;

        // Display the number of saved results as previous
        fdata = SSRTJ.numberOfSavedAsPrev;
        AmountAsPrev.text = fdata.ToString();

    }

}
