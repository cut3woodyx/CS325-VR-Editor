using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawGrid : MonoBehaviour
{
	//How many lines to show in the grid
	public int lines = 10;

	//Variables to change the grid visuals
	public float scale = 1f;
	public Color gridColor = new Color(.5f, .5f, .5f, .6f);
	public Material gridMaterial;

	void Start()
	{
		//Changes the grid colour based on input
		GetComponent<MeshFilter>().mesh = GridMesh(lines, scale);
		GetComponent<MeshRenderer>().material = gridMaterial;
		GetComponent<MeshRenderer>().material.color = gridColor;

		transform.position = Vector3.zero;
	}

	//Through code generate the lines meshes of the grid
	Mesh GridMesh(int lineCount, float scale)
	{
		float half = (lineCount / 2f) * scale;

		//To accomodate the grid lines to be equal
		lineCount++;

		//2 Vertices per line and 2 lines per grid
		Vector3[] lines = new Vector3[lineCount * 4];   
		Vector3[] normals = new Vector3[lineCount * 4];
		Vector2[] uv = new Vector2[lineCount * 4];
		int[] indices = new int[lineCount * 4];

		//Generates the mesh vertice datas
		int n = 0;
		for (int y = 0; y < lineCount; y++)
		{
			//If the lines is divisible 10 the uv will be 1, else 0
			//Sets the actual position of the vertices of the line
			indices[n] = n;
			uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
			lines[n++] = new Vector3(y * scale - half, 0f, -half);

			indices[n] = n;
			uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
			lines[n++] = new Vector3(y * scale - half, 0f, half);

			indices[n] = n;
			uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
			lines[n++] = new Vector3(-half, 0f, y * scale - half);

			indices[n] = n;
			uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
			lines[n++] = new Vector3(half, 0f, y * scale - half);
		}

		//Sets the normals of the lines
		for (int i = 0; i < lines.Length; i++)
		{
			normals[i] = Vector3.up;
		}

		//Generates the mesh based on the vertices data generated above.
		Mesh tm = new Mesh();

		tm.name = "GridMesh";
		tm.vertices = lines;
		tm.normals = normals;
		tm.subMeshCount = 1;
		tm.SetIndices(indices, MeshTopology.Lines, 0);
		tm.uv = uv;

		return tm;
	}
}
