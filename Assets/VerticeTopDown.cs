using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticeTopDown : VerticeDraggable {

	public GameObject assetShape;
	public faces face;
	public PolygonCreator polygonCreator;
	public Element element;

	public enum faces
	{
		TOP,
		BOTTOM
	}
	void Start()
	{
		Events.OnResizeWorldMultiplier += OnResizeWorldMultiplier;
	}
	void OnDestroy()
	{
		Events.OnResizeWorldMultiplier -= OnResizeWorldMultiplier;
	}
	void OnResizeWorldMultiplier(float multiplier)
	{
		assetShape.transform.localScale *= multiplier;
	}
	public override void StartDragging()
	{
		lastUpdateVector = Vector3.zero;
		polygonCreator.SetEditableMode (true);
		element.StartBeingEditted ();
	}
	public override void StopDragging()
	{		
		polygonCreator.SetEditableMode (false);
		element.StopBeingEditted ();
	}

	public void SetFace(faces _face)
	{
		this.face = _face;
		SetAsset ();
	}
	void SetAsset()
	{
		Vector3 rot = Vector3.zero;
		switch (face) {
		case faces.BOTTOM:
			rot = new Vector3 (180, 0, 0);
			break;
		}
		assetShape.transform.localEulerAngles = rot;
	}
	public override void NewPos(Vector3 updaterVector)
	{
		FixedPositionByFaceTopDown (updaterVector, face);
		switch (face) {
		case faces.TOP:
			polygonCreator.MoveTop (updaterVector.y);
			break;
		case faces.BOTTOM:
			polygonCreator.MoveBottom (updaterVector.y);
			break;
		}
	}
}
