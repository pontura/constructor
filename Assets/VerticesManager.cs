using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticesManager : MonoBehaviour {

	public List<VerticeDraggable> verticesDraggables;

	public VerticeFaceDraggable verticeDraggable;
	public VerticeFaceDraggable verticeFaceDraggable;
	MeshConstructor meshConstructor;

	private Vector3 verticesSize = new Vector3 (0.1f, 0.1f, 0.1f);

	private int totalVertices = 8;

	void Start () {
		meshConstructor = GetComponent<MeshConstructor> ();
		AddVerticeDraggables ();
		AddFaceVertices();
		Events.DraggReleased += DraggReleased;
	}
	void OnDestroy()
	{
		Events.DraggReleased -= DraggReleased;
	}
	public void ShowOnlyOneVertice(VerticeDraggable vd, bool showIt)
	{
		foreach (VerticeDraggable v in verticesDraggables)
			if (v == vd)
				v.asset.SetActive (showIt);
			else
				v.asset.SetActive (false);
	}
	public void HideAllVertices()
	{
		foreach (VerticeDraggable v in verticesDraggables)
			if(v.asset != null)
				v.asset.SetActive (false);
	}
	public void OnDestroyElement()
	{
		foreach (VerticeDraggable v in verticesDraggables)
			v.OnDestroyed ();
	}
	void DraggReleased()
	{
		RepositionateFaces ();
	}
	void AddVerticeDraggables()
	{
		for(int a = 0; a<totalVertices; a++)
		{
			VerticeFaceDraggable v = Instantiate(verticeDraggable);
			v.transform.SetParent(transform);
			v.Init(meshConstructor, a, meshConstructor.GetVerticeByID(a, Vector3.zero));
			verticesDraggables.Add(v);
			//v.transform.localScale = verticesSize;
		}       
	}
	void Update()
	{
		foreach (VerticeDraggable v in verticesDraggables)
			v.UpdatedByConstructor();
	}
	int faceID;
	void AddFaceVertices()
	{
		for (int faceID = 0; faceID < 6; faceID++) {
			VerticeFaceDraggable faceVertice = Instantiate (verticeFaceDraggable);
			List<int> childsIDs = new List<int> ();
			switch (faceID) {
			//TOP
			case 0:
				childsIDs.Add (0);
				childsIDs.Add (1);
				childsIDs.Add (2);
				childsIDs.Add (3);
				faceVertice.SetFace (VerticeFaceDraggable.faces.TOP);
				break;
			//bottom
			case 1:
				childsIDs.Add (4);
				childsIDs.Add (5);
				childsIDs.Add (7);
				childsIDs.Add (6);
				faceVertice.SetFace (VerticeFaceDraggable.faces.BOTTOM);
				break;
			//front
			case 2:
				childsIDs.Add (2);
				childsIDs.Add (3);
				childsIDs.Add (7);
				childsIDs.Add (6);
				faceVertice.SetFace (VerticeFaceDraggable.faces.FRONT);
				break;
			//back
			case 3:
				childsIDs.Add (0);
				childsIDs.Add (1);
				childsIDs.Add (4);
				childsIDs.Add (5);
				faceVertice.SetFace (VerticeFaceDraggable.faces.BACK);
				break;
			//right
			case 4:
				childsIDs.Add (1);
				childsIDs.Add (2);
				childsIDs.Add (5);
				childsIDs.Add (7);
				faceVertice.SetFace (VerticeFaceDraggable.faces.RIGHT);
				break;
			//left
			case 5:
				childsIDs.Add (0);
				childsIDs.Add (3);
				childsIDs.Add (4);
				childsIDs.Add (6);
				faceVertice.SetFace (VerticeFaceDraggable.faces.LEFT);
				break;
			}
			Vector3 newPos = Vector3.zero;
			foreach (int id in childsIDs) {
				faceVertice.childs.Add (verticesDraggables [id]);
				newPos += verticesDraggables [id].transform.localPosition;
			}
			newPos /= childsIDs.Count;
		
			faceVertice.transform.SetParent (transform);
			faceVertice.Init (meshConstructor, verticesDraggables.Count + 1, Vector3.zero);
			faceVertice.SetFaceType ();
			//faceVertice.transform.localScale = verticesSize;
			verticesDraggables.Add (faceVertice);
		}
		RepositionateFaces ();
	}
	void RepositionateFaces()
	{
		foreach (VerticeFaceDraggable vd in verticesDraggables) {
			if (vd.childs.Count > 0) {
				Vector3 newPos = Vector3.zero;
				foreach (VerticeFaceDraggable child in vd.childs) {
					newPos += child.transform.localPosition;
				}
				newPos /=  vd.childs.Count;
				vd.transform.localPosition = newPos;
			}
		}
	}
}
