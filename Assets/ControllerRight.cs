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
		Invoke("Delayed", 0.5f);
		bezierPointer = GetComponent<VRTK_BezierPointer> ();
		simplePointer = GetComponent<SimplePointer_Draw> ();
	}
	void Delayed()
	{
		hand.Pointer (HandController.types.LEFT);
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}

	void FixedUpdate()
	{		
		ResetAll ();
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (character.interaction_with_ui == true) {			
			simplePointer.enabled = true;
			bezierPointer.enabled = false;
			return;
		} 
		if (character.state == Character.states.TELEPORT) {
			bezierPointer.enabled = true;
		} else {
			bezierPointer.enabled = false;
		}
		if (character.state == Character.states.FREE_DRAWING) {
			simplePointer.enabled = true;
		} else {
			simplePointer.enabled = false;
		}

		if (character.state == Character.states.TELEPORT) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				heightAdjustTeleport.ignoreTargetWithTagOrClass = "None";
			} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				heightAdjustTeleport.ignoreTargetWithTagOrClass = "Element";
			}
		} else if (character.state == Character.states.FREE_DRAWING) {	
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerLeftDown ();
			} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnTriggerLeftDown ();
				Events.OnTriggerLeftUp ();
				Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
			}
		} else if (character.state == Character.states.CUBE_CONSTRUCTOR) {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Events.OnAddElement (Element.types.CUBE, transform.position);				
			}
		}


		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			Events.OnTriggerLeftDown();
		} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			Events.OnTriggerLeftUp();
			Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
		}






		///} else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
		//uiZoom.SetEvent ();
		//} 

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {

		} 
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			//heightAdjustTeleport.ignoreTargetWithTagOrClass = "None";
		} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			//	heightAdjustTeleport.ignoreTargetWithTagOrClass = "Element";
		}
	}
	void ResetAll()
	{
		//bezierPointer.enabled = false;
		//simplePointer.enabled = false;
	}
}