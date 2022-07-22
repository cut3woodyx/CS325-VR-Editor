using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * all objects that need to be highlighted need to have the "Outline" script attached
 *      Outline Mode >> Outline And Silhouete
 *      Outline Colour >> whatever colour, red seems ok
 *      Outline Width >> 10 (maxed)
 */


public class HierarchyManager : MonoBehaviour
{
    public GameObject RuntimeInspector;
    public Transform[] ChildrenInHView;
    public GameObject prevSelected_HView, currSelected_HView;
    public GameObject prevSeleted_Scene, currSelected_Scene;

    void Start()
    {
        prevSelected_HView = currSelected_HView = null;
        prevSeleted_Scene = currSelected_Scene = null;
    }

    void Update()
    {
        MouseClickSelection();
    }

    // OBJECT HIGHLIGHT CONTROL
    //--------------------------------------------------------------------------------------------------------------------------------------------

    void MouseClickSelection()
    {
        if (Camera.main.pixelRect.Contains(Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHitObj;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHitObj, Mathf.Infinity)) // do MousePos to WorldSpace raycast
                {
                    currSelected_Scene = rayHitObj.transform.gameObject;
                    if (prevSeleted_Scene == null)
                    {
                        prevSeleted_Scene = currSelected_Scene;
                    }
                    if (currSelected_Scene != prevSeleted_Scene)
                    {
                        prevSeleted_Scene = currSelected_Scene;
                    }

                    RuntimeInspector.GetComponent<RuntimeInspectorNamespace.RuntimeInspector>().Inspect(rayHitObj.transform.gameObject);

                    SearchNameInHViewContent(currSelected_Scene.transform.name);
                }
                else
                {
                    RuntimeInspector.GetComponent<RuntimeInspectorNamespace.RuntimeInspector>().StopInspect();
                    SearchNameInHViewContent("aaa");
                }
            }
        }
    }

    void SearchNameInHViewContent(string name)
    {
        if (name != "aaa")
        {
            ChildrenInHView = GetComponentsInChildren<Transform>();
            foreach (Transform tr in ChildrenInHView)
            {
                if (tr.name == "Name")
                {
                    if (tr.GetComponent<UnityEngine.UI.Text>().text == name)
                    {
                        Debug.Log("hit");

                        currSelected_HView = tr.gameObject; // @ Name
                        currSelected_HView = currSelected_HView.transform.parent.gameObject; // @ Content
                        currSelected_HView = currSelected_HView.transform.parent.gameObject; // @ HierarchyField(Clone)
                        currSelected_HView = currSelected_HView.transform.GetChild(0).gameObject; // @ Background
                        currSelected_HView.GetComponent<UnityEngine.UI.Image>().color = new Color(0.3115809f, 0.5730224f, 0.8308824f, 1);
                        // change colour
                        break;
                    }
                }
            }
        }

        // loop to uncolour all other "Names"
        foreach (Transform tr in ChildrenInHView)
        {
            if (tr.name == "Name")
            {
                if (tr.GetComponent<UnityEngine.UI.Text>().text != name)
                {
                    Debug.Log("hit");
                    GameObject temp = tr.gameObject;
                    temp = temp.transform.parent.gameObject; // @ Content
                    temp = temp.transform.parent.gameObject; // @ HierarchyField(Clone)
                    temp = temp.transform.GetChild(0).gameObject; // @ Background
                    temp.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
                }
            }
        }
    }

    void SetHighlight()
    {

    }


    // OBSELETE STUFF - cause got the HierarchyPlugin
    //--------------------------------------------------------------------------------------------------------------------------------------------

    //enum CHANGETYPE {
    //    ERROR = 0,
    //    ADD,
    //    DELETE,
    //    ENABLE,
    //    DISABLE,
    //    SELECT,
    //    SCREENSIZE
    //}

    //// SCREEN STUFF
    //    public GameObject FirstElementPosition; // position of first UI element
    //    Vector2 _screenSz, _posStart; // (width,height) && (x,y)
    //    float _posBufferY; // buffer amount between UI elements
    //// GAME OBJECT STUFF
    //    List<GameObject> _allUIs; // stores all UI
    //    int _totalNumOfGOs;
    //    GameObject _currObj, _currUI; // used for Add & Delete objects
    //    bool _haveChanges;
    //    CHANGETYPE _typeOfChange;

    //void Start()
    //{
    //// SCREEN STUFF
    //    _screenSz = _posStart = new Vector2(0, 0);
    // // GAME OBJECT STUFF
    //    _allUIs = new List<GameObject>();
    //    _totalNumOfGOs = 0;
    //    _currObj = _currUI = null;
    //    _haveChanges = false;
    //    _typeOfChange = CHANGETYPE.ERROR;
    //}

    //void BaseLogic()
    //{
    //    if (_screenSz.x != Screen.height ||
    //        _screenSz.y != Screen.width)
    //    {
    //        _screenSz.x = Screen.height;
    //        _screenSz.y = Screen.width;

    //        _posStart.x = FirstElementPosition.transform.localPosition.x;
    //        _posStart.y = FirstElementPosition.transform.localPosition.y;

    //        _posBufferY = 50.0f; // technically should scale with the screen size BUT will only implement if got time
    //    }
    //}

    //void HierarchyLogic()
    //{
    //    if (!_haveChanges)
    //    { }
    //    else
    //    {
    //        _haveChanges = false;
    //        switch (_typeOfChange)
    //        {
    //            case CHANGETYPE.ADD:
    //                {
    //                    LogicAddGO(_currObj);
    //                    break;
    //                }
    //            case CHANGETYPE.DELETE:
    //                {
    //                    break;
    //                }
    //            case CHANGETYPE.ENABLE:
    //                {
    //                    break;
    //                }
    //            case CHANGETYPE.DISABLE:
    //                {
    //                    break;
    //                }
    //            case CHANGETYPE.SELECT:
    //                {
    //                    break;
    //                }
    //            case CHANGETYPE.SCREENSIZE:
    //                {
    //                    LogicUpdateUI();
    //                    break;
    //                }
    //            default:
    //                {
    //                    Debug.Log(" --- WARNING --- HierarchyManager >> Attempting to make changes with CHANGETYPE." + _typeOfChange);
    //                    break;
    //                }
    //        }
    //    }
    //}

    //void NewObjUI()
    //{
    //    _currUI = new GameObject(); // should create a prefab and createPrefab here
    //    // need handle changes to Display here
    //}

    //void LogicUpdateUI()
    //{
    //    float currY = 0.0f;
    //    for (int i = 0; i < _totalNumOfGOs; i++)
    //    {
    //        _allUIs[i].transform.position = new Vector3(_allUIs[i].transform.position.x, currY, _allUIs[i].transform.position.z);
    //        currY += _posBufferY;
    //    }
    //}

    //void LogicAddGO(GameObject obj)
    //{
    //    if (_currObj == null)
    //    {
    //        Debug.Log(" --- WARNING --- HierarchyManager >> LogicAddGO(null)");
    //        return;
    //    }
    //    NewObjUI();
    //    _currUI = _currObj = null;
    //}


    //// called by UI button for Adding GameObject (GO)
    //public void AddGO(GameObject obj)
    //{
    //    _haveChanges = true;
    //    _currObj = obj;
    //    _typeOfChange = CHANGETYPE.ADD;
    //}

    //// called by UI button for Deleting GO
    //public void DeleteGO(GameObject obj)
    //{
    //    _haveChanges = true;
    //    _currObj = obj;
    //    _typeOfChange = CHANGETYPE.DELETE;
    //}

    //// called by UI button for Enabling GO
    //public void EnableGO(GameObject obj)
    //{
    //    _haveChanges = true;
    //    _currObj = obj;
    //    _typeOfChange = CHANGETYPE.ENABLE;
    //}

    //// called by UI button for Disabling GO
    //public void DisableGO(GameObject obj)
    //{
    //    _haveChanges = true;
    //    _currObj = obj;
    //    _typeOfChange = CHANGETYPE.DISABLE;
    //}

    //// called by mouse click
    //public void SelectGO(GameObject obj)
    //{
    //    _haveChanges = true;
    //    _currObj = obj;
    //    _typeOfChange = CHANGETYPE.SELECT;
    //}

    //--------------------------------------------------------------------------------------------------------------------------------------------
}
