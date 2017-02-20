using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SimplePointer_Draw : VRTK_SimplePointer {

	public Character character;
	public DrawManager drawManager;
	private VRTK_ControllerEvents events;

	protected virtual void OnEnable()
	{
		base.OnEnable();
		events = GetComponent<VRTK_ControllerEvents> ();
		events.AliasPointerOn += AliasPointerOn;
		events.AliasPointerOff += AliasPointerOff;
	}
	protected virtual void OnDisable()
	{
		base.OnDisable();
		events.AliasPointerOn -= AliasPointerOn;
		events.AliasPointerOff -= AliasPointerOff;
	}
	void AliasPointerOn(object o, ControllerInteractionEventArgs args)
	{
		if(character.state == Character.states.FREE_DRAWING && !character.interaction_with_ui)
			drawManager.Init();
	}
	void AliasPointerOff(object o, ControllerInteractionEventArgs args)
	{
		if(character.state == Character.states.FREE_DRAWING && !character.interaction_with_ui)
			drawManager.End();
	}
	public override void SetPointerPosition(Vector3 destination)
	{		
		base.SetPointerPosition (destination);
		if(character.state == Character.states.FREE_DRAWING && !character.interaction_with_ui)
			drawManager.SetPosition(destination);
	}
}