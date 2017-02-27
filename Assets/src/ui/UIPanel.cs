using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

	public buttons button;
	public GameObject panel;

	public GameObject uiPanelColors;
	public GameObject uiPanelInit;
	public GameObject uiPanelSettings;

	public VRTK.Examples.UIButtonSimple colorButton;

	public panels panelActive;
	public enum panels
	{
		INIT,
		COLORS,
		SETTINGS
	}

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
		SNAPPING_OFF,
		COLORS_ON,
		COLORS_OFF,
		COLOR_PICK,
		SETTINGS_ON,
		SETTINGS_OFF,
		DESTROY,
		EDITING_FREE,
	}
	public Character character;
	void Start()
	{
		ClickSimpleButton (buttons.CUBE_CONSTRUCTOR);
		Events.ShowUI += ShowUI;
		Events.OnChangeCharacterState += OnChangeCharacterState;
		SetActivePanel ();
		ShowUI (false);
	}
	void ShowUI(bool isActive)
	{
		panel.SetActive (isActive);

		if(isActive)
			SetActivePanel ();
		
		character.interaction_with_ui = isActive;
	}
	void OnChangeCharacterState(Character.states state)
	{
		switch (state) {
		case Character.states.EDITING:
			Events.OnUIButtonActivate(buttons.EDITING);
			break;
		case Character.states.EDITING_FREE:
			Events.OnUIButtonActivate(buttons.EDITING_FREE);
			break;
		}
	}

	public void ClickSimpleButton(buttons button)
	{
		switch (button) {
		case buttons.EDITING:
			character.ChangeState (Character.states.EDITING);
			break;
		case buttons.EDITING_FREE:
			character.ChangeState (Character.states.EDITING_FREE);
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
		case buttons.COLORS_ON:
			character.ChangeState (Character.states.COLOR_PAINT);
			panelActive = panels.COLORS;
			SetActivePanel ();
			break;
		case buttons.COLORS_OFF:
			panelActive = panels.INIT;
			SetActivePanel ();
			break;
		case buttons.SETTINGS_ON:
			panelActive = panels.SETTINGS;
			SetActivePanel ();
			break;
		case buttons.SETTINGS_OFF:
			panelActive = panels.INIT;
			SetActivePanel ();
			break;
		case buttons.DESTROY:
			character.ChangeState (Character.states.DESTROY);
			break;
		}
	}
	void SetActivePanel()
	{
		return;
		switch (panelActive) {
		case panels.COLORS:
			uiPanelColors.SetActive (true);
			uiPanelInit.SetActive (false);
			uiPanelSettings.SetActive (false);
			break;
		case panels.INIT:
			uiPanelColors.SetActive (false);
			uiPanelInit.SetActive (true);
			uiPanelSettings.SetActive (false);
			if (character.state == Character.states.COLOR_PAINT) {				
				Events.OnUIButtonInactivate (buttons.EDITING);
				colorButton.SetButtonActive (true);
			}
			break;
		case panels.SETTINGS:
			uiPanelColors.SetActive (false);
			uiPanelInit.SetActive (false);
			uiPanelSettings.SetActive (true);
			break;
		}
	}
}
