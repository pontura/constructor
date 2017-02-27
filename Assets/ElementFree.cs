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
		OnVerticesChangeState (false);
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
	public override void OnVerticesChangeState(bool show)
	{
		verticesDraggablesTopDown.verticeTop.assetShape.SetActive (show);
		verticesDraggablesTopDown.verticeBottom.assetShape.SetActive (show);
	}
	public override void OnVerticeActive(VerticeDraggable vd, bool isOver)
	{
		vd.GetComponent<VerticeTopDown>().OnRollOver(isOver);
	}

}
