using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {

	public GameObject edit;
	public GameObject editFree;
	public GameObject free;
	public GameObject paint;
	public GameObject cube;
	public GameObject teleport;
	public GameObject destroy;

	void Start () {
		Events.OnChangeCharacterState += OnChangeCharacterState;
	}
	void OnDestroy () {
		Events.OnChangeCharacterState -= OnChangeCharacterState;
	}

	void OnChangeCharacterState (Character.states state) {
		edit.SetActive (false);
		editFree.SetActive (false);
		free.SetActive (false);
		paint.SetActive (false);
		cube.SetActive (false);
		teleport.SetActive (false);
		destroy.SetActive (false);
		
		switch (state) {
		case Character.states.EDITING:
			edit.SetActive (true);
			break;
		case Character.states.EDITING_FREE:
			editFree.SetActive (true);
			break;
		case Character.states.FREE_DRAWING:
			free.SetActive (true);
			break;
		case Character.states.COLOR_PAINT:
			paint.SetActive (true);
			break;
		case Character.states.CUBE_CONSTRUCTOR:
			cube.SetActive (true);
			break;
		case Character.states.TELEPORT:
			teleport.SetActive (true);
			break;
		case Character.states.DESTROY:
			destroy.SetActive (true);
			break;

		}
	}
}
