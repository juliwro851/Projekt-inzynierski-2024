using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for modifying the font size in TextMeshPro components.
/// </summary>
public class TextSizeModifier : MonoBehaviour
{
    /// <summary>
    /// Method called when the script is first loaded or the object is activated.
    /// </summary>
    void Start()
    {
        // Call the method to update the text size.
        UpdateTextSize();
    }

    /// <summary>
    /// Method to update the font size of TMP_Text components.
    /// </summary>
    void UpdateTextSize()
    {
        // Find all TMP_Text components in the scene.
        TMP_Text[] textMeshComponents = FindObjectsOfType<TMP_Text>();

        // Iterate through each TMP_Text component and update the font size.
        foreach (TMP_Text textMesh in textMeshComponents)
        {
            // Increase the current font size by the retrieved increment from PlayerPrefs.
            textMesh.fontSize = textMesh.fontSize + PlayerPrefs.GetInt("font-size");
        }
    }
}
