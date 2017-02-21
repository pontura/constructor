using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSnapping : MonoBehaviour {

	Element element;

	void Start()
	{
		element = GetComponent<Element> ();
	}
	public void Init()
	{
		if (!World.Instance.useSnapping)
			return;
		element.StartBeingSnapped ();
		UpdatePosition (transform.localPosition);
		UpdateEulerAngles (transform.eulerAngles);
	}
	public void UpdatePosition(Vector3 pos)
	{		
		Vector3 newPos = new Vector3 (
			ToDecimals (pos.x),
			ToDecimals (pos.y),
			ToDecimals (pos.z));

		transform.localPosition = newPos;
	}
	float ToDecimals(float num)
	{
		float multiplier = World.Instance.zoomMultiplier;

		int num_to_multiply = (int)(10 * multiplier);
		if (num_to_multiply < 1)
			num_to_multiply = 1;
		float n = Mathf.Round(num *num_to_multiply) /num_to_multiply;

		//print (multiplier + " _ num_to_multiply _ " + num_to_multiply);

		return n;

	}
	public void UpdateEulerAngles(Vector3 rot)
	{
		Vector3 newRot = new Vector3 (
			To90Degrees (rot.x),
			To90Degrees (rot.y),
			To90Degrees (rot.z));

		transform.eulerAngles = newRot;
	}
	float To90Degrees(float degree)
	{	
		if (degree < 0)
			degree = 360 + degree;
		float newFloat = Mathf.Round((degree / 90));
		int newDegree = (int)(newFloat);
		return newDegree * 90;
	}
}
