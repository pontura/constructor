using UnityEngine;
using System.Collections;
using VRTK.Examples;

public static class Events
{
    public static System.Action ResetApp = delegate { };

	public static System.Action<UIZoom.sizes> OnResizeWorld = delegate { };
	public static System.Action<float> OnResizeWorldMultiplier = delegate { };
    public static System.Action<string> GotoTo = delegate { };
    public static System.Action<string> GotoBackTo = delegate { };
    public static System.Action Back = delegate { };

    public static System.Action<Element.types, Vector3> OnAddElement = delegate { };

	public static System.Action<HandConstructor.states> ChangeConstructionState = delegate { }; 
	public static System.Action<Element> StopCarrying = delegate { };
	public static System.Action StartCarrying = delegate { };
	public static System.Action DraggClkicked = delegate { };
	public static System.Action DraggReleased = delegate { };

	public static System.Action OnTriggerLeftDown = delegate { };
	public static System.Action OnTriggerLeftUp = delegate { };
	public static System.Action OnTriggerRightDown = delegate { };
	public static System.Action OnTriggerRightUp = delegate { };

	public static System.Action OnRecalculateGravity = delegate { };

	public static System.Action<GameObject, bool> OnChangeLeftInteractiveState = delegate { };
	public static System.Action<Character.states> OnChangeCharacterState = delegate { };

	public static System.Action<Vector3> OnTeleportTo = delegate { };

	public static System.Action<bool> ShowUI = delegate { };
	public static System.Action<Transform> OnStartUse_UIObject = delegate { };
	public static System.Action<Transform> OnStopUse_UIObject = delegate { };
	public static System.Action OnTriggerOverUI = delegate { };
	public static System.Action<VRTK.Examples.UIButton> OnUIButtonSelected = delegate { };
	public static System.Action<UIPanel.buttons> OnUIButtonActivate = delegate { };
	public static System.Action<UIPanel.buttons> OnUIButtonInactivate = delegate { };


}
   
