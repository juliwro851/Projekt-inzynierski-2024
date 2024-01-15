using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


/// <summary>
/// Class responsible for shuffling a list of image identifiers with consideration for repeated elements.
/// </summary>
public class ImagesShuffle
{
    private List<int> imagesDisplayOrder = new List<int>();
    private List<int> imagesTemporaryList = new List<int>();
    private static System.Random r = new System.Random();
    private List<int> imagesToRepeat = new List<int>();
    private List<int> imagesRepeated = new List<int>();
    int a, b = 0;
    String message;

    /// <summary>
    /// Creates a new list of image identifiers considering repeated images.
    /// </summary>
    /// <param name="numberOfImages">Total number of images.</param>
    /// <param name="partRepeatedPercent">Percentage of images to be repeated.</param>
    /// <returns>New list of image identifiers.</returns>
    public List<int> CreateNewImageList(int numberOfImages, double partRepeatedPercent)
    {
        a = 0; b=0;
        ClearAllLists();

        CreateTempImageIdLists(numberOfImages, partRepeatedPercent);

        int n = 1;
        for (int i = 0; i < imagesToRepeat.Count; i+=2)
        {
            
            int j = n;
            while (j > 0)
            {
                imagesRepeated.Add(imagesToRepeat[i]);
                if (imagesToRepeat.Count != i+1)
                {
                    imagesRepeated.Add(imagesToRepeat[i + 1]);
                }
                j--;
            }
            n++;
        }

        RandomiseList(imagesRepeated);
        CorrectRepeatedList(imagesRepeated);


        int aMax = imagesRepeated.Count;
        int bMax = imagesTemporaryList.Count;
        float rand;

        for (int i = 0; i < imagesRepeated.Count + imagesTemporaryList.Count; i++)
        {
            if (a < aMax && b < bMax) {
                rand = UnityEngine.Random.Range(0.0f, 1.0f);
                
                if (rand < 0.4)
                {
                    if (i == 0)
                    {
                        imagesDisplayOrder.Add(imagesRepeated[a]);
                        a++;
                    }
                    else
                    {

                        if (imagesDisplayOrder[i - 1] != imagesRepeated[a])
                        {
                            imagesDisplayOrder.Add(imagesRepeated[a]);
                            a++;
                        }
                        else
                        {
                            imagesDisplayOrder.Add(imagesTemporaryList[b]);
                            b++;
                        }
                    }
                    }

                else
                {
                    if (i == 0)
                    {
                        imagesDisplayOrder.Add(imagesTemporaryList[b]);
                        b++;
                    }
                    else
                    {
                        if (imagesDisplayOrder[i - 1] != imagesTemporaryList[b])
                        {
                            imagesDisplayOrder.Add(imagesTemporaryList[b]);
                            b++;
                        }
                        else
                        {
                            imagesDisplayOrder.Add(imagesRepeated[a]);
                            a++;
                        }
                    }
                }
            }
            else
            {
                if (a < aMax)
                {
                    imagesDisplayOrder.Add(imagesRepeated[a]);
                    a++;
                }

                else if (b < bMax)
                {
                    imagesDisplayOrder.Add(imagesTemporaryList[b]);
                    b++;
                }
                else
                    break;

            }
        }
        CorrectRepeatedList(imagesDisplayOrder);
        return imagesDisplayOrder;
    }

    /// <summary>
    /// Creates a temporary list of image identifiers.
    /// </summary>
    /// <param name="numberOfImages">Total number of images.</param>
    /// <param name="partRepeatedPercent">Percentage of images to be repeated.</param>
    private void CreateTempImageIdLists(int numberOfImages, double partRepeatedPercent)
    {
        int partToRepeat = 0;

        for (int i = 0; i < numberOfImages; i++)
        {
            imagesTemporaryList.Add(i);
        }

        RandomiseList(imagesTemporaryList);

        partToRepeat = (int)(imagesTemporaryList.Count * partRepeatedPercent);


        for (int i = 0; i < partToRepeat; i++)
        {
            imagesToRepeat.Add(imagesTemporaryList[i]);
        }

    }

    /// <summary>
    /// Randomly shuffles the elements in the list.
    /// </summary>
    /// <param name="list">List to be shuffled.</param>
    void RandomiseList(List<int> list) {

        int length = list.Count;
        while (length > 0)
        {
            length--;
            int k = r.Next(length + 1);
            int value = list[k];
            list[k] = list[length];
            list[length] = value;
        }
    }

    /// <summary>
    /// Corrects the list with repeated elements.
    /// </summary>
    /// <param name="list">List to be corrected.</param>
    void CorrectRepeatedList(List<int> list)
    {
        for(int i = 1; i < list.Count-1; i++)
        {
            if (list[i - 1] == list[i])
            {
                if (list[i] == list[i + 1])
                {
                    if (i > list.Count / 2)
                    {
                        int t = list[i];
                        list[i] = list[i - 2];
                        list[i - 2] = t;
                    }
                    else
                    {
                        int t = list[i];
                        list[i] = list[i + 2];
                        list[i + 2] = t;
                    }
                }
                else
                {
                    if (i > list.Count / 2)
                    {
                        int t = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = t;
                    }
                    else
                    {
                        int t = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = t;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Initializes the image shuffling.
    /// </summary>
    private void Start()
    {
        Start(imagesDisplayOrder);
    }

    /// <summary>
    /// Invokes the image shuffling and displays a message with the sorted list.
    /// </summary>
    void Start(List<int> imagesDisplayOrder)
    {
        //CreateNewImageList();
        //displayListMessage(imagesDisplayOrder);
    }

    /// <summary>
    /// Displays a message with the list of image identifiers.
    /// </summary>
    /// <param name="list">List of image identifiers.</param>
    void displayListMessage(List<int> list)
    {
        message = "";
        for(int i=0;i<list.Count(); i++)
        {
            message += " " + list[i].ToString();
        }
        Debug.Log(message);
    }

    /// <summary>
    /// Clears all lists in the class.
    /// </summary>
    void ClearAllLists(){

        imagesDisplayOrder.Clear();
        imagesTemporaryList.Clear();
        imagesToRepeat.Clear();
        imagesRepeated.Clear();
    }

}
