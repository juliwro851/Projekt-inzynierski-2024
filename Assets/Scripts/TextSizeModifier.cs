using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSizeModifier : MonoBehaviour
{
    void Start()
    {
        UpdateTextSize();
    }

    void UpdateTextSize()
    {
        TMP_Text[] textMeshComponents = FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text textMesh in textMeshComponents)
        {
            textMesh.fontSize = textMesh.fontSize + PlayerPrefs.GetInt("font-size");
            //Debug.Log(textMesh.text);
        }
    }
}
