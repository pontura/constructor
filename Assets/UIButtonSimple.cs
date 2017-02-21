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
			SNAPPING
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
