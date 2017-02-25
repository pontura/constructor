using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticeDraggable : MonoBehaviour {
    
    public int id;

	public Color activeColor;
	public Color defaultColor;
    
	private int factor = 10;

	public Element element;

	public Vector3 lastUpdateVector;
	private MeshRenderer[] meshRenderer;

	public virtual void Start()
	{
		meshRenderer = GetComponentsInChildren<MeshRenderer> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "handOverColliderLeft") {
			OnOver (true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.name == "handOverColliderLeft") {
			OnOver (false);
			OnRollOver (false);
		}
	}
	public void OnDestroyed()
	{
		Events.OnHandOver (this.gameObject, false);
	}
	bool isOver;
	public void OnOver(bool _isOver)
	{	
		isOver = _isOver;
		if (_isOver) {
			Events.OnHandOver (this.gameObject, true);
		}
		else {
			Events.OnHandOver (this.gameObject, false);
		}
		element.OnVerticeActive(this, isOver);
	}
	public void OnRollOver(bool isOver)
	{
		if (meshRenderer == null)
			return;
		foreach (MeshRenderer mr in meshRenderer) {
			if(isOver)
				mr.material.color = activeColor;
			else
				mr.material.color = defaultColor;
		}
	}
	public void SetFaceType()
	{
		gameObject.layer = 13;
	}
    public virtual void UpdatedByConstructor()
    {
    }
	public virtual void StartDragging()
	{
	}
	public virtual void StopDragging()
	{		
	}
    public void UpdatePosition(Vector3 newWorldPosition)
    {
		Vector3 pos = new Vector3 (
			              ToDecimals (newWorldPosition.x),
			              ToDecimals (newWorldPosition.y),
			              ToDecimals (newWorldPosition.z));

		if (lastUpdateVector == pos)
			return;
		
		NewPos (pos-lastUpdateVector);
		lastUpdateVector = pos;
    }
	public virtual void NewPos(Vector3 pos)
	{
		
	}
	float ToDecimals(float num)
	{
		return Mathf.Round( num *10) /10;
	}
	public void FixedPositionByFace(Vector3 globalDelta, VerticeFaceDraggable.faces face)
	{
		Vector3 localP = transform.localPosition;
		transform.position += globalDelta;
		switch (face) {
		case VerticeFaceDraggable.faces.CORNER:
			localP.x = transform.localPosition.x;
			localP.y = transform.localPosition.y;
			localP.z = transform.localPosition.z;
			break;
		case VerticeFaceDraggable.faces.TOP:
		case VerticeFaceDraggable.faces.BOTTOM:
			localP.y = transform.localPosition.y;
			break;

		case VerticeFaceDraggable.faces.RIGHT:
		case VerticeFaceDraggable.faces.LEFT:
			localP.x = transform.localPosition.x;
			break;

		case VerticeFaceDraggable.faces.BACK:
		case VerticeFaceDraggable.faces.FRONT:
			localP.z = transform.localPosition.z;
			break;
		}
		transform.localPosition = localP;
	}
	public void FixedPositionByFaceTopDown(Vector3 globalDelta, VerticeTopDown.faces face)
	{
		Vector3 localP = transform.localPosition;
		transform.position += globalDelta;
		switch (face) {
		case VerticeTopDown.faces.TOP:
		case VerticeTopDown.faces.BOTTOM:
			localP.y = transform.localPosition.y;
			break;
		}
		transform.localPosition = localP;
	}
}
