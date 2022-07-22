using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class RightClickDropDown : MonoBehaviour, IPointerClickHandler
{
    public GameObject _mainUI;
    public GameObject _emptyPanel;
    public GameObject _buttonPrefab;
    public List<string> _dropDownOptions;
    public List<Button.ButtonClickedEvent> _dropDownEvents;

    private List<GameObject> _dropDownButtons = new List<GameObject>();

    private static bool _exist = false;
    private static GameObject _lastPanel = null;
    void Start()
    {
        // Locates UI Canvas object for parenting
        if (_mainUI == null)
        {
            _mainUI = GameObject.Find("UI");
            if (_mainUI == null)
            {
                _mainUI = gameObject;
            }
        }
        // Builds all the buttons and components, sets positions

        // Creates dropdown panel
        _emptyPanel = Instantiate(_emptyPanel);
        _emptyPanel.transform.SetParent(_mainUI.transform);
        _emptyPanel.transform.localScale = new Vector3(1, 1, 1);
        _emptyPanel.transform.position = new Vector3(0,0,0);

        // Gets button locations and sizing
        float buttonX = _buttonPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float buttonY = _buttonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        _emptyPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonX, buttonY * _dropDownOptions.Count);

        // Creates buttons in panel
        for (int i = 0; i < _dropDownOptions.Count; ++i)
        {
            // Generates buttons
            GameObject newObj = Instantiate(_buttonPrefab);
            _dropDownButtons.Add(newObj);

            // Sets button name, position and sizing
            newObj.name = _dropDownOptions[i];
            newObj.transform.Find("Text").GetComponent<Text>().text = newObj.name;
            newObj.transform.SetParent(_emptyPanel.transform);
            newObj.transform.localScale = new Vector3(1, 1, 1);
            newObj.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * newObj.GetComponent<RectTransform>().sizeDelta.y, 0);

            // Inserts Unity Events to run on click
            if (_dropDownEvents.Count > i)
            {
                newObj.GetComponent<Button>().onClick = _dropDownEvents[i];
            }
            newObj.SetActive(false);
        }
        _emptyPanel.SetActive(false);
    }
    void Update()
    {
        // Closes right click window on left click frame after
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(_InvokeNextFrame());
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Move to mouse location
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Only one dropdown instance allowed
            if (_exist)
            {
                _lastPanel.SetActive(false);
            }

            _exist = true;
            _lastPanel = _emptyPanel;

            _emptyPanel.SetActive(true);
            _emptyPanel.transform.position = eventData.position;
            foreach (GameObject i in _dropDownButtons)
            {
                i.SetActive(true);
            }
        }
    }

    public void OnMouseOver()
    {
        // Move to mouse location
        if (Input.GetMouseButtonUp(1))
        {
            // Only one dropdown instance allowed
            if (_exist)
            {
                _lastPanel.SetActive(false);
            }

            _exist = true;
            _lastPanel = _emptyPanel;

            _emptyPanel.SetActive(true);
            _emptyPanel.transform.position = Input.mousePosition;
            foreach (GameObject i in _dropDownButtons)
            {
                i.SetActive(true);
            }
        }
    }

    // Test function
    public void PrintMessage()
    {
        Debug.Log("Printing Message");
    }

    private IEnumerator _InvokeNextFrame()
    {
        yield return null;

        // Resets Dropdown
        _exist = false;
        _lastPanel = _emptyPanel;

        foreach (GameObject i in _dropDownButtons)
        {
            i.SetActive(false);
        }
        _emptyPanel.SetActive(false);
    }
}
