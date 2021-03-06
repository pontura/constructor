﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public states state;

	public enum states
	{
		EDITING,
		CUBE_CONSTRUCTOR,
		FREE_DRAWING,
		TELEPORT,
		COLOR_PAINT,
		DESTROY,
		EDITING_FREE
	}
	public bool interaction_with_ui;

	void Start()
	{
		Invoke("Delayed", 0.5f);
	}
	void Delayed()
	{
		ChangeState (states.CUBE_CONSTRUCTOR);
	}
	public void ChangeState(states _state)
	{
		this.state = _state;
		Events.OnChangeCharacterState (state);
	}
}
