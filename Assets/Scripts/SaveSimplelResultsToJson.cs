using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class for storing and managing simple results to be saved as JSON.
/// </summary>
public class SaveSimpleResultsToJson
{
    /// <summary>
    /// Date of the result.
    /// </summary>
    public string date = "00.00.00";

    /// <summary>
    /// Folder name associated with the result.
    /// </summary>
    public string folderName = "none";

    /// <summary>
    /// Percentage of correct answers.
    /// </summary>
    public string rightPercent = "0/0";

    /// <summary>
    /// Percentage of wrong answers.
    /// </summary>
    public string wrongPercent = "0(0)";

    /// <summary>
    /// Overall reaction time.
    /// </summary>
    public float reactionTime = 0.0f;

    /// <summary>
    /// Best reaction time recorded.
    /// </summary>
    public float bestReactionTime = 0.0f;

    /// <summary>
    /// Number of the best appearance.
    /// </summary>
    public int bestAppearence = 0;

    /// <summary>
    /// Worst reaction time recorded.
    /// </summary>
    public float worstReactionTime = 0.0f;

    /// <summary>
    /// Number of the worst appearance.
    /// </summary>
    public int worstAppearence = 0;

    /// <summary>
    /// Number of times the result was saved as previous.
    /// </summary>
    public int numberOfSavedAsPrev = 0;

}
