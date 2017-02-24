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
		verticesManager.HideAllVertices ();
	}
	public override void DestroyElement() { 
		verticesManager.OnDestroyElement ();
		base.DestroyElement ();
	}
	public override void OnStopBeingCarried() {
		constructor.RecalculateColliders ();
	}
	public override void OnStartBeingCarried() {
		verticesManager.HideAllVertices ();
	}
	public override void ShowOnlyOneVertice(VerticeDraggable vd, bool showIt)
	{
		verticesManager.ShowOnlyOneVertice (vd, showIt);
		if(showIt)
			this.isOverVertices = isOverVertices;
	}

}
