using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CharacterPivot : MonoBehaviour {
	
	public Vector3 pos = Vector3.zero;

	void Start () {
		Events.OnTeleportTo += OnTeleportTo;
	}
	void OnTeleportTo(Vector3 pos)
	{
		this.pos = pos;
		transform.position = pos;
	}

}
