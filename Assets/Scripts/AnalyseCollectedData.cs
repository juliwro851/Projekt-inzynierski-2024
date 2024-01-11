using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

// Klasa odpowiedzialna za analizę zebranych danych, takich jak kliknięcia, odpowiedzi, czasy reakcji, itp.
public class AnalyseCollectedData 
{
    // Dane zebrane podczas eksperymentu.
    private static List<ImageInfo> collectedData;
    private float averageReactionTime = 0.25f; // Domyślna średnia czasu reakcji.
    private static List<int> imagesOrder;
    private static List<Click> clicks;
    private static Click worstClick, bestClick;
    private static int numberOfSavedAsPrev = 0;

    // Metoda analizująca zebrane dane, przekazywane jako listy obiektów ImageInfo, int i Click.
    public void AnalyseData(List<ImageInfo> collectedImageInfo, List<int> collectedImagesOrder, List<Click> collectedClicks)
    {
        collectedData = collectedImageInfo;
        imagesOrder = collectedImagesOrder;
        clicks = collectedClicks;
        RemoveTooShortReactionTime();
    }

    // Metoda zwracająca liczbę kliknięć zebranych podczas eksperymentu.
    public int GetNumberOfClicks()
    {
        return clicks.Count;
    }

    // Metoda obliczająca procent poprawnych odpowiedzi.
    public int RightPercent()
    {
        float correctAnswers = 0;
        float numberOfRepetitions = 0;
        int returnMessage = 0;

        foreach (Click click in clicks)
        {
            if (click.firstClick && click.wasCorrect)
            {
                correctAnswers++;
            }
        }

        foreach (ImageInfo imIn in collectedData)
        {
            if (imIn.displayId.Count > 1)
                numberOfRepetitions = numberOfRepetitions + imIn.displayId.Count - 1;
        }

        double result = correctAnswers/ numberOfRepetitions * 100;

        returnMessage = (int)result;

        return returnMessage;
    }

    // Metoda zwracająca informację o liczbie poprawnych odpowiedzi.
    public string AccurateAnswers()
    {
        int correctAnswers = 0;
        int numberOfRepetitions = 0;
        string returnMessage;

        foreach (Click click in clicks)
        {
            if (click.firstClick && click.wasCorrect)
            {
                correctAnswers++;
            }
        }

        foreach (ImageInfo imIn in collectedData)
        {
            if (imIn.displayId.Count>1)
                numberOfRepetitions = numberOfRepetitions + imIn.displayId.Count - 1;
        }

        returnMessage = correctAnswers.ToString() + "/" + numberOfRepetitions.ToString();

        return returnMessage;
    }

    // Metoda obliczająca procent błędnych odpowiedzi.
    public int WrongPercent()
    {
        float wrongAnswers = 0, totalClicks = 0, totalPhotos = 0;
        int returnMessage = 0;

        foreach (Click click in clicks)
        {
            if (!click.wasCorrect && click.firstClick)
            {
                wrongAnswers++;
            }
            totalClicks++;
        }
        totalPhotos = imagesOrder.Count;

        
        float result = wrongAnswers / totalPhotos * 100;

        returnMessage = (int)result;

        return returnMessage;
    }

    // Metoda zwracająca informację o liczbie błędnych odpowiedzi.
    public string WrongAnswers()
    {
        int wrongAnswers = 0, totalClicks = 0;
        string returnMessage = "";

        foreach (Click click in clicks)
        { 
            if (!click.wasCorrect)
            {
                wrongAnswers++;  
            }
            totalClicks++;
        }

        returnMessage = wrongAnswers.ToString() + " na (" + totalClicks + ") wszystkich";

        return returnMessage;
    }

    // Metoda obliczająca średni czas reakcji.
    public float AverageReactionTime()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 0;

