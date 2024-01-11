using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Klasa odpowiedzialna za zapisanie ogólnych wyników do formatu JSON
public class SaveOverallResultsToJson
{
    // Listy do przechowywania danych Click, ImageInfo i kolejności obrazów
    public List<Click> clicks = new();
    public List<ImageInfo> imageInfo = new();
    public List<int> imagesOrder = new();

    // Konwertuje dane Click na sformatowany ciąg znaków
    public string ClicksToString()
    {
        string s = "";
        foreach (Click c in clicks)
        {
            // Dołącza dane Click do ciągu znaków z separatorem średnika
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

    // Konwertuje dane ImageInfo na sformatowany ciąg znaków
    public string ImageInfoToString()
    {
        string s = "";
        foreach (ImageInfo im in imageInfo)
        {
            // Dołącza dane ImageInfo do ciągu znaków z separatorem średnika
            s += im.id.ToString() + ";";

            // Dołącza czas pojawienia się z separatorem przecinka
            foreach (float t in im.timeOfAppearence)
            {
                s += t.ToString();
                if (t != im.timeOfAppearence[im.timeOfAppearence.Count - 1])
                {
                    s += ",";
                }
            }
            s += ";";

            // Dołącza czas zniknięcia z separatorem przecinka
            foreach (float t in im.timeOfDisappearence)
            {
                s += t.ToString();
                if ( t != im.timeOfDisappearence[im.timeOfDisappearence.Count-1])
                {
                    s += ",";
                }
            }
            s += ";";

            s += "\n"; // Przechodzi do nowej linii dla kolejnych danych ImageInfo
        }
        return s;
    }

    // Konwertuje dane kolejności obrazów na sformatowany ciąg znaków
    public string ImageOrderToString()
    {
        string s = "";
        foreach (int im in imagesOrder)
        {
            // Dołącza dane kolejności obrazów do ciągu znaków z separatorem średnika
            s += im.ToString() + ";";
        }
        s += "\n"; // Przechodzi do nowej linii dla kolejnych danych kolejności obrazów
        return s;
    }
}
