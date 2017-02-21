namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class UIButtonSimple : UIButton {

		public UIPanel uiPanel;
		public UIPanel.buttons button;

		public override void OnTriggerOverUI()
		{			
			if (isOver) { 
				uiPanel.ClickSimpleButton (button);
			}
			base.OnTriggerOverUI ();
		}
	}
}
