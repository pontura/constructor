using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticesDraggablesTopDown : MonoBehaviour {

	public PolygonCreator polygonCreator;
	public VerticeTopDown verticeTop;
	public VerticeTopDown verticeBottom;

	public void Init () {
		polygonCreator = GetComponent<PolygonCreator> ();
		verticeTop.SetFace (VerticeTopDown.faces.TOP);
		verticeBottom.SetFace (VerticeTopDown.faces.BOTTOM);

		verticeTop.transform.localPosition = polygonCreator.GetCenter (true);
		verticeBottom.transform.localPosition = polygonCreator.GetCenter (false);
	}
}
