using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class UIButtonColor : UIButtonSimple {

	private UIPanelColors uiPanelColors;
	private Color color;
	private int id;
	public SpriteRenderer assetToPaint;

	public void Init(UIPanelColors _uiPanelColors, Color _color, int _id)
	{
		this.uiPanelColors = _uiPanelColors;
		this.color = _color;
		this.id = _id;
		assetToPaint.color = _color;
	}

	public override void OnTriggerOverUI()
	{			
		if (isOver) { 
			uiPanelColors.PickColor (id);
		}
		base.OnTriggerOverUI ();
	}
}
