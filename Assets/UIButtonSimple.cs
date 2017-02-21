namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class UIButtonSimple : UIButton {

		public UIPanel uiPanel;
		public UIPanel.buttons button;

		public types type;
		public enum types
		{
			EDITION,
			ZOOM,
			COLORS
		}
		public override void OnUIButtonSelected(UIButton uiButton)
		{
			if (uiButton.GetComponent<UIButtonSimple> ().type == type) {
				if (uiButton == this) 
					SetButtonActive (true);
				else
					SetButtonActive (false);
			}
		}
		public override void OnTriggerOverUI()
		{			
			if (isOver) { 
				uiPanel.ClickSimpleButton (button);
			}
			base.OnTriggerOverUI ();
		}
	}
}
