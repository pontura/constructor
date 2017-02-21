using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

	public buttons button;
	public GameObject panel;

	public enum buttons
	{		
		CUBE_CONSTRUCTOR,
		FREE_DRAWING,
		TELEPORT,
		SCALE_X1,
		SCALE_X2,
		SCALE_X3
	}
	public Character character;
	void Start()
	{
		ClickSimpleButton (buttons.CUBE_CONSTRUCTOR);
		Events.ShowUI += ShowUI;
	}
	void ShowUI(bool isActive)
	{
		panel.SetActive (isActive);
	}
	public void ClickSimpleButton(buttons button)
	{
		switch (button) {
		case buttons.CUBE_CONSTRUCTOR:
			character.ChangeState (Character.states.CUBE_CONSTRUCTOR);
			break;
		case buttons.FREE_DRAWING:
			character.ChangeState (Character.states.FREE_DRAWING);
			break;
		case buttons.TELEPORT:
			character.ChangeState (Character.states.TELEPORT);
			break;
		case buttons.SCALE_X1:
			Events.OnResizeWorld (UIZoom.sizes.SMALL);
			break;
		case buttons.SCALE_X2:
			Events.OnResizeWorld (UIZoom.sizes.MEDIUM);
			break;
		case buttons.SCALE_X3:
			Events.OnResizeWorld (UIZoom.sizes.BIG);
			break;
		}
	}
}
