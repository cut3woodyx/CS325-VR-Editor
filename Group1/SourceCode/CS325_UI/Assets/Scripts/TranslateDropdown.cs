using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;

public class TranslateDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _gizmo;
    void Start()
    {
        //Clears all the dropdown options
        GetComponent<Dropdown>().ClearOptions();

        //Dropdown options will be populated thru the settings in TransformGizmo Script

        foreach (float opt in _gizmo.GetComponent<TransformGizmo>()._translateSnapSizes)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();

            option.text = opt.ToString();

            GetComponent<Dropdown>().options.Add(option);
        }

        //Makes sure that the default value is reflected.
        GetComponent<Dropdown>().RefreshShownValue();
    }
}
