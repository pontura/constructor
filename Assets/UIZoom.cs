using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIZoom : MonoBehaviour {

	public enum sizes
	{
		SMALL,
		MEDIUM,
		BIG
	}
	public GameObject zoom1;
	public GameObject zoom2;
	public GameObject zoom3;
	int value = 1;
	int lastSendedValue;

	void Start()
	{
		SetValue ();
		lastSendedValue = value;
	}

	public void SetEvent()
	{
		if (lastSendedValue == value)
			return;
		lastSendedValue = value;
		switch (value) {
		case 1:
			Events.OnResizeWorld (sizes.SMALL);
			break;
		case 2:
			Events.OnResizeWorld (sizes.MEDIUM);
			break;
		case 3:
			Events.OnResizeWorld (sizes.BIG);
			break;
		}
	}
	public void SetNewValue(bool left)
	{
		if (left)
			value--;
		else
			value++;
		if (value < 1)
			value = 3;
		if (value > 3)
			value = 1;
		SetValue ();
	}
	public void SetValue()
	{
		zoom1.SetActive (false);
		zoom2.SetActive (false);
		zoom3.SetActive (false);

		switch (value) {
		case 1:
			zoom1.SetActive (true);
			break;
		case 2:
			zoom2.SetActive (true);
			break;
		case 3:
			zoom3.SetActive (true);
			break;
		}
	}
}
