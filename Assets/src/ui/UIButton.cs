﻿namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class UIButton : VRTK_InteractableObject {

		public bool isOver;
		public GameObject activeBG;
		public bool initSelected;

		public virtual void Start()
		{
			if(initSelected)
				SetButtonActive (true);
		}
		void OnEnable()
		{
			Events.OnStartUse_UIObject += OnStartUse_UIObject;
			Events.OnStopUse_UIObject += OnStopUse_UIObject;
			Events.OnTriggerOverUI += OnTriggerOverUI;
			Events.OnUIButtonSelected += OnUIButtonSelected;
			Events.OnUIButtonActivate += OnUIButtonActivate;
			Events.OnUIButtonInactivate += OnUIButtonInactivate;

		}
		void OnDisable()
		{
			Events.OnStartUse_UIObject -= OnStartUse_UIObject;
			Events.OnStopUse_UIObject -= OnStopUse_UIObject;
			Events.OnTriggerOverUI -= OnTriggerOverUI;
			Events.OnUIButtonSelected -= OnUIButtonSelected;
			Events.OnUIButtonActivate -= OnUIButtonActivate;
			Events.OnUIButtonInactivate -= OnUIButtonInactivate;
		}

		public virtual void OnUIButtonActivate(UIPanel.buttons b) { }
		public virtual void OnUIButtonInactivate(UIPanel.buttons b) { }
		public virtual void OnUIButtonSelected(UIButton uiButton) { }

		public virtual void OnTriggerOverUI()
		{
			if (isOver) {
				SetButtonActive (true);
				Events.OnUIButtonSelected (this);
			}
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
