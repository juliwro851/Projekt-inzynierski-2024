using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImageInfo
{
    public int id = -1;
    public Texture usedTexture;
    public List<int> displayId = new List<int>();
    public List<float> timeOfAppearence = new List<float>();
    public List<float> timeOfDisappearence = new List<float>();

    public ImageInfo GetImageInfoById(List<ImageInfo> list, int id)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            if(item.id == id)
            {
                return item;
            }

        }

        return info;
    }

    public ImageInfo GetImageInfoByDisplayId(List<ImageInfo> list, int displayId)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            foreach (int id in item.displayId)
            {
                if (id == displayId)
                {
                    return item;
                }
            }
        }

        return info;
    }

}
