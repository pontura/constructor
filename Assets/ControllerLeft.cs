using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerLeft : MonoBehaviour
{
	public Character character;
	public HandController hand;
	SteamVR_TrackedObject trackedObj;
	public UIZoom uiZoom;
	public UIPanel uiPanel;

	void Awake()
	{		
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}
	void Update()
	{		
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (character.interaction_with_ui == true) {
			return;
		} 
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			hand.Grab (HandController.types.LEFT);
			Events.OnTriggerLeftDown ();
		} else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			hand.Idle (HandController.types.LEFT);
			Events.OnTriggerLeftUp ();
			Events.ChangeConstructionState (HandConstructor.states.INACTIVE);
		}
	}		

	private readonly Vector2 mXAxis = new Vector2(1, 0);
	private readonly Vector2 mYAxis = new Vector2(0, 1);
	private bool trackingSwipe = false;
	private bool checkSwipe = false;

	private readonly string [] mMessage = {
		"",
		"Swipe Left",
		"Swipe Right",
		"Swipe Top",
		"Swipe Bottom"
	};

	public int mMessageIndex = 0;
	private const float mAngleRange = 30;
	private const float mMinSwipeDist = 0.2f;
	private const float mMinVelocity  = 4.0f;
	private Vector2 mStartPosition;
	private Vector2 endPosition;
	private float mSwipeStartTime;

	void DoSwipe() {
		
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		// Touch down, possible chance for a swipe
		if ((int)trackedObj.index != -1 && device.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_Axis0)) {
			trackingSwipe = true;
			// Record start time and position
			mStartPosition = new Vector2 (device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).x,
				device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).y);
			mSwipeStartTime = Time.time;
		}
		// Touch up , possible chance for a swipe
		else if (device.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_Axis0)) {
			trackingSwipe = false;
			trackingSwipe = true;
			checkSwipe = true;
			//Debug.Log ("Tracking Finish");
		}
		else if(trackingSwipe)
		{
			endPosition= new Vector2 (device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).x,
				device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).y);
		}

		if (checkSwipe) {
			checkSwipe = false;

			float deltaTime = Time.time - mSwipeStartTime;
			Vector2 swipeVector = endPosition - mStartPosition;

			float velocity = swipeVector.magnitude/deltaTime;
			if (velocity > mMinVelocity &&
				swipeVector.magnitude > mMinSwipeDist) {
				// if the swipe has enough velocity and enough distance


				swipeVector.Normalize();

				float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
				angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

				// Detect left and right swipe
				if (angleOfSwipe < mAngleRange) {
					OnSwipeRight();
				} else if ((180.0f - angleOfSwipe) < mAngleRange) {
					OnSwipeLeft();
				} else {
					// Detect top and bottom swipe
					angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
					angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
					if (angleOfSwipe < mAngleRange) {
						OnSwipeTop();
					} else if ((180.0f - angleOfSwipe) < mAngleRange) {
						OnSwipeBottom();
					} else {
						mMessageIndex = 0;
					}
				}
			}
		}

	}

	private void OnSwipeLeft() {
		//Debug.Log ("Swipe Left");
		mMessageIndex = 1;
		uiZoom.SetNewValue(true);
	}

	private void OnSwipeRight() {
		//Debug.Log ("Swipe right");
		mMessageIndex = 2;
		uiZoom.SetNewValue(false);
	}

	private void OnSwipeTop() {
		//Debug.Log ("Swipe Top");
		mMessageIndex = 3;
	}

	private void OnSwipeBottom() {
		//Debug.Log ("Swipe Bottom");
		mMessageIndex = 4;
	}



}