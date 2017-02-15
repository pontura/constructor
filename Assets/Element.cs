using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	public Material mat_normal;
	public Material mat_over;

    public types type;

	Rigidbody _rigidBody;
	Collider _colliders;
	MeshConstructor constructor;
	public VerticesManager verticesManager;
	public bool isOverVertices;
	public ElementSnapping snapping;
	public states state;

	public enum states
	{
		IDLE,
		CARRYING
	}

    public enum types
    {
        CUBE
    }
	
	void Start () {
		Events.OnChangeLeftInteractiveState += OnChangeLeftInteractiveState;
		Events.OnActivateElements += OnActivateElements;
		_colliders = GetComponent<Collider> ();
		_rigidBody = GetComponent<Rigidbody> ();
		constructor = GetComponent<MeshConstructor> ();
		verticesManager = GetComponent<VerticesManager> ();
		snapping = GetComponent<ElementSnapping> ();
    }
	void OnActivateElements()
	{
		
	}
	void Update()
	{
		if (_rigidBody.velocity == Vector3.zero) {
			_rigidBody.isKinematic = true;
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
	public void Snapped()
	{
		_rigidBody.isKinematic = false;
		//float v = 0.1f;
		//_rigidBody.velocity = new Vector3 (v,v,v);
	}
	public void StopSnapping()
	{
		SetPhysics (true);
	}
	void OnOver(bool isOver)
	{
		if (isOverVertices)
			return;
		if (isOver)
			Events.OnChangeLeftInteractiveState (this.gameObject, true);
		else {
			Events.OnChangeLeftInteractiveState (this.gameObject, false);
			SetOver (false);
		}
	}
	public void SetOver(bool isOver)
	{
		if (isOver)
			GetComponent<MeshRenderer> ().material = mat_over;
		else
			GetComponent<MeshRenderer> ().material = mat_normal;
	}
	public void OnOverOnVertices(bool isOverAVertice)
	{
		if(isOverAVertice)
			GetComponent<MeshRenderer> ().material = mat_normal;
		this.isOverVertices = isOverVertices;
	}
	private Transform lastParent;
	public void StartBeingCarried(Transform pivot)
    {
		SetPhysics (false);
		lastParent = transform.parent;
		transform.SetParent (pivot);
		state = states.CARRYING;
    }
    public void StopBeingCarried()
    {
		constructor.RecalculateColliders ();
		SetPhysics (true);
		transform.SetParent (lastParent);
		state = states.IDLE;
		verticesManager.CheckVerticesToSnap ();
    }
	public void StartBeingEditted()
	{
		SetPhysics (false);
	}

	public void StopBeingEditted()
	{
		SetPhysics (true);
	}
	void SetPhysics(bool active)
	{
		if(!active)
			_rigidBody.velocity = Vector3.zero;
		_rigidBody.isKinematic = !active;
		_rigidBody.useGravity = active;
		_colliders.enabled = active;
	}
	void OnChangeLeftInteractiveState(GameObject go, bool isOver)
	{
	}

}
