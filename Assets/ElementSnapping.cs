using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSnapping : MonoBehaviour {

	Element element;
	private int Degrees_To_Snap = 7;

	void Start()
	{
		element = GetComponent<Element> ();
	}
	public void SnapTo(Element otherElement, VerticeDraggable v1, VerticeDraggable otherv2)
	{
		return;
		//float diffAngles = Mathf.Abs (Mathf.DeltaAngle(otherElement.transform.eulerAngles.y, transform.eulerAngles.y));
	//	if (diffAngles<Degrees_To_Snap) {
			element.Snapped ();
			SetOrientation (otherElement, v1.element);
			Vector3 NewPosition = v1.transform.position - otherv2.transform.position;
			element.transform.position -= NewPosition;
		//}
	}
	void SetOrientation(Element master, Element slave)
	{
		GetMostSimilarDirection(master.transform, slave.transform);
		slave.transform.forward = newDir;
	}
	Vector3 newDir = Vector3.zero;
	void GetMostSimilarDirection(Transform master, Transform slave)
	{
		float newDirDistance = 0;

		if (DiferenceBetwwenAngles (master.forward, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (master.forward, slave.forward);
			newDir = master.forward;
		}
		if (DiferenceBetwwenAngles (master.up, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (master.up, slave.forward);
			newDir = master.up;
		}
		if (DiferenceBetwwenAngles (master.right, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (master.right, slave.forward);
			newDir = master.right;
		}
		if (DiferenceBetwwenAngles (-master.forward, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (-master.forward, slave.forward);
			newDir = -master.forward;
		}
		if (DiferenceBetwwenAngles (-master.up, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (-master.up, slave.forward);
			newDir = -master.up;
		}
		if (DiferenceBetwwenAngles (-master.right, slave.forward) < newDirDistance) {
			newDirDistance = DiferenceBetwwenAngles (-master.right, slave.forward);
			newDir = -master.right;
		}
	}
	private float DiferenceBetwwenAngles(Vector2 vec1, Vector2 vec2)
	{
		return Mathf.Abs( Vector3.Distance (vec1, vec2) );
	}
}
