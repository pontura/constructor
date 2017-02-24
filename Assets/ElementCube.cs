using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCube : Element {

	MeshConstructor constructor;
	public VerticesManager verticesManager;
	public bool isOverVertices;

	public override void Start () {
		base.Start ();
		constructor = GetComponent<MeshConstructor> ();
		verticesManager = GetComponent<VerticesManager> ();
		verticesManager.AllVerticesSetState (false);
	}
	public override void OnVerticesChangeState(bool show)
	{
		verticesManager.AllVerticesSetState (show);
	}
	public override void DestroyElement() { 
		verticesManager.OnDestroyElement ();
		base.DestroyElement ();
	}
	public override void OnStopBeingCarried() {
		constructor.RecalculateColliders ();
	}
	public override void OnStartBeingCarried() {
		verticesManager.AllVerticesSetState (false);
	}
	public override void OnVerticeActive(VerticeDraggable vd, bool showIt)
	{
		verticesManager.OnVerticeActive (vd, showIt);
		if(showIt)
			this.isOverVertices = isOverVertices;
	}

}
