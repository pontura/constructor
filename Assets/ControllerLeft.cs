using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerLeft : MonoBehaviour
{

	SteamVR_TrackedObject trackedObj;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}

	void FixedUpdate()
	{		
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Events.OnTriggerLeftDown();
		} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Events.OnTriggerLeftUp();
			Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
		}
	}
	/// <summary>
	/// /////////////////////
	/// </summary>
	void ____________FixedUpdate()
	{		
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Grip)) {
			Events.StartCarrying ();
		} else
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Grip)) {
			Events.StopCarrying ();
		}
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Events.DraggClkicked();
		} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Events.DraggReleased();
			Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
		}
	}
	/// <summary>
	/// /////////////////////
	/// </summary>
}