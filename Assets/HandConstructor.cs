using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandConstructor : MonoBehaviour {

    private Collider colliders;
    public states state;
	public VerticeDraggable verticeDraggable;
	public GameObject pivot;
	public Element carringElement;
	public List<GameObject> overObjects;
	public bool isLeft;
	public Character character;

    public enum states
    {
        INACTIVE,
        PULLING,
        DRAGGING,
        CARRYING,
		STOPPED_CARRYING
    }

	void Start () {
        colliders = GetComponent<Collider>();
		Events.ChangeConstructionState += ChangeConstructionState;
		if (!isLeft) {
			Events.OnTriggerRightDown += OnTriggerRightDown;
			Events.OnTriggerRightUp += OnTriggerRighttUp;
		} else  {
			Events.OnTriggerLeftDown += OnTriggerLeftDown;
			Events.OnTriggerLeftUp += OnTriggerLeftUp;
		}
		Events.OnChangeLeftInteractiveState += OnChangeLeftInteractiveState;
    }

	void OnChangeLeftInteractiveState(GameObject go, bool isOver)
	{
		if (isOver)
			AddOverObjects (go);
		else
			RemoveOverObjects (go);

	}
	void AddOverObjects(GameObject newGO)
	{
		
		foreach (GameObject go in overObjects)
			if (go == newGO)
				return;

		//print ("newGO" + newGO.name);

		overObjects.Add (newGO);
		SetNewRollOver();
	}
	void RemoveOverObjects(GameObject newGO)
	{
		GameObject newGOToRemove = null;
		foreach (GameObject go in overObjects)
			if (go == newGO) {
				newGOToRemove = go;
				break;
			}
		
		if(newGOToRemove != null)
			overObjects.Remove (newGOToRemove);		

		Invoke("SetNewRollOver", 0.1f);
	}
	void ResetOvers()
	{
		foreach (GameObject go in overObjects) {
			if (go.GetComponent<Element> ()) 
				go.GetComponent<Element> ().SetOver (false);
		}
	}
	void SetNewRollOver()
	{
		GameObject go = GetActiveObject();

		if (go == null)
			return;
		if (go.GetComponent<VerticeDraggable> ()) {
			
		} else if (go.GetComponent<Element> ()) {
				go.GetComponent<Element> ().SetOver (true);
			}
	}
	GameObject GetActiveObject()
	{
		foreach (GameObject go in overObjects) {
			if (go == null)
				StartCoroutine( DestroyDelayed(go));
			else if (go.GetComponent<VerticeDraggable> ()) {
				return go;
			}
		}
		foreach (GameObject go in overObjects) {
			if (go == null)
				StartCoroutine( DestroyDelayed(go));
			else if (go!= null && go.GetComponent<Element> ()) {
				return go;
			}
		}
		return null;
	}
	IEnumerator DestroyDelayed(GameObject go)
	{
		yield return new WaitForSeconds (0.1f);
		overObjects.Remove (go);
	}
	void OnTriggerRightDown()
	{
		GameObject go = GetActiveObject ();
		if (go == null)
			return;
		
		Element element = go.GetComponent<Element> ();
		if (element == null)
			return;
		if (character.state == Character.states.EDITING)
			StartCarryingElement (element);
		else if (character.state == Character.states.COLOR_PAINT)
			element.OnChangeColor (World.Instance.activeColor);
		else if (character.state == Character.states.DESTROY)
			element.DestroyElement();
	}
	void OnTriggerRighttUp()
	{
		if (state == states.CARRYING)
			StopCarrying ();
		else if(carringElement != null)
			StopCarrying ();
	}


	void OnTriggerLeftDown()
	{
		GameObject go = GetActiveObject ();
		if (go == null)
			return;
		if (go.GetComponent<VerticeDraggable> ()) {
			StartDragging (go.GetComponent<VerticeDraggable> ());
		}
	}
	void OnTriggerLeftUp()
	{
		if (state == states.DRAGGING)
			DraggReleased();
	}


	void ChangeConstructionState(states newState)
	{
		this.state = newState;
	}

    void Update()
    {
		if (state == states.INACTIVE)
			Inactive ();
        else if (state == states.DRAGGING)
            Drag();
        else if (state == states.PULLING)
            Pull();
        else if (state == states.CARRYING)
            Carrying();
    }

	void StopCarrying()
	{
		if (state == states.STOPPED_CARRYING)
			return;

		CancelInvoke ();
		
		state = states.STOPPED_CARRYING;

		if (carringElement != null) {
			carringElement.StopBeingCarried ();	
			SetPivot(Vector3.zero);
			Invoke ("DelayToInactive", 0.6f);
			carringElement = null;
		} else {
			Invoke ("DelayToInactive", 0.05f);
		}
	}
	void DelayToInactive()
	{
		EnableColliders (false);
		state = states.INACTIVE;
		Inactive ();
	}
    void Inactive()
    {
		startDraggingPosition = Vector3.zero;
    }

	public void DraggReleased()
	{
		Events.DraggReleased();
		EnableColliders (false);
		if (verticeDraggable != null) {
			StopDragging (verticeDraggable);
			verticeDraggable = null;
		}
		gameObject.layer = 9;
		state = states.INACTIVE;
	}
	private Vector3 startDraggingPosition;
	void StartDragging(VerticeDraggable vd)
	{
		if (vd == null)
			return;
		verticeDraggable = vd;
		state = states.DRAGGING;
		vd.StartDragging ();

		//foreach (VerticeDraggable vdNew in vd.childs) {
		//	vdNew.StartDragging();
		//}
		startDraggingPosition = vd.transform.position;
	}
	void StopDragging(VerticeDraggable vd)
	{
		vd.StopDragging();
		//foreach (VerticeDraggable vdNew in vd.childs) {
		//	vdNew.StopDragging();
		//}
	}
    void Drag()
    {
		Vector3 sum = transform.position - startDraggingPosition;
		verticeDraggable.UpdatePosition (sum);
		//foreach (VerticeDraggable vd in verticeDraggable.childs) {
			//vd.UpdatePosition (sum);
		//}
    }
    void Pull()
    {
        //colliders.enabled = true;
    }
	Vector3 offset_Pos = Vector3.zero;
	Vector3 starting_Rot;
	void StartCarryingElement(Element element)
    {
		this.carringElement = element; 
		if (state == states.CARRYING)
			return;
        state = states.CARRYING;

		starting_Rot = transform.eulerAngles;
		offset_Pos =  element.transform.position - transform.position;
		SetPivot(offset_Pos);
		element.StartBeingCarried(pivot.transform);
    }
    void Carrying()
    {
    }
	void EnableColliders (bool enableThem)
	{
		colliders.enabled = enableThem;
	}
	void SetPivot(Vector3 pos)
	{
		pivot.transform.localPosition = pos;
	}
}
