using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour {
	
	public ElementFree elementFree;
	public DrawingAsset paintAsset;
	public Transform container;
	private float fpsToDraw = 0.5f;
	private float fps;
	private states state;
	private Vector3 drawingPos;

	private enum states
	{
		IDLE,
		DRAWING
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
		CheckToDraw ();		
	}
	void CheckToDraw()
	{
		fps += Time.deltaTime;
		if (fps > fpsToDraw)
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
		float scaleFactor = 1;
		if (World.Instance.size == UIZoom.sizes.BIG)
			scaleFactor = 100f;
		else if (World.Instance.size == UIZoom.sizes.MEDIUM)
			scaleFactor =  10f;
		else
			scaleFactor = 1;


		DrawingAsset[] assets = container.GetComponentsInChildren<DrawingAsset> ();

		if (assets.Length < 3)
			return;

		element = Instantiate (elementFree);
		element.transform.SetParent (World.Instance.world.transform);
		//element.transform.eulerAngles = new Vector3 (90, 0, 0);
		element.transform.localPosition = new Vector3 (0, 1f, 0);
		
		points = new Vector2 [assets.Length];
		int id = 0;
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
	}
	void Delayed()
	{
		element.PointsReady(points);
	}
}
