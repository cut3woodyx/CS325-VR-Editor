using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
#if UNITY_STANDALONE_WIN
using AnotherFileBrowser.Windows;
#endif
using GILES;

public class LevelManager : MonoBehaviour
{
    enum OPERATION {
        NONE = 0,
        NEW,
        OPEN,
        OPENRECENT,
        SAVE,
        SAVEAS
    }

    public GameObject PopUpMenu; // normal height = 200, withPic = 478
    //public GameObject PicOpen, PicSaveAs;
    public Text textHeader, textBody, textYesButton, textNoButton;
    OPERATION currOp, prevOp;
    bool isMenuOn, isMenuWithPic, isYesPressed, isNoPressed, isOpChanged;

    //private AssetBundle myLoadedAssetBundle;
    //private string[] scenePaths;

    public GameObject sceneParent;

    private string currentLevel;
    private string previousLevel;

    void Start()
    {
        currOp = prevOp = OPERATION.NONE;
        isMenuOn = isMenuWithPic = isYesPressed = isNoPressed = isOpChanged = false;
        sceneParent = pb_Scene.instance.gameObject;

        // scene loading var
        //myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }

    void Update()
    {
        if (currOp != prevOp)
        {
            isOpChanged = true;
            prevOp = currOp;
        }

        // Main loop for controlling 
        switch(currOp)
        {
            case OPERATION.NONE:
                {
                    if ( isMenuOn || isMenuWithPic )
                        DisableMenu();

                    if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
                    {
                        currOp = OPERATION.SAVE;
                    }
                    break;
                }
            case OPERATION.NEW:
                {
                    SceneNewLogic();
                    break;
                }
            case OPERATION.OPEN:
                {
                    SceneOpenLogic();
                    break;
                }
            case OPERATION.OPENRECENT:
                {
                    SceneOpenRecentLogic();
                    break;
                }
            case OPERATION.SAVE:
                {
                    SceneSaveLogic();
                    break;
                }
            case OPERATION.SAVEAS:
                {
                    SceneSaveAsLogic();
                    break;
                }
            default:
                break;
        }
    }

 

    // Logic for different stuffs
    void SceneNewLogic()
    {
        // single call on OPERATION change
        if (isOpChanged)
        {
            isOpChanged = false;
            EnableMenu(false);  
            textHeader.text = "New Scene";
            textBody.text = "Unsaved changes will be lost.\nDo you want to open a new scene?";
        }

        if (isYesPressed || isNoPressed)
        {
            if (isYesPressed)
            {
                //SceneManager.LoadScene("EmptyScene");
                foreach(Transform t in sceneParent.transform)
                {
                    Destroy(t.gameObject);
                }
                previousLevel = currentLevel;
                currentLevel = "";
            }
            currOp = OPERATION.NONE;
        }
    }

    void SceneOpenLogic()
    {
        if (isOpChanged)
        {
            isOpChanged = false;
            EnableMenu(true);
            textHeader.text = "Open Scene";
            textBody.text = "Unsaved changes will be lost.\nDo you want to open selected scene?";
        }

        if (isYesPressed || isNoPressed)
        {
            if (isYesPressed)
            {
                OpenFileBrowser();
                //System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
                //p.Start();
                //SceneManager.LoadScene("RecentScene");
            }
            currOp = OPERATION.NONE;
        }
    }

    void SceneOpenRecentLogic()
    {
        if (isOpChanged)
        {
            isOpChanged = false;
            EnableMenu(false);
            textHeader.text = "Open Recent Scene";
            textBody.text = "Unsaved changes will be lost.\nDo you want to open previous scene?";
        }

        if (isYesPressed || isNoPressed)
        {
            if (isYesPressed)
            {
                OpenFileBrowser(false);
                //SceneManager.LoadScene("RecentScene");
            }
            currOp = OPERATION.NONE;
        }
    }

    void SceneSaveLogic()
    {
        if (isOpChanged)
        {
            isOpChanged = false;
            EnableMenu(true);
            textHeader.text = "Save Scene";
            textBody.text = "Are you sure you want to save?";
        }

        if (isYesPressed || isNoPressed)
        {
            if (isYesPressed)
            {
                SaveFileBrowser(false);
            }
            currOp = OPERATION.NONE;
        }
    }

    void SceneSaveAsLogic()
    {
        if (isOpChanged)
        {
            isOpChanged = false;
            EnableMenu(true);
            textHeader.text = "Save As Scene";
            textBody.text = "Are you sure you want to save?";
        }

        if (isYesPressed || isNoPressed)
        {
            if (isYesPressed)
            {
                SaveFileBrowser();
                //System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
                //p.Start();
                // save to selected scene
            }
            currOp = OPERATION.NONE;
        }
    }

    void EnableMenu(bool gotPic)
    {
        isMenuOn = true;
        isMenuWithPic = gotPic; // open different popup menu for with or without explorer
        isYesPressed = isNoPressed = false;
        PopUpMenu.SetActive(true);
        //if (isMenuWithPic)
        //{
        //    PopUpMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
        //    if (currOp == OPERATION.OPEN)
        //        PicOpen.SetActive(true);
        //    if (currOp == OPERATION.SAVEAS)
        //        PicSaveAs.SetActive(true);
        //}
    }
    void DisableMenu()
    {
        PopUpMenu.SetActive(false);
        //if (isMenuWithPic)
        //{
        //    PopUpMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 200);
        //    PicOpen.SetActive(false);
        //    PicSaveAs.SetActive(false);
        //}
        isMenuOn = isMenuWithPic = isYesPressed = isNoPressed = false;
    }


    // public functions for button calls
    public void SceneNew()
    {
        currOp = OPERATION.NEW;
    }
    public void SceneOpen()
    {
        currOp = OPERATION.OPEN;
    }
    public void SceneOpenRecent()
    {
        currOp = OPERATION.OPENRECENT;
    }
    public void SceneSave()
    {
        currOp = OPERATION.SAVE;
    }
    public void SceneSaveAs()
    {
        currOp = OPERATION.SAVEAS;
    }

    public void ButtonYes()
    {
        isYesPressed = true;
    }
    public void ButtonNo()
    {
        isNoPressed = true;
    }

    public void OpenFileBrowser(bool open = true)
    {
#if UNITY_STANDALONE_WIN
        if (open)
        {
            var bp = new BrowserProperties();
            bp.filter = "json files (*.json)|*.json";
            bp.filterIndex = 0;

            new FileBrowser().OpenFileBrowser(bp, result =>
            {
                string path = pb_FileUtility.SanitizePath(result);
                currentLevel = path;

                if (!pb_FileUtility.IsValidPath(path, ".json"))
                {
                    Debug.LogWarning(path + " not found, or file is not a JSON scene.");
                    return;
                }

                string level = pb_FileUtility.ReadFile(path);

                pb_Scene.LoadLevel(level);
            });
        }
        else
        {
            if (!string.IsNullOrEmpty(previousLevel))
            {
                string level = pb_FileUtility.ReadFile(previousLevel);

                pb_Scene.LoadLevel(level);
            }
            else if(!string.IsNullOrEmpty(currentLevel))
            {
                string level = pb_FileUtility.ReadFile(currentLevel);

                pb_Scene.LoadLevel(level);
            }
        }
#endif
    }

    public void SaveFileBrowser(bool saveAs = true)
    {
#if UNITY_STANDALONE_WIN
        if (saveAs)
        {
            var bp = new BrowserProperties();
            bp.filter = "json files (*.json)|*.json";
            bp.filterIndex = 0;

            new FileBrowser().SaveFileBrowser(bp, "scene", ".json", result =>
            {
                string path = pb_FileUtility.SanitizePath(result);
                currentLevel = path;

                if (!path.EndsWith(".json"))
                    path += ".json";

                if (!pb_FileUtility.IsValidPath(path, ".json"))
                {
                    Debug.LogWarning(path + " is not a valid path.");
                    return;
                }

                pb_FileUtility.SaveFile(path, pb_Scene.SaveLevel());
            });
        }
        else
        {
            if (string.IsNullOrEmpty(currentLevel))
            {
                var bp = new BrowserProperties();
                bp.filter = "json files (*.json)|*.json";
                bp.filterIndex = 0;

                new FileBrowser().SaveFileBrowser(bp, "scene", ".json", result =>
                {
                    string path = pb_FileUtility.SanitizePath(result);
                    currentLevel = path;

                    if (!path.EndsWith(".json"))
                        path += ".json";

                    if (!pb_FileUtility.IsValidPath(path, ".json"))
                    {
                        Debug.LogWarning(path + " is not a valid path.");
                        return;
                    }

                    pb_FileUtility.SaveFile(path, pb_Scene.SaveLevel());
                });
            }
            else
            {
                pb_FileUtility.SaveFile(currentLevel, pb_Scene.SaveLevel());
            }
        }
#endif
    }
}
