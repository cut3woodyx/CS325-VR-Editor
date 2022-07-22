using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public GameObject confirmationWindow;
    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //fake loading
        slider.value += 0.005f;
        //if load complete, open the confirmation window
        if (slider.value >= 1)
        {
            //close loading window
            loadingScreen.SetActive(false);
            slider.value = 0;
            confirmationWindow.SetActive(true);
        }
    }
}
