using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;

public class CameraTranslateDropdown : MonoBehaviour
{
    public GameObject _camera;
    void Start()
    {
        //Clears all the dropdown options
        GetComponent<Dropdown>().ClearOptions();

        //Dropdown options will be populated thru the settings in Camera Control Script
        foreach (float opt in _camera.GetComponent<CameraControl>()._cameraTranslateSpeeds)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();

            option.text = opt.ToString();

            GetComponent<Dropdown>().options.Add(option);
        }

        //Makes sure that the default value is reflected.
        GetComponent<Dropdown>().RefreshShownValue();
    }
}
