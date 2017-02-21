namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class UIButton : VRTK_InteractableObject {

		public bool isOver;
		public GameObject activeBG;

		void Start()
		{
			Events.OnStartUse_UIObject += OnStartUse_UIObject;
			Events.OnStopUse_UIObject += OnStopUse_UIObject;
			Events.OnTriggerOverUI += OnTriggerOverUI;
		}
		public virtual void OnTriggerOverUI()
		{
			if (isOver)  
				SetButtonActive (true);
			 else
				SetButtonActive (false);
		}
		void OnStartUse_UIObject(Transform t)
		{
			if (t == transform)
				isOver = true;
		}
		void OnStopUse_UIObject(Transform t)
		{
			if (t == transform)
				isOver = false;
		}
		public void SetButtonActive (bool isActive)
		{
			activeBG.SetActive (isActive);
			isOver = false;
		}
	}
}
