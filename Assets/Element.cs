﻿using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	public Color mat_normal;
	public Color mat_over;

    public types type;

	Rigidbody _rigidBody;
	Collider _colliders;

	public ElementChilds childs;
	public ElementSnapping snapping;
	public states state;
	public MeshRenderer meshRenderer;
	private Color color;


	public enum states
	{
		IDLE,
		CARRYING,
		EDITING,
		INACTIVE
	}

    public enum types
    {
        CUBE
    }
	
	public virtual void Start () {
		Events.OnRecalculateGravity += OnRecalculateGravity;

		this.color = World.Instance.activeColor;
		meshRenderer = GetComponent<MeshRenderer> ();
		childs = GetComponent<ElementChilds> ();
		_colliders = GetComponent<Collider> ();
		_rigidBody = GetComponent<Rigidbody> ();

		snapping = GetComponent<ElementSnapping> ();

		if (!World.Instance.useGravity) {
			_rigidBody.velocity = Vector3.zero;
			_rigidBody.isKinematic = true;
			_rigidBody.useGravity = false;
			_colliders.enabled = true;
		}
    }
	void OnDestroy()
	{
		Events.OnRecalculateGravity -= OnRecalculateGravity;
	}
	void OnRecalculateGravity()
	{
		_rigidBody.isKinematic = false;
		_rigidBody.useGravity = true;
		_colliders.enabled = true;
		state = states.INACTIVE;
		Invoke ("Delayed", 0.01f);
	}
	void Delayed()
	{
		state = states.IDLE;
	}
	void Update()
	{
		if (!World.Instance.useGravity)
			return;
		if (state == states.INACTIVE)
			return;
		if (_rigidBody.velocity == Vector3.zero && _rigidBody.isKinematic == false) {
			//print ("INACTIVE!!  " + state);
			_rigidBody.isKinematic = true;
			if(snapping)
				snapping.Init ();
			state = states.INACTIVE;
			//está snappeado
		} else if(_rigidBody.isKinematic == false){
			//está en movimiento
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "handOverColliderRight") {
			OnOver (true);
		} else if (other.name == "handOverColliderLeft") {
			OnVerticesChangeState (true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.name == "handOverColliderRight") {
			OnOver (false);
		} else if (other.name == "handOverColliderLeft") {
			OnVerticesChangeState (false);
		}
	}
	public virtual void OnVerticesChangeState(bool show){}

	void OnOver(bool isOver)
	{
		if (isOver) {
			//este es nuevo:SetOver (true);
			SetOver (true);
			Events.OnHandOver (this.gameObject, true);
		}
		else {
			Events.OnHandOver (this.gameObject, false);
			SetOver (false);
		}
	}
	public void SetOver(bool isOver)
	{
		if (isOver)
			meshRenderer.materials[0].color = mat_over;
		else {
			meshRenderer.materials[0].color = color;
		}
	}

	public virtual void OnVerticeActive(VerticeDraggable vd, bool isOver) { }

	private Transform lastParent;
	public void StartBeingCarried(Transform pivot)
    {		
		//print ("StartBeingCarried");	
		SetPhysics (false);
		OnStartBeingCarried ();
		lastParent = transform.parent;
		transform.SetParent (pivot);
		state = states.CARRYING;
    }
	public virtual void OnStartBeingCarried() {}
	public virtual void OnChangeColor(Color _color) {
		this.color = _color;
		meshRenderer.materials[0].color = color;
	}
	public virtual void DestroyElement() { 
		StopBeingCarried ();
		Invoke("DelayedDestroy", 0.2f);
	}
	void DelayedDestroy()
	{
		Destroy (gameObject);
	}
    public void StopBeingCarried()
    {	
		//print ("StopBeingCarried");	
		OnOver (false);
		OnStopBeingCarried ();
		SetPhysics (true);
		transform.SetParent (lastParent);
		state = states.IDLE;
		Events.StopCarrying (this);
    }
	public virtual void OnStopBeingCarried() {}

	public void StartBeingSnapped()
	{
	}
	public void StopBeingSnapped()
	{
	}
	public void StartBeingEditted()
	{		
		//print ("StartBeingEditted");	
		OnOver (false);
		state = states.EDITING;
		SetPhysics (false);
	}
	public void Repositionate()
	{
		SetPhysics (true);
		state = states.IDLE;
	}
	public void StopBeingEditted()
	{
		//print ("StopBeingEditted");	
		SetPhysics (true);
		state = states.IDLE;
	}
	void SetPhysics(bool active)
	{
		if (!World.Instance.useGravity)
			return;
		//print ("SetPhysics " + active);
		if(!active)
			_rigidBody.velocity = Vector3.zero;
		_rigidBody.isKinematic = !active;
		_rigidBody.useGravity = active;
		_colliders.enabled = active;
		_rigidBody.velocity = new Vector3 (0f, -0.001f, 0f);
	}

}
