using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using System.Windows;
public class ToggleMenuBar : MonoBehaviour
{
    public List<GameObject> _menuBarObjects;
    public List<GameObject> _menuBarButtons;
    public Color _selectionColour;
    private int _currentSelection = 0;
    public GameObject _confirmationWindow;

    //DLL for the deprecated functions
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();


    public void Start()
    {
        ToggleMenu(0);

        _confirmationWindow.SetActive(false);
    }

    //Logic behind toggling between bookmark panels
    //When one button is active, the rest of the panel will be turned off.
    public void ToggleMenu(int selection)
    {
        _currentSelection =selection;
        int index = 0;

        foreach(GameObject go in _menuBarObjects)
        {
            if(_currentSelection == index)
            {
                _menuBarButtons[index].GetComponent<Image>().color = _selectionColour;
                go.SetActive(true);
            }    
            else
            {
                _menuBarButtons[index].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                go.SetActive(false);
            }

            ++index;
        }
    }


    //Originally planned to have the 3 default windows button to be present in our application
    //In the end decided to use the default unity windowed mode buttons.
    //Therefore, bottom 4 function is deprecated.
    public void CheckToQuit()
    {   
        //Possible Checks if user have unsaved work and prompts
        _confirmationWindow.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Minimize()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void Maximize()
    {
        //Not sure if this works
        ShowWindow(GetActiveWindow(), 3);
    }
}
