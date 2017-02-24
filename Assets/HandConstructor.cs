using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandConstructor : MonoBehaviour {

    private Collider colliders;
    public states state;
	public VerticeDraggable verticeDraggable;
	public GameObject pivot;
	public Element carringElement;

	public Element activeElemet;
	public VerticeDraggable activeVertice;

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
		Events.OnHandOver += OnHandOver;

    }

	void OnHandOver(GameObject go, bool isOver)
	{
		if (isOver)
			AddOverObjects (go);
		else
			RemoveOverObjects (go);

	}
	void AddOverObjects(GameObject newGO)
	{
		if (isLeft && newGO.GetComponent<VerticeDraggable> ()) {
			activeVertice = newGO.GetComponent<VerticeDraggable> ();
			activeVertice.OnRollOver (true);
		} else if(newGO.GetComponent<Element> ()) {
			activeElemet = newGO.GetComponent<Element> ();	
			activeElemet.SetOver (true);
		}
	}
	void RemoveOverObjects(GameObject newGO)
	{
		if (isLeft && newGO.GetComponent<VerticeDraggable> ()) {	
			if(activeVertice)
				activeVertice.OnRollOver (false);
			activeVertice = null;
		}
		else if( newGO.GetComponent<Element> ()) 
			activeElemet = null;
	}	
	void OnTriggerRightDown()
	{
		if (activeElemet == null)
			return;
		if (character.state == Character.states.EDITING || character.state == Character.states.EDITING_FREE)
			StartCarryingElement (activeElemet);
		else if (character.state == Character.states.COLOR_PAINT)
			activeElemet.OnChangeColor (World.Instance.activeColor);
		else if (character.state == Character.states.DESTROY)
			activeElemet.DestroyElement();
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
		if (activeVertice == null)
			return;
		StartDragging (activeVertice);
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
