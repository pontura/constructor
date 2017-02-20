namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class UIButton : VRTK_InteractableObject {

		protected override void Start()
		{
			base.Start();
			//Events.OnStartUse += OnStartUse;
		}
		void OnStartUse(Transform t)
		{
			if (t == transform) {
				print ("_________");
			}
		}
		public virtual void StartTouching(GameObject currentTouchingObject)
		{
			print ("_______OVER: " + gameObject.name);
			base.StartTouching(currentTouchingObject);

		}
		public virtual void StartUsing(GameObject currentUsingObject)
		{
			print ("_______OUT " + gameObject.name);
			base.StartUsing(currentUsingObject);

		}
	}
}
