using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerRight : MonoBehaviour {

	public HandController hand;
	public Character character;

	//public WorldManager worldManager;
	//public GameObject prefab;
	[HideInInspector]
	public VRTK_BezierPointer bezierPointer;
	[HideInInspector]
	public SimplePointer_Draw simplePointer;

	SteamVR_TrackedObject trackedObj;
	public VRTK.VRTK_HeightAdjustTeleport heightAdjustTeleport;

	void Start()
	{		
		bezierPointer = GetComponent<VRTK_BezierPointer> ();
		simplePointer = GetComponent<SimplePointer_Draw> ();

		Events.ShowUI += OverUI;
	}
	public void OverUI(bool isOver)
	{
		if (isOver) {
			simplePointer.enabled = true;
			bezierPointer.enabled = false;
			hand.Pointer (HandController.types.RIGHT);
		} else {
			OnActivateAction (character.state);
		}
	}
	void OnActivateAction(Character.states state)
	{
		switch (state) {
		case Character.states.CUBE_CONSTRUCTOR:
		case Character.states.EDITING:
		case Character.states.EDITING_FREE:
		case Character.states.COLOR_PAINT:
		case Character.states.DESTROY:
			hand.Idle (HandController.types.RIGHT);
			bezierPointer.enabled = false;
			simplePointer.enabled = false;
			break;
		case Character.states.FREE_DRAWING:
			hand.Pointer (HandController.types.RIGHT);
			bezierPointer.enabled = false;
			simplePointer.enabled = true;
			break;
		case Character.states.TELEPORT:
			hand.Pointer (HandController.types.RIGHT);
			simplePointer.enabled = false;
			bezierPointer.enabled = true;
			break;
		}
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}

	void Update()
	{		
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (character.interaction_with_ui == true) {	
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerOverUI();
			}
			return;
		} 

		if (character.state == Character.states.TELEPORT) {
			bezierPointer.enabled = true;
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				heightAdjustTeleport.ignoreTargetWithTagOrClass = "None";
			} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				heightAdjustTeleport.ignoreTargetWithTagOrClass = "Element";
			}
		} else if (character.state == Character.states.FREE_DRAWING) {	
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightDown ();
			} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightUp ();
				Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
				character.ChangeState (Character.states.EDITING);
			}
		} else if (character.state == Character.states.CUBE_CONSTRUCTOR) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnAddElement (Element.types.CUBE, transform.position);	
				character.ChangeState (Character.states.EDITING);
			}
		} else if (character.state == Character.states.COLOR_PAINT) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightDown ();
			}
		} else if (character.state == Character.states.DESTROY) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightDown ();
			}
		}
		else if (character.state == Character.states.EDITING || character.state == Character.states.EDITING_FREE) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightDown ();
				hand.Grab (HandController.types.RIGHT);
			} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerRightUp ();
				hand.Idle (HandController.types.RIGHT);
			}
		}
	}
}