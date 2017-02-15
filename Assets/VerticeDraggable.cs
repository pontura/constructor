using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticeDraggable : MonoBehaviour {
    
    public int id;
    private MeshConstructor meshConstructor;
    Transform parent;
	private int factor = 10;
	public List<VerticeDraggable> childs;

	public Material mat_normal;
	public Material mat_over;
	public Element element;

	public Vector3 lastUpdateVector;


    public void Init(MeshConstructor mc, int _id, Vector3 _vectorToModify)
    {
		this.element = mc.GetComponent<Element> ();
        this.meshConstructor = mc;
        this.id = _id;
        transform.localPosition = _vectorToModify;
        parent = transform.parent;
		gameObject.layer = 8;
    }
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "handOverCollider") {
			OnOver (true);
		} else if(element.state == Element.states.CARRYING){
			VerticeDraggable otherVD = other.GetComponent<VerticeDraggable> ();
			if (otherVD != null) {
				if (element.transform.position.y > otherVD.element.transform.position.y) {
					element.verticesManager.AddVerticestoSnap (this, otherVD);
				}
			}
		} 
	}
	void OnTriggerExit(Collider other)
	{
		if (other.name == "handOverCollider") {
			OnOver (false);
		} else {
			VerticeDraggable otherVD = other.GetComponent<VerticeDraggable> ();
			if (otherVD != null) 
				element.verticesManager.RemoveVerticesToSnap ();
		} 
	}
	public void OnOver(bool isOver)
	{		
		element.OnOverOnVertices (isOver);
		if (isOver)
			Events.OnChangeLeftInteractiveState (this.gameObject, true);
		else {
			Events.OnChangeLeftInteractiveState (this.gameObject, false);
			SetOver (false);
		}
	}
	public void SetOver(bool isOver)
	{
		if(isOver)
			ChangeMaterials( mat_over);
		else
			ChangeMaterials( mat_normal);
	}
	public virtual void ChangeMaterials(Material mat)
	{
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			mr.material = mat;
	}
	public void SetFaceType()
	{
		gameObject.layer = 13;
	}
    public void UpdatedByConstructor()
    {
        meshConstructor.ChangeVertice(id, transform.localPosition);
    }
	public void StartDragging()
	{
		lastUpdateVector = Vector3.zero;
		meshConstructor.SetEditableMode (true);
		meshConstructor.element.StartBeingEditted ();
	}
	public void StopDragging()
	{		
		meshConstructor.SetEditableMode (false);
		meshConstructor.element.StopBeingEditted ();
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
}
