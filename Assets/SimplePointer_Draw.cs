using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SimplePointer_Draw : VRTK_SimplePointer {

	public DrawManager drawManager;
	private VRTK_ControllerEvents events;

	protected virtual void OnEnable()
	{
		base.OnEnable();
		events = GetComponent<VRTK_ControllerEvents> ();
		events.AliasPointerOn += AliasPointerOn;
		events.AliasPointerOff += AliasPointerOff;
	}
	void AliasPointerOn(object o, ControllerInteractionEventArgs args)
	{
		drawManager.Init();
	}
	void AliasPointerOff(object o, ControllerInteractionEventArgs args)
	{
		drawManager.End();
	}
	public override void SetPointerPosition(Vector3 destination)
	{		
		drawManager.SetPosition(destination);
	}
}