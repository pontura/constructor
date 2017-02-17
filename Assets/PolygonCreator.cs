﻿using UnityEngine;

public class PolygonCreator : MonoBehaviour {

	public Material mat;

	int[] trianlges;

	private MeshFilter filter;
	private Mesh mesh;

	private Element element;
	Vector3[] vertices;
	Vector2[] uvs;

	Triangulator triangulator;
	int polyLength;

	private float offsetInitial_down= 	-0.05f;
	private float offsetInitial_up = 	0.05f;

	public bool editableMode;

	public Vector3 GetCenter(bool top)
	{
		Vector3 prom = Vector3.zero;
		for (int a = 0; a < polyLength; a++)
			prom += vertices [a];
		Vector3 pos = prom / polyLength;
		if (top)
			pos.y = offsetInitial_up;
		else
			pos.y = offsetInitial_down;
		return pos;
	}
	public Vector3[] GetVerticesByFace(bool top)
	{
		Vector3[] verticesOfFace = new Vector3[polyLength];
		int id = 0;
		foreach (Vector3 v in vertices) {
			if (top && id < polyLength) {
				verticesOfFace [id] = v;
				id++;
			} else if (top && id > polyLength) {
				verticesOfFace [id] = v;
				id++;
			}			
		}
		return verticesOfFace;
	}
	public void MoveTop(float qty)
	{
		offsetInitial_up += qty;
	}
	public void MoveBottom(float qty)
	{
		offsetInitial_down += qty;
	}
	public void Create (Vector2[] v2d) {

		element = GetComponent<Element> ();

		triangulator = new Triangulator(v2d);
		trianlges = triangulator.Triangulate();
		vertices = new Vector3[v2d.Length*2];
		uvs = new Vector2[v2d.Length*2];

		polyLength = v2d.Length;

		for(int i=0;i<v2d.Length;i++)
		{
			vertices[i].x = v2d[i].x;
			vertices[i].z = v2d[i].y;
			vertices[i+v2d.Length].x = v2d[i].x;
			vertices[i+v2d.Length].z = v2d[i].y; 
		}
		filter = GetComponent<MeshFilter> ();
		mesh = filter.mesh;
		CreateMesh ();
		gameObject.GetComponent<MeshRenderer> ().material = mat;
	}
	public void RecalculateColliders()
	{
		done = 0;
	}
	public void SetEditableMode(bool isActive)
	{
		editableMode = isActive;
	}
	int done;
	void Update()
	{				
		if (polyLength == 0)
			return;
		if (done>2 && editableMode == false)
			return;
		CreateMesh ();
		done++;
	}
	void CreateMesh()
	{

		GetComponent<MeshCollider>().sharedMesh = filter.mesh;

		for(int i=0;i<polyLength;i++)
		{
			vertices[i].y = offsetInitial_up; // front vertex
			//uvs[i] = new Vector2(vertices[i].x, vertices[i].z); // front vertex
			vertices[i+polyLength].y = offsetInitial_down;  // back vertex    
			//uvs[i+polyLength] = new Vector2(vertices[i+polyLength].x, vertices[i+polyLength].z);  // back vertex    
		};

		int[] triangles = new int[trianlges.Length*2+polyLength*6];

		int count_tris = 0;
		for(int i=0;i<trianlges.Length;i+=3)
		{
			triangles[i] = trianlges[i];
			triangles[i+1] = trianlges[i+1];
			triangles[i+2] = trianlges[i+2];
		} 
		// front vertices
		count_tris+=trianlges.Length;
		for(int i=0;i<trianlges.Length;i+=3)
		{
			triangles[count_tris+i] = trianlges[i+2]+polyLength;
			triangles[count_tris+i+1] = trianlges[i+1]+polyLength;
			triangles[count_tris+i+2] = trianlges[i]+polyLength;
		} // back vertices
		count_tris+=trianlges.Length;

		//0,2,3,//first triangle
		//3,1,0,//second triangle


		for(int i=0;i<polyLength;i++)
		{
			// triangles around the perimeter of the object
			int n = (i+1)%polyLength;
			triangles[count_tris] = i;
			triangles[count_tris+1] = i + polyLength;
			triangles[count_tris+2] =  n + polyLength;
			triangles[count_tris+3] =  n + polyLength;
			triangles[count_tris+4] = n;
			triangles[count_tris+5] = i;

			count_tris += 6;
		}

		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//mesh.uv = uvs;
		mesh.RecalculateNormals();



		return;
		////////ORIGINAL:
		for(int i=0;i<polyLength;i++)
		{
			// triangles around the perimeter of the object
			int n = (i+1)%polyLength;
			triangles[count_tris] = i;
			triangles[count_tris+1] =  n;
			triangles [count_tris + 2] = i + polyLength;
			triangles[count_tris+3] = n;
			triangles[count_tris+4] = n + polyLength;
			triangles[count_tris+5] = i + polyLength;

			count_tris += 6;
		}
		//////////////////////
		/// 
		/// 
		/// este anda mas o menos bien:
		//////////////
		for(int i=0;i<polyLength;i++)
		{
			// triangles around the perimeter of the object
			int n = (i+1)%polyLength;
			triangles[count_tris] = i;
			triangles[count_tris+1] = i + polyLength;
			triangles[count_tris+2] =  n + polyLength;
			triangles[count_tris+3] =  n + polyLength;
			triangles[count_tris+4] = n;
			triangles[count_tris+5] = i;

			count_tris += 6;
		}
	}
}