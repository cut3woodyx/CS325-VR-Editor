using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFile : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Openfile()
    {
        //create a process object
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        //open the windows explorer
        p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
        p.Start();
    }
}
