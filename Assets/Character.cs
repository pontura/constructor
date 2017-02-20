using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public states state;
	public enum states
	{
		CUBE_CONSTRUCTOR,
		FREE_DRAWING,
		TELEPORT
	}
	public bool interaction_with_ui;


}
