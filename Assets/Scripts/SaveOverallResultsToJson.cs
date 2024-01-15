using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class responsible for saving overall results to JSON format.
/// </summary>
public class SaveOverallResultsToJson
{
    /// <summary>
    /// List to store Click data.
    /// </summary>
    public List<Click> clicks = new();

    /// <summary>
    /// List to store ImageInfo data.
    /// </summary>
    public List<ImageInfo> imageInfo = new();

    /// <summary>
    /// List to store the order of images.
    /// </summary>
    public List<int> imagesOrder = new();


    /// <summary>
    /// Converts Click data to a formatted string.
    /// </summary>
    /// <returns>Formatted string containing Click data.</returns>
    public string ClicksToString()
    {
        string s = "";
        foreach (Click c in clicks)
        {
            // Concatenate Click data to the string with a semicolon separator
            s += c.wasCorrect.ToString() + ";";
            s += c.firstClick.ToString() + ";";
            s += c.orderInAppearence.ToString() + ";";
            s += c.timestamp.ToString() + ";";
            s += c.connectedImageInfo.id.ToString() + ";";
            s += c.displayedId.ToString() + ";";

            s += "\n";
        }
        return s;
    }

    /// <summary>
    /// Converts ImageInfo data to a formatted string.
    /// </summary>
    /// <returns>Formatted string containing ImageInfo data.</returns>
    public string ImageInfoToString()
    {
        string s = "";
        foreach (ImageInfo im in imageInfo)
        {
            // Concatenate ImageInfo data to the string with a semicolon separator
            s += im.id.ToString() + ";";

            // Concatenate time of appearance with a comma separator
            foreach (float t in im.timeOfAppearence)
            {
                s += t.ToString();
                if (t != im.timeOfAppearence[im.timeOfAppearence.Count - 1])
                {
                    s += ",";
                }
            }
            s += ";";

            // Concatenate time of disappearance with a comma separator
            foreach (float t in im.timeOfDisappearence)
            {
                s += t.ToString();
                if ( t != im.timeOfDisappearence[im.timeOfDisappearence.Count-1])
                {
                    s += ",";
                }
            }
            s += ";";

            s += "\n"; // Move to a new line for the next ImageInfo data
        }
        return s;
    }

    /// <summary>
    /// Converts image order data to a formatted string.
    /// </summary>
    /// <returns>Formatted string containing image order data.</returns>
    public string ImageOrderToString()
    {
        string s = "";
        foreach (int im in imagesOrder)
        {
            // Concatenate image order data to the string with a semicolon separator
            s += im.ToString() + ";";
        }
        s += "\n"; // Move to a new line for the next image order data
        return s;
    }
}
