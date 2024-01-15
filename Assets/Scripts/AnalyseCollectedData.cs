using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

/// <summary>
/// Class responsible for analyzing collected data, such as clicks, responses, reaction times, etc.
/// </summary>
public class AnalyseCollectedData 
{
    // Collected data during the experiment.
    private static List<ImageInfo> collectedData;
    private float averageReactionTime = 0.25f; // Default average reaction time.
    private static List<int> imagesOrder;
    private static List<Click> clicks;
    private static Click worstClick, bestClick;
    private static int numberOfSavedAsPrev = 0;

    /// <summary>
    /// Analyzes the collected data.
    /// </summary>
    /// <param name="collectedImageInfo">List of collected ImageInfo objects.</param>
    /// <param name="collectedImagesOrder">List of collected image orders.</param>
    /// <param name="collectedClicks">List of collected Click objects.</param>
    public void AnalyseData(List<ImageInfo> collectedImageInfo, List<int> collectedImagesOrder, List<Click> collectedClicks)
    {
        collectedData = collectedImageInfo;
        imagesOrder = collectedImagesOrder;
        clicks = collectedClicks;
        RemoveTooShortReactionTime();
    }

    /// <summary>
    /// Gets the number of clicks collected during the experiment.
    /// </summary>
    /// <returns>The number of clicks.</returns>
    public int GetNumberOfClicks()
    {
        return clicks.Count;
    }

    /// <summary>
    /// Calculates the percentage of correct answers.
    /// </summary>
    /// <returns>The percentage of correct answers.</returns>
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

    /// <summary>
    /// Returns information about the number of correct answers and repetitions.
    /// </summary>
    /// <returns>A string containing correct answers and repetitions.</returns>
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

    /// <summary>
    /// Calculates the percentage of wrong answers.
    /// </summary>
    /// <returns>The percentage of wrong answers.</returns>
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

    /// <summary>
    /// Returns information about the number of wrong answers.
    /// </summary>
    /// <returns>A string containing the number of wrong answers.</returns>
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

    /// <summary>
    /// Calculates the average reaction time.
    /// </summary>
    /// <returns>The average reaction time.</returns>
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

    /// <summary>
    /// Returns the best reaction time.
    /// </summary>
    /// <returns>The best reaction time.</returns>
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

    /// <summary>
    /// Returns the worst reaction time.
    /// </summary>
    /// <returns>The worst reaction time.</returns>
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

    /// <summary>
    /// Returns the image associated with the worst reaction time.
    /// </summary>
    /// <returns>The texture of the image associated with the worst reaction time.</returns>
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

    /// <summary>
    /// Returns the image associated with the best reaction time.
    /// </summary>
    /// <returns>The texture of the image associated with the best reaction time.</returns>
    public Texture BestReactionTimeImage()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;

        // Iterating through clicks to find the best reaction time.
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

    /// <summary>
    /// Returns the session number in which the worst reaction time occurred.
    /// </summary>
    /// <returns>The session number for the worst reaction time.</returns>
    public int WorstReactionTimeAppeared()
    {
        ImageInfo ImIn = new();
        float reactionTime = 0;
        int r = 0;

        // Iterating through clicks to find the session with the worst reaction time.
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

    /// <summary>
    /// Returns the session number in which the best reaction time occurred.
    /// </summary>
    /// <returns>The session number for the best reaction time.</returns>
    public int BestReactionTimeAppeared()
    {
        List<float> reactionTimes = new List<float>();
        float reactionTime = 100;
        int r=0;

        // Iterating through clicks to find the session with the best reaction time.
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

    /// <summary>
    /// Removes too short reaction times and moves clicks to the previous session.
    /// </summary>
    void RemoveTooShortReactionTime()
    {
        Click C = new();
        List<Click> tempClicks;

        averageReactionTime += PlayerPrefs.GetFloat("allow-save-click-as-prev");

        // Iteration through collected data to find too short reaction times.
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


    /// <summary>
    /// Moves the click to the previous session.
    /// </summary>
    /// <param name="imageId">Image ID.</param>
    /// <param name="displayId">Display ID.</param>
    /// <param name="clickTime">Timestamp of the click.</param>
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

    /// <summary>
    /// Gets the number of clicks saved as previous sessions.
    /// </summary>
    /// <returns>The number of clicks saved as previous sessions.</returns>
    public int GetNumberOfSavedAsPrev()
    {
        return numberOfSavedAsPrev;
    }
}
