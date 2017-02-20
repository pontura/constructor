using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {
	
	public types type;
	public enum types
	{
		LEFT,
		RIGHT
	}
	public states state;
	public enum states
	{
		IDLE,
		POINTER,
		GRAB
	}
	Animator anim;
	void Start () {
		HandEvents.Idle += Idle;
		HandEvents.Pointer += Pointer;
		HandEvents.Grab += Grab;
		anim = GetComponent<Animator> ();
	}
	public void Idle(types _type)
	{	
		if (!isThisHand(_type) || state == states.IDLE)
			return;
		state = states.IDLE;
		anim.Play ("idle");
	}
	public void Pointer(types _type)
	{
		if (!isThisHand(_type) || state == states.POINTER)
			return;
		state = states.POINTER;
		anim.Play ("pointer");
	}
	public void Grab(types _type)
	{
		if (!isThisHand(_type) || state == states.GRAB)
			return;
		state = states.GRAB;
		anim.Play ("grab");
	}
	bool isThisHand(types _type)
	{
		if (this.type == _type)
			return true;
		return false;
	}
}
