using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

	public buttons button;
	public GameObject panel;

	public enum buttons
	{		
		EDITING,
		CUBE_CONSTRUCTOR,
		FREE_DRAWING,
		TELEPORT,
		SCALE_X1,
		SCALE_X2,
		SCALE_X3,
		GRAVITY_YES,
		GRAVITY_NO,
		SNAPPING_ON,
		SNAPPING_OFF
	}
	public Character character;
	void Start()
	{
		ClickSimpleButton (buttons.CUBE_CONSTRUCTOR);
		Events.ShowUI += ShowUI;
		Events.OnChangeCharacterState += OnChangeCharacterState;
	}
	void ShowUI(bool isActive)
	{
		panel.SetActive (isActive);
		character.interaction_with_ui = isActive;
	}
	void OnChangeCharacterState(Character.states state)
	{
		switch (state) {
		case Character.states.EDITING:
			Events.OnUIButtonActivate(buttons.EDITING);
			break;
		}
	}

	public void ClickSimpleButton(buttons button)
	{
		switch (button) {
		case buttons.EDITING:
			character.ChangeState (Character.states.EDITING);
			break;
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
		case buttons.GRAVITY_YES:			
			World.Instance.useGravity = true;
			Events.OnRecalculateGravity ();
			break;
		case buttons.GRAVITY_NO:
			World.Instance.useGravity = false;
			break;
		case buttons.SNAPPING_ON:			
			World.Instance.useSnapping = true;
			break;
		case buttons.SNAPPING_OFF:
			World.Instance.useSnapping = false;
			break;
		}
	}
}
