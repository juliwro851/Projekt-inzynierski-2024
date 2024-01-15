using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

/// <summary>
/// Represents a click event during an image presentation.
/// </summary>
public class Click
{
    public bool wasCorrect;// Indicates whether the click was correct.
    public bool firstClick;// Indicates if it was the first click.
    public int orderInAppearence;// Order of the click in appearance.
    public float timestamp;// Timestamp of the click.
    public ImageInfo connectedImageInfo;// Information about the connected image.
    public int displayedId;// Identifier of the displayed image.

    /// <summary>
    /// Gets a list of clicks associated with a specific displayed image identifier.
    /// </summary>
    /// <param name="list">List of clicks to filter.</param>
    /// <param name="id">Displayed image identifier.</param>
    /// <returns>List of clicks associated with the specified displayed image identifier.</returns>
    public List<Click> GetClicksForDisplayedId(List<Click> list, int id)
    {
        List<Click> clicks = new List<Click>();
        foreach(Click c in list)
        {
            if(c.displayedId== id)
            {
                clicks.Add(c);
            }
        }
        return clicks;
    }

}
