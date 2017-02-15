﻿using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action ResetApp = delegate { };
    
    public static System.Action<string> GotoTo = delegate { };
    public static System.Action<string> GotoBackTo = delegate { };
    public static System.Action Back = delegate { };

    public static System.Action<Element.types, Vector3> OnAddElement = delegate { };

	public static System.Action<HandConstructor.states> ChangeConstructionState = delegate { }; 
	public static System.Action StopCarrying = delegate { };
	public static System.Action StartCarrying = delegate { };
	public static System.Action DraggClkicked = delegate { };
	public static System.Action DraggReleased = delegate { };

	public static System.Action OnTriggerLeftDown = delegate { };
	public static System.Action OnTriggerLeftUp = delegate { };
	public static System.Action<GameObject, bool> OnChangeLeftInteractiveState = delegate { };
	public static System.Action OnActivateElements = delegate { };


}
   