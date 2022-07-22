using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTip;
    public string _text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //set tooltip to mouse position
        toolTip.transform.position = Input.mousePosition;
        toolTip.transform.position = new Vector3(toolTip.transform.position.x, toolTip.transform.position.y-40, toolTip.transform.position.z);
        toolTip.transform.GetChild(0).GetComponent<Text>().text = _text;
        toolTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }
}
