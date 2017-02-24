using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFree : Element {

	public PolygonCreator polygonCreator;
	public VerticesDraggablesTopDown verticesDraggablesTopDown;

	public override void Start () {
		base.Start ();
		verticesDraggablesTopDown = GetComponent<VerticesDraggablesTopDown> ();
		polygonCreator = GetComponent<PolygonCreator> ();
	}
	public override void DestroyElement() { 
		base.DestroyElement ();
	}
	public void PointsReady(Vector2[] points)
	{
		polygonCreator.Create (points);
		verticesDraggablesTopDown = GetComponent<VerticesDraggablesTopDown> ();
		verticesDraggablesTopDown.Init ();
	}
	public override void OnStopBeingCarried() {
		polygonCreator.RecalculateColliders ();
	}
	public override void OnStartBeingCarried() {
	}
	public override void ShowOnlyOneVertice(VerticeDraggable vd, bool showIt)
	{
		//print("______ ElementFree + ShowOnlyOneVertice " + showIt);
		//if(showIt)
			//this.isOverVertices = isOverVertices;
	}

}
