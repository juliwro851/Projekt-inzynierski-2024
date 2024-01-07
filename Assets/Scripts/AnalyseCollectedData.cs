using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class AnalyseCollectedData 
{
    private static List<ImageInfo> collectedData;
    private float averageReactionTime = 0.25f;
    private static List<int> imagesOrder;
    private static List<Click> clicks;
    private static Click worstClick, bestClick;
    private static int numberOfSavedAsPrev = 0;

    public void AnalyseData(List<ImageInfo> collectedImageInfo, List<int> collectedImagesOrder, List<Click> collectedClicks)
    {
        collectedData = collectedImageInfo;
        imagesOrder = collectedImagesOrder;
        clicks = collectedClicks;
        RemoveTooShortReactionTime();
    }

    public int GetNumberOfClicks()
    {
        return clicks.Count;
    }

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
    public Texture BestReactionTimeImage()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;

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
    public int WorstReactionTimeAppeared()
    {
        ImageInfo ImIn = new();
        float reactionTime = 0;
        int r = 0;

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

    void RemoveTooShortReactionTime()
    {
        Click C = new();
        List<Click> tempClicks;

        averageReactionTime += PlayerPrefs.GetFloat("allow-save-click-as-prev");

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

    public int GetNumberOfSavedAsPrev()
    {
        return numberOfSavedAsPrev;
    }
}
