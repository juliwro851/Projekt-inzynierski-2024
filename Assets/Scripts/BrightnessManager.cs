using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class BrightnessManager : MonoBehaviour
{
    public PostProcessProfile brightness;
    public PostProcessProfile contrast;
    public PostProcessLayer layer;

    AutoExposure exposure;
    ColorGrading contr;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "UserSettingsBrightness")
        {
            brightness.TryGetSettings(out exposure);
            AdjustBrightness();
        }
        else
        {
            contrast.TryGetSettings(out contr);
            AdjustContrast();
        }
    }

   public void AdjustBrightness()
    {   
        if(PlayerPrefs.GetFloat("brightness")>=1)
            exposure.keyValue.value = PlayerPrefs.GetFloat("brightness");
        else
            exposure.keyValue.value = 1;

        //Debug.Log(exposure.keyValue.value);
    }

    public void AdjustContrast()
    {
        contr.contrast.value = PlayerPrefs.GetInt("contrast");
        //Debug.Log(contr.saturation.value);
    }
}
