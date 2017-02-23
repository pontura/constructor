using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour {

	public bool DEBUGGER;
	public PolygoncreatorDebug polygoncreatorDebug;
	public ElementFree elementFree;
	public DrawingAsset paintAsset;
	public Transform container;
	private float fpsToDraw = 0.005f;
	private float fps;
	private states state;
	private Vector3 drawingPos;

	public enum states
	{
		IDLE,
		DRAWING
	}
	void Start()
	{
		if (DEBUGGER)
			CreatePolygonDebug ();
	}
	public void Init()
	{
		state = states.DRAWING;
		fps = 0;
	}
	public void End()
	{
		state = states.IDLE;
		CreatePolygon ();
	}
	public void SetPosition(Vector3 pos)
	{
		this.drawingPos = pos;
	}
	void Update()
	{
		if (state == states.IDLE)
			return;
		else
			CheckToDraw ();		
	}
	void CheckToDraw()
	{
		fps += Time.deltaTime;
		if (fps >= fpsToDraw)
			Draw ();
	}
	void Draw()
	{
		fps = 0;
		DrawingAsset go = Instantiate (paintAsset);
		go.transform.SetParent (container);
		go.transform.position = drawingPos;
	}
	ElementFree element;
	Vector2[] points;
	void CreatePolygon()
	{			
		
		float scaleFactor = GetScaleFactor ();
		int resta = 4;

		DrawingAsset[] assets;

		assets = new DrawingAsset [container.GetComponentsInChildren<DrawingAsset> ().Length/resta];

		if (assets.Length < 3)
			return;
		
		int selectedPointID = 0;
		int id = 0;
		foreach (DrawingAsset da in container.GetComponentsInChildren<DrawingAsset> ()) {
			if (selectedPointID == (resta-1)) {
				assets [id] = da;
				id++;
				selectedPointID = 0;
			}	else		
				selectedPointID++;
		}



		element = Instantiate (elementFree);
		element.transform.SetParent (World.Instance.world.transform);
		element.transform.localPosition = new Vector3 (0, 1f, 0);
		
		points = new Vector2 [assets.Length];
		id = 0;
		foreach (DrawingAsset go in assets) {
			Vector3 _pos_vector3 = go.transform.localPosition;
			_pos_vector3 /= scaleFactor;
			float _x = (_pos_vector3.x);
			float _y = (_pos_vector3.z);
			Vector2 pos = new Vector2 (_x, _y);
			if (pos.x != 0 && pos.y != 0) {
				points [id] = pos;
			}			
			id++;
		}
		Utils.RemoveAllChildsIn (container);
		Invoke ("Delayed", 0.1f);
		state = states.IDLE;
	}
	void Delayed()
	{
		element.PointsReady(points);
	}
	float GetScaleFactor()
	{
		float scaleFactor = 1;
		if (World.Instance.size == UIZoom.sizes.BIG)
			scaleFactor = 100f;
		else if (World.Instance.size == UIZoom.sizes.MEDIUM)
			scaleFactor =  10f;
		else
			scaleFactor = 1;
		
		return scaleFactor;
	}



	PolygoncreatorDebug Pe;

	void CreatePolygonDebug()
	{			
		float scaleFactor = 1;
		int resta = 4;

		DrawingAsset[] assets = new DrawingAsset [container.GetComponentsInChildren<DrawingAsset> ().Length];

		if (assets.Length < 3)
			return;

		int selectedPointID = 0;
		int id = 0;
		foreach (DrawingAsset da in container.GetComponentsInChildren<DrawingAsset> ()) {
			assets [id] = da;
			id++;
		}


		Pe = Instantiate (polygoncreatorDebug);
		Pe.transform.localPosition = new Vector3 (0, 0, 0);

		points = new Vector2 [assets.Length];
		id = 0;
		foreach (DrawingAsset go in assets) {
			Vector3 _pos_vector3 = go.transform.localPosition;
			_pos_vector3 /= scaleFactor;
			float _x = (_pos_vector3.x);
			float _y = (_pos_vector3.z);
			Vector2 pos = new Vector2 (_x, _y);
			//if (pos.x != 0 && pos.y != 0) {
				points [id] = pos;
			//}			
			id++;
		}
		Utils.RemoveAllChildsIn (container);
		Invoke ("DelayedDebug", 0.1f);
		state = states.IDLE;
	}
	void DelayedDebug()
	{
		Pe.Create(points);
	}

}
