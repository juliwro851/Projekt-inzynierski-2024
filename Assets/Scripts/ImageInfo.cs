using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class storing information about an image.
/// </summary>
public class ImageInfo
{
    /// <summary>
    /// Unique identifier of the image.
    /// </summary>
    public int id = -1;
    /// <summary>
    /// Texture used in the image.
    /// </summary>
    public Texture usedTexture;
    /// <summary>
    /// List of display identifiers where the image appeared.
    /// </summary>
    public List<int> displayId = new List<int>();
    /// <summary>
    /// List of times when the image appeared in individual displays.
    /// </summary>
    public List<float> timeOfAppearence = new List<float>();
    /// <summary>
    /// List of times when the image disappeared from individual displays.
    /// </summary>
    public List<float> timeOfDisappearence = new List<float>();


    /// <summary>
    /// Static method to retrieve image information based on its identifier.
    /// </summary>
    /// <param name="list">List of ImageInfo instances.</param>
    /// <param name="id">Image identifier.</param>
    /// <returns>ImageInfo instance corresponding to the provided id, or an empty instance if not found.</returns>
    public ImageInfo GetImageInfoById(List<ImageInfo> list, int id)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            // Check if the image identifier matches the requested id.
            if (item.id == id)
            {
                return item;
            }

        }
        // Return an empty ImageInfo instance if no matching id is found.
        return info;
    }

    /// <summary>
    /// Static method to retrieve image information based on display identifier.
    /// </summary>
    /// <param name="list">List of ImageInfo instances.</param>
    /// <param name="displayId">Display identifier.</param>
    /// <returns>ImageInfo instance corresponding to the provided displayId, or an empty instance if not found.</returns>
    public ImageInfo GetImageInfoByDisplayId(List<ImageInfo> list, int displayId)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            foreach (int id in item.displayId)
            {
                // Check if the DisplayIds list contains the requested displayId.
                if (id == displayId)
                {
                    return item;
                }
            }
        }
        // Return an empty ImageInfo instance if no matching displayId is found.
        return info;
    }

}
