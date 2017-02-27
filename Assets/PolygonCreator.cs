using UnityEngine;

public class PolygonCreator : MonoBehaviour {

	public Material mat;

	int[] trianlges;

	private MeshFilter filter;
	private Mesh mesh;

	private Element element;
	Vector3[] vertices;

	Triangulator triangulator;
	int polyLength;

	private float offsetInitial_down= 	-0.1f;
	private float offsetInitial_up = 	0.1f;

	public bool editableMode;

	private MeshCollider meshcollider; 

	void Start()
	{
		meshcollider = GetComponent<MeshCollider> ();
	}
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
		vertices = new Vector3[v2d.Length*6];

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

		CalculateNormals ();

		if (done>2 && editableMode == false)
			return;
		CreateMesh ();
		done++;
	}
	void CreateMesh()
	{
		meshcollider.sharedMesh = filter.mesh;

		for(int i=0;i<polyLength;i++)
		{
			vertices[i].y = offsetInitial_up; 
			vertices[i+polyLength].y = offsetInitial_down;

			CreateFreeVertice (vertices[i], i+"a");
			CreateFreeVertice (vertices[i+polyLength], i+"b");
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

		int verticesCount = polyLength*2;

		//paredes :
		for(int i=0;i<polyLength;i++)
		{
			int next = (i+1)%polyLength;

			int A = verticesCount++;
			int B = verticesCount++;
			int C = verticesCount++;
			int D = verticesCount++;

			vertices[A] = vertices[i]; // front vertex			
			vertices[B] = vertices[i+polyLength]; // front vertex
			vertices[C] = vertices[next]; // front vertex
			vertices[D] = vertices[next+polyLength]; // front vertex

			triangles[count_tris] = A;
			triangles[count_tris+1] = B;
			triangles[count_tris+2] = C;

			triangles[count_tris+3] = B;
			triangles[count_tris+4] = D;
			triangles[count_tris+5] = C;

			count_tris += 6;
		}

		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		CalculateNormals ();

	}
	void CalculateNormals()
	{
		mesh.RecalculateNormals();
		Invoke ("CalculateNormals", 1);
	}
	void CreateFreeVertice(Vector3 pos, string id)
	{
		//FreeVertice fv = Instantiate (freeVertice);
		//fv.transform.localPosition = pos;
		//fv.name = id + " (x:" + pos.x + " y:" + pos.y + " z:" + pos.z + ")";
	}
}