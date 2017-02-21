using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	public Material mat_onMovement;
	public Material mat_normal;
	public Material mat_over;

    public types type;

	Rigidbody _rigidBody;
	Collider _colliders;

	public ElementChilds childs;
	public ElementSnapping snapping;
	public states state;
	public MeshRenderer meshRenderer;


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
		meshRenderer = GetComponent<MeshRenderer> ();
		childs = GetComponent<ElementChilds> ();
		_colliders = GetComponent<Collider> ();
		_rigidBody = GetComponent<Rigidbody> ();

		snapping = GetComponent<ElementSnapping> ();

    }
	void Update()
	{
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
		if (other.name == "handOverCollider") {
			OnOver (true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.name == "handOverCollider") {
			OnOver (false);
		}
	}
	void OnOver(bool isOver)
	{
		if (isOver) {
			Events.OnChangeLeftInteractiveState (this.gameObject, true);
		}
		else {
			Events.OnChangeLeftInteractiveState (this.gameObject, false);
			SetOver (false);
		}
	}
	public void SetOver(bool isOver)
	{
		if (state == states.EDITING)
			return;
		if (state == states.CARRYING)
			return;
		if (isOver) {
			meshRenderer.material = mat_over;
		}
		else
		{
			meshRenderer.material = mat_normal;
		}
	}

	public virtual void ShowOnlyOneVertice(VerticeDraggable vd, bool isOver) { }

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
		//print ("SetPhysics " + active);
		if(!active)
			_rigidBody.velocity = Vector3.zero;
		_rigidBody.isKinematic = !active;
		_rigidBody.useGravity = active;
		_colliders.enabled = active;
		_rigidBody.velocity = new Vector3 (0f, -0.001f, 0f);
	}

}
