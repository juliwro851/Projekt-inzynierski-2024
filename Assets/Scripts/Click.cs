using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Click
{
    public bool wasCorrect;
    public bool firstClick;
    public int orderInAppearence;
    public float timestamp;
    public ImageInfo connectedImageInfo;
    public int displayedId;

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