        foreach (Click click in clicks)
        {

            if (click.timestamp >= click.connectedImageInfo.timeOfAppearence[click.orderInAppearence]
                && click.timestamp <= click.connectedImageInfo.timeOfDisappearence[click.orderInAppearence]
                && click.firstClick)
            {
                reactionTimes.Add(click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence]);
            }

        }

        if (reactionTimes.Count > 0)
        {
            reactionTime = reactionTimes.Sum() / reactionTimes.Count;
            reactionTime = (float)Math.Round(reactionTime, 2);
        }

        return reactionTime;

    }

    // Metoda zwracająca najlepszy czas reakcji.
    public float BestReactionTime()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;

        foreach (Click click in clicks)
        {
            if(click.timestamp >= click.connectedImageInfo.timeOfAppearence[click.orderInAppearence] 
                &&  click.timestamp <= click.connectedImageInfo.timeOfDisappearence[click.orderInAppearence] 
                &&  click.firstClick)
            {
                if (click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence] < reactionTime)
                {
                    reactionTime = click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence];
                    bestClick = click;
                }

            }

        }

        reactionTime = (float)Math.Round(reactionTime, 2);

        return reactionTime;
    }

    // Metoda zwracająca najgorszy czas reakcji.
    public float WorstReactionTime()
    {
        ImageInfo ImIn = new();
        float reactionTime = 0;

        foreach (Click click in clicks)
        {
            if (click.wasCorrect == true)
            {
                ImIn = ImIn.GetImageInfoByDisplayId(collectedData, click.displayedId);

                for (int i = 0; i < ImIn.displayId.Count; i++)
                {
                    if (ImIn.displayId[i] == click.displayedId && click.timestamp - ImIn.timeOfAppearence[i] > reactionTime)
                    {
                        reactionTime = click.timestamp - ImIn.timeOfAppearence[i];
                        worstClick = click;
                    }
                }
            }
        }

        reactionTime = (float)Math.Round(reactionTime, 2);

        return reactionTime;
    }

    // Metoda zwracająca obraz związany z najgorszym czasem reakcji.
    public Texture WorstReactionTimeImage()
    {
        ImageInfo ImIn = new();
        float reactionTime = 0;

        foreach (Click click in clicks)
        {
            if (click.wasCorrect == true)
            {
                ImIn = ImIn.GetImageInfoByDisplayId(collectedData, click.displayedId);

                for (int i = 0; i < ImIn.displayId.Count; i++)
                {
                    if (ImIn.displayId[i] == click.displayedId && click.timestamp - ImIn.timeOfAppearence[i] > reactionTime)
                    {
                        reactionTime = click.timestamp - ImIn.timeOfAppearence[i];
                        worstClick = click;
                    }
                }
            }
        }

        reactionTime = (float)Math.Round(reactionTime, 2);

        return worstClick.connectedImageInfo.usedTexture;
    }

    // Metoda zwracająca obraz związany z najlepszym czasem reakcji.
    public Texture BestReactionTimeImage()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;

        // Iteracja przez kliknięcia w poszukiwaniu najlepszego czasu reakcji.
        foreach (Click click in clicks)
        {
            if (click.timestamp >= click.connectedImageInfo.timeOfAppearence[click.orderInAppearence]
                && click.timestamp <= click.connectedImageInfo.timeOfDisappearence[click.orderInAppearence]
                && click.firstClick)
            {
                if (click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence] < reactionTime)
                {
                    reactionTime = click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence];
                    bestClick = click;
                }
            }
        }
        reactionTime = (float)Math.Round(reactionTime, 2);
        return bestClick.connectedImageInfo.usedTexture;
    }

    // Metoda zwracająca numer sesji, w której wystąpił najdłuższy czas reakcji.
    public int WorstReactionTimeAppeared()
    {
        ImageInfo ImIn = new();
        float reactionTime = 0;
        int r = 0;

        // Iteracja przez kliknięcia w poszukiwaniu najdłuższego czasu reakcji.
        foreach (Click click in clicks)
        {
            if (click.wasCorrect == true)
            {
                ImIn = ImIn.GetImageInfoByDisplayId(collectedData, click.displayedId);

                for (int i = 0; i < ImIn.displayId.Count; i++)
                {
                    if (ImIn.displayId[i] == click.displayedId && click.timestamp - ImIn.timeOfAppearence[i] > reactionTime)
                    {
                        reactionTime = click.timestamp - ImIn.timeOfAppearence[i];
                        r = click.orderInAppearence + 1;
                    }
                }
            }
        }


        return r;
    }

    // Metoda zwracająca numer sesji, w której wystąpił najkrótszy czas reakcji.
    public int BestReactionTimeAppeared()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;
        int r=0;

        foreach (Click click in clicks)
        {
            if (click.timestamp >= click.connectedImageInfo.timeOfAppearence[click.orderInAppearence]
                && click.timestamp <= click.connectedImageInfo.timeOfDisappearence[click.orderInAppearence]
                && click.firstClick)
            {
                if (click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence] < reactionTime)
                {
                    reactionTime = click.timestamp - click.connectedImageInfo.timeOfAppearence[click.orderInAppearence];
                    r = click.orderInAppearence + 1;
                }

            }

        }
        return r;
    }

    // Metoda usuwająca zbyt krótkie czasy reakcji i przenosząca kliknięcia do poprzedniej sesji.
    void RemoveTooShortReactionTime()
    {
        Click C = new();
        List<Click> tempClicks;

        averageReactionTime += PlayerPrefs.GetFloat("allow-save-click-as-prev");

        // Iteracja przez zebrane dane w poszukiwaniu zbyt krótkich czasów reakcji.
        foreach (ImageInfo info in collectedData)
        {
            for (int i = 0; i < info.displayId.Count; i++)
            {
                tempClicks = C.GetClicksForDisplayedId(clicks, info.displayId[i]);

                foreach (Click t in tempClicks)
                {
                    if (t.timestamp >= info.timeOfAppearence[i] && t.timestamp <= info.timeOfDisappearence[i])
                    {
                        float clickTime = t.timestamp - info.timeOfAppearence[i];

                        if (t.timestamp - info.timeOfAppearence[i] < averageReactionTime)
                        {
                            clickTime = t.timestamp;

                            PlaceInPreviousImageInfo(info.id, info.displayId[i], clickTime);
                            numberOfSavedAsPrev++;
                        }
                    }
                }
            }
        }
    }


    // Metoda przenosząca kliknięcie do poprzedniej sesji.
    void PlaceInPreviousImageInfo(int imageId, int displayId, float clickTime)
    {

        foreach (Click click in clicks)
        {
            if (click.orderInAppearence > 0 && click.timestamp == clickTime)
            {
                click.timestamp = clickTime + averageReactionTime;
                click.displayedId = displayId - 1;

                for (int i = click.displayedId; i >= 0; i--)
                {
                    if (imagesOrder[i] == imagesOrder[click.displayedId])
                    {
                        click.wasCorrect = true;
                        break;
                    }
                    else
                        click.wasCorrect = false;
                }
            }
        }
    }

    // Metoda zwracająca liczbę kliknięć zapisanych jako poprzednie sesje.
    public int GetNumberOfSavedAsPrev()
    {
        return numberOfSavedAsPrev;
    }
}
