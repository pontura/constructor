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
			GRAVITY,
			SNAPPING,
			UI,
			COLORS
		}
		public override void OnUIButtonActivate(UIPanel.buttons b) 
		{
			if(b == UIPanel.buttons.EDITING)
			{
				if (b == button)
					SetButtonActive (true);
				else if(type == types.EDITION)
					SetButtonActive (false);
			}
		}
		public override void OnUIButtonInactivate(UIPanel.buttons b) 
		{
			if(b == button)
			{
				SetButtonActive (false);
			}
		}

		public override void OnUIButtonSelected(UIButton uiButton)
		{
			if (uiButton.GetComponent<UIButtonSimple> ().type == types.UI)
				return;
			if (uiButton.GetComponent<UIButtonSimple> ().type == type) {
				if (uiButton == this) 
					SetButtonActive (true);
				else
					SetButtonActive (false);
			}
		}
		public override void OnTriggerOverUI()
		{			
			if (isOver && uiPanel != null) { 
				uiPanel.ClickSimpleButton (button);
			}
			base.OnTriggerOverUI ();
		}
	}
}
