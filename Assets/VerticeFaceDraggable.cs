using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticeFaceDraggable : VerticeDraggable {

	public List<VerticeDraggable> childs;
	public GameObject assetShape;
	public faces face;
	private MeshConstructor meshConstructor;

	public enum faces
	{
		TOP,
		BOTTOM,
		RIGHT,
		LEFT,
		BACK,
		FRONT
	}
	public void Init(MeshConstructor mc, int _id, Vector3 _vectorToModify)
	{
		this.element = mc.GetComponent<ElementCube> ();
		this.meshConstructor = mc;
		this.id = _id;
		transform.localPosition = _vectorToModify;
		gameObject.layer = 8;
	}
	public override void UpdatedByConstructor()
	{
		meshConstructor.ChangeVertice(id, transform.localPosition);
	}
	public override void StartDragging()
	{
		lastUpdateVector = Vector3.zero;
		meshConstructor.SetEditableMode (true);
		meshConstructor.element.StartBeingEditted ();
	}
	public override void StopDragging()
	{		
		meshConstructor.SetEditableMode (false);
		meshConstructor.element.StopBeingEditted ();
	}
	void Start()
	{
		Events.OnResizeWorldMultiplier += OnResizeWorldMultiplier;
	}
	void OnResizeWorldMultiplier(float multiplier)
	{
		assetShape.transform.localScale *= multiplier;
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
		case faces.RIGHT:
			rot = new Vector3 (0, 0, -90);
			break;
		case faces.LEFT:
			rot = new Vector3 (0, 0, 90);
			break;
		case faces.BACK:
			rot = new Vector3 (90, 0, 0);
			break;
		case faces.FRONT:
			rot = new Vector3 (-90, 0, 0);
			break;
		}
		assetShape.transform.localEulerAngles = rot;
	}
	public override void NewPos(Vector3 updaterVector)
	{
		FixedPositionByFace (updaterVector, face);
		foreach (VerticeDraggable vd in childs) {
			vd.FixedPositionByFace (updaterVector, face);
		}
	}
}
