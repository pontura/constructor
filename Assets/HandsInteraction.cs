using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInteraction : MonoBehaviour {

	public Character character;
	public ControllerRight controllerRight;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "UI") {
			character.interaction_with_ui = true;
			controllerRight.OverUI (true);
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "UI") {
			character.interaction_with_ui = false;
			controllerRight.OverUI (false);
		}
	}
}
