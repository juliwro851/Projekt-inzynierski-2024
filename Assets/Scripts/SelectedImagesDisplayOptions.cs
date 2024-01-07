
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedDisplayImagesOptions : MonoBehaviour
{
    [SerializeField] TMP_Dropdown maxTimeBetweenImagesDropdown;
    [SerializeField] TMP_Dropdown minTimeBetweenImagesDropdown;
    [SerializeField] TMP_Dropdown partRepeatedPercentDropdown;

    void Start()
    {
        UpdateData();
    }
    public void UpdateData()
    {
        InitMaxValues(maxTimeBetweenImagesDropdown);

        InitMinValues(minTimeBetweenImagesDropdown);

        InitPercentValues(partRepeatedPercentDropdown);
    }


    public void InitMaxValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        dropdown.options = options;

        options.Add(new TMP_Dropdown.OptionData(4.ToString()));
        options.Add(new TMP_Dropdown.OptionData(6.ToString()));
        options.Add(new TMP_Dropdown.OptionData(8.ToString()));
        options.Add(new TMP_Dropdown.OptionData(10.ToString()));
        options.Add(new TMP_Dropdown.OptionData(12.ToString()));

        maxTimeBetweenImagesDropdown.options = options;
        maxTimeBetweenImagesDropdown.value = 1;
    }
    public void InitMinValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        dropdown.options = options;

        options.Add(new TMP_Dropdown.OptionData(3.ToString()));
        options.Add(new TMP_Dropdown.OptionData(4.ToString()));
        options.Add(new TMP_Dropdown.OptionData(6.ToString()));
        options.Add(new TMP_Dropdown.OptionData(8.ToString()));
        options.Add(new TMP_Dropdown.OptionData(10.ToString()));

        minTimeBetweenImagesDropdown.options = options;
        minTimeBetweenImagesDropdown.value = 0;
    }

    public void InitPercentValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        dropdown.options = options;

        options.Add(new TMP_Dropdown.OptionData(0.2f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.4f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.5f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.6f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.8f.ToString()));

        partRepeatedPercentDropdown.options = options;
        partRepeatedPercentDropdown.value = 2;
    }


}
