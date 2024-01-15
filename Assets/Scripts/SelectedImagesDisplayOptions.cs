
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for handling options related to displaying selected images.
/// </summary>
public class SelectedDisplayImagesOptions : MonoBehaviour
{
    /// <summary>
    /// Reference to the dropdown for maximum time between images.
    /// </summary>
    [SerializeField] TMP_Dropdown maxTimeBetweenImagesDropdown;
    /// <summary>
    /// Reference to the dropdown for minimum time between images.
    /// </summary>
    [SerializeField] TMP_Dropdown minTimeBetweenImagesDropdown;
    /// <summary>
    /// Reference to the dropdown for the percentage of repeated images.
    /// </summary>
    [SerializeField] TMP_Dropdown partRepeatedPercentDropdown;

    /// <summary>
    /// Method called when the script starts.
    /// </summary>
    void Start()
    {
        // Update data when the script starts
        UpdateData();
    }

    /// <summary>
    /// Method to update data in dropdowns.
    /// </summary>
    public void UpdateData()
    {
        // Initialize maximum values
        InitMaxValues(maxTimeBetweenImagesDropdown);

        // Initialize minimum values
        InitMinValues(minTimeBetweenImagesDropdown);

        // Initialize percentage values
        InitPercentValues(partRepeatedPercentDropdown);
    }

    /// <summary>
    /// Method to initialize maximum values.
    /// </summary>
    /// <param name="dropdown">Dropdown to be initialized.</param>
    public void InitMaxValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Set dropdown options
        dropdown.options = options;

        // Add available values
        options.Add(new TMP_Dropdown.OptionData(4.ToString()));
        options.Add(new TMP_Dropdown.OptionData(6.ToString()));
        options.Add(new TMP_Dropdown.OptionData(8.ToString()));
        options.Add(new TMP_Dropdown.OptionData(10.ToString()));
        options.Add(new TMP_Dropdown.OptionData(12.ToString()));

        // Set dropdown options and default value
        maxTimeBetweenImagesDropdown.options = options;
        maxTimeBetweenImagesDropdown.value = 1;
    }

    /// <summary>
    /// Method to initialize minimum values.
    /// </summary>
    /// <param name="dropdown">Dropdown to be initialized.</param>
    public void InitMinValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Set dropdown options
        dropdown.options = options;

        // Add available values
        options.Add(new TMP_Dropdown.OptionData(3.ToString()));
        options.Add(new TMP_Dropdown.OptionData(4.ToString()));
        options.Add(new TMP_Dropdown.OptionData(6.ToString()));
        options.Add(new TMP_Dropdown.OptionData(8.ToString()));
        options.Add(new TMP_Dropdown.OptionData(10.ToString()));

        // Set dropdown options and default value
        minTimeBetweenImagesDropdown.options = options;
        minTimeBetweenImagesDropdown.value = 0;
    }

    /// <summary>
    /// Method to initialize percentage values.
    /// </summary>
    /// <param name="dropdown">Dropdown to be initialized.</param>
    public void InitPercentValues(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // Set dropdown options
        dropdown.options = options;

        // Add available values
        options.Add(new TMP_Dropdown.OptionData(0.2f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.4f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.5f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.6f.ToString()));
        options.Add(new TMP_Dropdown.OptionData(0.8f.ToString()));

        // Set dropdown options and default value
        partRepeatedPercentDropdown.options = options;
        partRepeatedPercentDropdown.value = 2;
    }


}
