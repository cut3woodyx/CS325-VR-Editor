using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeGizmos;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public float _cameraTranslateSpeed;
    public float _cameraRotateSpeed;
	public GameObject Grid, UI;
	public GameObject _gridToggle;

	public List<float> _cameraTranslateSpeeds;
	public List<float> _cameraRotateSpeeds;

	bool isIn2D, // disable rotation
		isInPreview; // disable camera translation
	Vector3 pos2D, pos3D, posPreview;
	Quaternion rot2D, rot3D, rotPreview;
	//Transform tr2D, tr3D;	// Translate seems to use ref

	public void SetCameraTranslationSpeed(int index)
    {
		_cameraTranslateSpeed = _cameraTranslateSpeeds[index];
    }

	public void SetCameraRotateSpeed(int index)
	{
		_cameraRotateSpeed = _cameraRotateSpeeds[index];
	}

	void Start()
    {
		isIn2D = isInPreview  = false;
		pos2D = pos3D = Camera.main.transform.position;
		pos2D = new Vector3(0, 10, 0);
		rot2D = rot3D = Camera.main.transform.rotation;
		rot2D = Quaternion.Euler(90, 0, 0);

		posPreview = new Vector3(-2,1.5f,-8);
		rotPreview = Quaternion.Euler(0, 0, 0);
	}

    // Update is called once per frame
    void Update()
    {
		if (isInPreview)
		{
			if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
				Camera.main.transform.localEulerAngles += new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * _cameraRotateSpeed;
			if (Input.GetMouseButton(1))
				OffScenePreview();
			return;
		}

			//Alt + Left Click : Camera Rotation
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0) && !isIn2D)
        {
			Camera.main.transform.localEulerAngles += new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * _cameraRotateSpeed;

			//See which is better
			/*float pitch = -Input.GetAxis("Mouse Y") * _cameraRotateSpeed;
			float yaw = Input.GetAxis("Mouse X") * _cameraRotateSpeed;

			pitch = Mathf.Clamp(pitch, -90f, 90f);

			if (yaw < 0)
			{
				yaw += 360f;
			}
			else if (yaw > 0)
			{
				yaw -= 360f;
			}

			Camera.main.transform.eulerAngles += new Vector3(pitch, yaw, 0);*/
		}

		//Alt + Middle Mouse
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2))
		{
			Vector3 rightMovement = -transform.right * Input.GetAxis("Mouse X");
			Vector3 upMovement = -transform.up * Input.GetAxis("Mouse Y");

			Camera.main.transform.position += (rightMovement + upMovement) *_cameraTranslateSpeed;
		}

		//Alt + Mouse Wheel
		if (Input.GetKey(KeyCode.LeftAlt) && Input.mouseScrollDelta.y != 0)
		{
			if (isIn2D)
			{
				if (Camera.main.transform.position.y > 1 &&
					Camera.main.transform.position.y < 20)
					Camera.main.transform.position += new Vector3(0, Input.mouseScrollDelta.y, 0);
				//if (Camera.main.orthographicSize - Input.mouseScrollDelta.y > 0 &&
				//	Camera.main.orthographicSize - Input.mouseScrollDelta.y < 20)
				//	Camera.main.orthographicSize -= Input.mouseScrollDelta.y;
			}
			else
			{
				Vector3 forwardMovement = transform.forward * Input.mouseScrollDelta.y;
				Camera.main.transform.position += forwardMovement * _cameraTranslateSpeed;
			}
		}

		// constant update to pos and rot values
		if (isIn2D)
			pos2D = Camera.main.transform.position; // no rot set cause cant rotate
		else
		{
			pos3D = Camera.main.transform.position;
			rot3D = Camera.main.transform.rotation;
		}
	}


	public void OnView2D() // Called by "View > 2D" Button
	{
		// set camera into 2D view mode
		isInPreview = false;
		isIn2D = Camera.main.orthographic = true;
		Camera.main.transform.position = pos2D;
        Camera.main.transform.rotation = rot2D;

		Grid.transform.position += new Vector3(0,1,0);
	}
	
	public void OnView3D() // Called by "View > 3D" Button
	{
		// set camera into 3D view mode
		isInPreview = false;
		isIn2D = Camera.main.orthographic = false;
		Camera.main.transform.position = pos3D;
		Camera.main.transform.rotation = rot3D;

		Grid.transform.position -= new Vector3(0,1,0);
	}

	public void OnScenePreview() // Called by "View > Scene Preview"
	{
		// set camera into preview mode
		isInPreview = true;
		UI.SetActive(false);
		this.gameObject.GetComponent<TransformGizmo>().enabled = false;
		Camera.main.rect = new Rect(0,0,1,1);
		Camera.main.orthographic = false;
		Camera.main.transform.position = posPreview;
		Camera.main.transform.rotation = rotPreview;
	}

	void OffScenePreview()
    {
		// disable preview mode
		// LMB detection in Update()
		isInPreview = false;
		UI.SetActive(true);
		this.gameObject.GetComponent<TransformGizmo>().enabled = true;
		Camera.main.rect = new Rect(0.15f, 0.24f, 0.7f, 0.62f);

		if (isIn2D)
			OnView2D();
		else
			OnView3D();
	}

	public void ToggleShowGrid() // Called by "View > Grid"
    {
		if (Grid.activeSelf == true)
		{
			Grid.SetActive(false);
			_gridToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
		}
		else
		{
			Grid.SetActive(true);
			_gridToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
		}
	}
}
