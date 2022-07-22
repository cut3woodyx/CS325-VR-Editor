using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GILES;

public class AssetManager : MonoBehaviour
{
    enum OPERATION {
        NONE = 0,
        CUT,
        COPY,
        PASTE
    }

    public GameObject RuntimeHierarchy;
    public GameObject Cube, Sphere, Capsule, Cylinder;
    public GameObject sceneParent;
    GameObject currObj;
    Vector3 currObjPos;
    OPERATION currOp = OPERATION.NONE;

    void Start()
    {
        sceneParent = pb_Scene.instance.gameObject;
    }

    void Update()
    {        
    }


    // logic used by button calls
    void SelectObj()
    {
        // get and temporarily save current obj
        if (RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene.transform != null)
        {
            currObj = RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene;
            currObjPos = currObj.transform.position;
        }
    }

    public void CutObj()
    {
        // if cut already set, 'swap' selected cut objects
        if (currOp == OPERATION.CUT)
            currObj.SetActive(true);

        currOp = OPERATION.CUT;
        SelectObj();
        currObj.SetActive(false);
    }
    public void CopyObj()
    {
        if (currOp == OPERATION.CUT)
            currObj.SetActive(true);

        currOp = OPERATION.COPY;
        SelectObj();
    }
    public void DeleteObj()
    {
        currOp = OPERATION.NONE;
        if (currObj == null)
            return;

        Destroy(currObj);
    }

    public void PasteObj()
    {
        if (currOp == OPERATION.CUT)
        {
            currObj.SetActive(true);
            return;
        }

        currOp = OPERATION.PASTE;
        if (currObj == null)
            return;

        // use name to create prefab
        string objName = currObj.transform.name;
        if (objName == "Cube" || objName == "Cube(Clone)")
            CopyCube(currObj);
        if (objName == "Sphere" || objName == "Sphere(Clone)")
            CopySphere(currObj);
        if (objName == "Capsule" || objName == "Capsule(Clone)")
            CopyCapsule(currObj);
        if (objName == "Cylinder" || objName == "Cylinder(Clone)")
            CopyCylinder(currObj);
    }

    // logic for copying items
    void CopyCube(GameObject currGO)
    {
        GameObject newGO = Instantiate(Cube,
            new Vector3(currObjPos.x + 1, currObjPos.y, currObjPos.z + 1),
            Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        currObj = newGO;
        currObjPos = currObj.transform.position;
        newGO.transform.parent = sceneParent.transform;
    }
    void CopySphere(GameObject currGO)
    {
        GameObject newGO = Instantiate(Sphere,
            new Vector3(currObjPos.x + 1, currObjPos.y, currObjPos.z + 1),
            Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        currObj = newGO;
        currObjPos = currObj.transform.position;
        newGO.transform.parent = sceneParent.transform;
    }
    void CopyCapsule(GameObject currGO)
    {
        GameObject newGO = Instantiate(Capsule,
            new Vector3(currObjPos.x + 1, currObjPos.y, currObjPos.z + 1),
            Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        currObj = newGO;
        currObjPos = currObj.transform.position;
        newGO.transform.parent = sceneParent.transform;
    }

    void CopyCylinder(GameObject currGO)
    {
        GameObject newGO = Instantiate(Cylinder,
            new Vector3(currObjPos.x + 1, currObjPos.y, currObjPos.z + 1),
            Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        currObj = newGO;
        currObjPos = currObj.transform.position;
        newGO.transform.parent = sceneParent.transform;
    }

    public void CreateCube()
    {
        
        GameObject newGO = Instantiate(Cube, new Vector3(0, 1, 0), Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        newGO.transform.parent = sceneParent.transform;
    }
    public void CreateSphere()
    {
        GameObject newGO = Instantiate(Sphere, new Vector3(0, 1, 0), Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        newGO.transform.parent = sceneParent.transform;
    }
    public void CreateCapsule()
    {
        GameObject newGO = Instantiate(Capsule, new Vector3(0, 1, 0), Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        newGO.transform.parent = sceneParent.transform;
    }

    public void CreateCylinder()
    {
        GameObject newGO = Instantiate(Cylinder, new Vector3(0, 1, 0), Quaternion.identity);
        RuntimeHierarchy.GetComponent<HierarchyManager>().currSelected_Scene = newGO;
        newGO.transform.parent = sceneParent.transform;
    }
}
