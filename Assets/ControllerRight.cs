using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRight : MonoBehaviour {

	//public WorldManager worldManager;
	//public GameObject prefab;

	SteamVR_TrackedObject trackedObj;
	public VRTK.VRTK_HeightAdjustTeleport heightAdjustTeleport;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}

	void FixedUpdate()
	{		
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Events.OnAddElement (Element.types.CUBE, transform.position);
		} 
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			heightAdjustTeleport.ignoreTargetWithTagOrClass = "None";
		}else
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			heightAdjustTeleport.ignoreTargetWithTagOrClass = "Element";
		}
	}
}