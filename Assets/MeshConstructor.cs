using UnityEngine;
using System.Collections;
 
public class MeshConstructor : MonoBehaviour
{    
    public Vector3 vertLeftTopFront = new Vector3(-1, 1, 1);
    public Vector3 vertRightTopFront = new Vector3(1, 1, 1);
    public Vector3 vertRightTopBack = new Vector3(1, 1, -1);
    public Vector3 vertLeftTopBack = new Vector3(-1, 1, -1);

    public Vector3 vertLeftBottomFront = new Vector3(-1, -1, 1);
    public Vector3 vertRightBottomFront = new Vector3(1, -1, 1);
    public Vector3 vertRightBottomBack = new Vector3(-1, -1, -1);
    public Vector3 vertLeftBottomBack = new Vector3(1, -1, -1);


    private MeshFilter mf;
    private Mesh mesh;

	public Element element;
    
	private float scaleFactor;
	public bool editableMode;

	void Awake()
	{		
		if (World.Instance.size == UIZoom.sizes.BIG)
			scaleFactor = 0.1f;
		else if (World.Instance.size == UIZoom.sizes.MEDIUM)
			scaleFactor = 1f;
		else
			scaleFactor = 10;
		
		vertLeftTopFront /= scaleFactor;
		vertRightTopFront /= scaleFactor;
		vertRightTopBack /= scaleFactor;
		vertLeftTopBack /= scaleFactor;

		vertLeftBottomFront /= scaleFactor;
		vertRightBottomFront /= scaleFactor;
		vertRightBottomBack /= scaleFactor;
		vertLeftBottomBack /= scaleFactor;
	}
	void Start()
	{
		element = GetComponent<Element> ();
		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;
		Events.OnResizeWorld += OnResizeWorld;
	}
	void OnResizeWorld(UIZoom.sizes _size)
	{
		//pontura:  esto es un hack por si pierde el Collider al hacer Resize (a veces pasa):
		Invoke("RecalculateColliders", 0.1f);
	}
    public void ChangeVertice(int id, Vector3 pos)
    {
        GetVerticeByID(id, pos);
    }
    public Vector3 GetVerticeByID(int id, Vector3 setNewPos)
    {
        switch (id)
        {
            case 0: if (setNewPos != Vector3.zero) { vertLeftTopFront = setNewPos; };  return vertLeftTopFront;
            case 1: if (setNewPos != Vector3.zero) { vertRightTopFront = setNewPos; }; return vertRightTopFront;
            case 2: if (setNewPos != Vector3.zero) { vertRightTopBack = setNewPos; }; return vertRightTopBack;
            case 3: if (setNewPos != Vector3.zero) { vertLeftTopBack = setNewPos; }; return vertLeftTopBack;

            case 4: if (setNewPos != Vector3.zero) { vertLeftBottomFront = setNewPos; }; return vertLeftBottomFront;
            case 5: if (setNewPos != Vector3.zero) { vertRightBottomFront = setNewPos; }; return vertRightBottomFront;
            case 6: if (setNewPos != Vector3.zero) { vertRightBottomBack = setNewPos; }; return vertRightBottomBack;
            case 7: if (setNewPos != Vector3.zero) { vertLeftBottomBack = setNewPos; }; return vertLeftBottomBack;
            default: return Vector3.zero;
        }
        
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
		if (done>1 && editableMode == false)
			return;
		ConstructorWork ();
		done++;
		//if (editableMode) {
		//	element.StartBeingEditted ();
		//}
	}
	void ConstructorWork()
	{
		GetComponent<MeshCollider>().sharedMesh = mf.mesh;
		//Vertices//
		Vector3[] vertices = new Vector3[]
		{
			//front face//
			vertLeftTopFront,//left top front, 0
			vertRightTopFront,//right top front, 1
			vertLeftBottomFront,//left bottom front, 2
			vertRightBottomFront,//right bottom front, 3

			//back face//
			vertRightTopBack,//right top back, 4
			vertLeftTopBack,//left top back, 5
			vertLeftBottomBack,//left bottom back, 6
			vertRightBottomBack,//right bottom back, 7

			//left face//
			vertLeftTopBack,//left top back, 8
			vertLeftTopFront,//left top front, 9
			vertRightBottomBack,//right bottom back, 10
			vertLeftBottomFront,//left bottom front, 11

			//right face//
			vertRightTopFront,//right top front, 12
			vertRightTopBack,//right top back, 13
			vertRightBottomFront,//right bottom front, 14
			vertLeftBottomBack,//left bottom back, 15

			//top face//
			vertLeftTopBack,//left top back, 16
			vertRightTopBack,//right top back, 17
			vertLeftTopFront,//left top front, 18
			vertRightTopFront,//right top front, 19

			//bottom face//
			vertLeftBottomFront,//left bottom front, 20
			vertRightBottomFront,//right bottom front, 21
			vertRightBottomBack,//left bottom back, 22
			vertLeftBottomBack//right bottom back, 23

		};

		//Triangles// 3 points, clockwise determines which side is visible
		int[] triangles = new int[]
		{
			//front face//
			0,2,3,//first triangle
			3,1,0,//second triangle

			//back face//
			4,6,7,//first triangle
			7,5,4,//second triangle

			//left face//
			8,10,11,//first triangle
			11,9,8,//second triangle

			//right face//
			12,14,15,//first triangle
			15,13,12,//second triangle

			//top face//
			16,18,19,//first triangle
			19,17,16,//second triangle

			//bottom face//
			20,22,23,//first triangle
			23,21,20//second triangle
		};

		//UVs//
		Vector2[] uvs = new Vector2[]
		{
			//front face// 0,0 is bottom left, 1,1 is top right//
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0),

			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0),

			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0),

			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0),

			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0),

			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0)
		};

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals();
	}
}