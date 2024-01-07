using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveOverallResultsToJson
{
    public List<Click> clicks = new();
    public List<ImageInfo> imageInfo = new();
    public List<int> imagesOrder = new();

    public string ClicksToString()
    {
        string s = "";
        foreach (Click c in clicks)
        {
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

    public string ImageInfoToString()
    {
        string s = "";
        foreach (ImageInfo im in imageInfo)
        {
            s += im.id.ToString() + ";";
            foreach(float t in im.timeOfAppearence)
            {
                s += t.ToString();
                if (t != im.timeOfAppearence[im.timeOfAppearence.Count - 1])
                {
                    s += ",";
                }
            }
            s += ";";

            foreach (float t in im.timeOfDisappearence)
            {
                s += t.ToString();
                if ( t != im.timeOfDisappearence[im.timeOfDisappearence.Count-1])
                {
                    s += ",";
                }
            }
            s += ";";

            s += "\n";
        }
        return s;
    }

    public string ImageOrderToString()
    {
        string s = "";
        foreach (int im in imagesOrder)
        {
            s += im.ToString() + ";";

        }
        s += "\n";
        return s;
    }
}
