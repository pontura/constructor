﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticeFaceDraggable : VerticeDraggable {

	public GameObject assetShape;
	public faces face;

	public enum faces
	{
		TOP,
		BOTTOM,
		RIGHT,
		LEFT,
		BACK,
		FRONT
	}
	void Start()
	{
		Events.OnResizeWorldMultiplier += OnResizeWorldMultiplier;
	}
	void OnResizeWorldMultiplier(float multiplier)
	{
		assetShape.transform.localScale *= multiplier;
	}
	public override void ChangeMaterials(Material mat)
	{
		return;
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			mr.material = mat;
	}
	public void SetFace(faces _face)
	{
		this.face = _face;
		SetAsset ();
	}
	void SetAsset()
	{
		Vector3 rot = Vector3.zero;
		switch (face) {
		case faces.BOTTOM:
			rot = new Vector3 (180, 0, 0);
			break;
		case faces.RIGHT:
			rot = new Vector3 (0, 0, -90);
			break;
		case faces.LEFT:
			rot = new Vector3 (0, 0, 90);
			break;
		case faces.BACK:
			rot = new Vector3 (90, 0, 0);
			break;
		case faces.FRONT:
			rot = new Vector3 (-90, 0, 0);
			break;
		}
		assetShape.transform.localEulerAngles = rot;
	}
	public override void NewPos(Vector3 updaterVector)
	{
		FixedPositionByFace (updaterVector, face);
		foreach (VerticeDraggable vd in childs) {
			vd.FixedPositionByFace (updaterVector, face);
		}
	}
}
