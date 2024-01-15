using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

/// <summary>
/// Class managing brightness and contrast settings using post-processing effects.
/// </summary>
public class BrightnessManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the post-processing profile for brightness.
    /// </summary>
    public PostProcessProfile brightness;

    /// <summary>
    /// Reference to the post-processing profile for contrast.
    /// </summary>
    public PostProcessProfile contrast;

    /// <summary>
    /// Reference to the post-processing layer.
    /// </summary>
    public PostProcessLayer layer;

    /// <summary>
    /// Reference to AutoExposure settings.
    /// </summary>
    AutoExposure exposure;

    /// <summary>
    /// Reference to ColorGrading settings.
    /// </summary>
    ColorGrading contr;

    /// <summary>
    /// Method called on object start.
    /// </summary>
    void Start()
    {
        // Check if the current scene is "UserSettingsBrightness"
        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
        {
            // If yes, retrieve AutoExposure settings
            brightness.TryGetSettings(out exposure);

            // Adjust brightness
            AdjustBrightness();
        }
        else
        {
            // If not, retrieve ColorGrading settings
            contrast.TryGetSettings(out contr);

            // Adjust contrast
            AdjustContrast();
        }
    }

    /// <summary>
    /// Method for adjusting brightness.
    /// </summary>
    public void AdjustBrightness()
    {
        // Set brightness to the saved value if greater than or equal to 1; otherwise, set it to 1
        if (PlayerPrefs.GetFloat("brightness")>=1)
            exposure.keyValue.value = PlayerPrefs.GetFloat("brightness");
        else
            exposure.keyValue.value = 1;

    }

    /// <summary>
    /// Method for adjusting contrast.
    /// </summary>
    public void AdjustContrast()
    {
        // Get the player-set contrast from PlayerPrefs and set the contrast to the saved value
        contr.contrast.value = PlayerPrefs.GetInt("contrast");
    }
}
