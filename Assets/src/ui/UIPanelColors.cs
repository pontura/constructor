using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelColors : MonoBehaviour {

	public Color[] colors;
	public UIButtonColor buttonColor;
	public Transform container;
	private UIPanel uiPanel;
	private float sizes = -1.6f;

	void Start()
	{		
		uiPanel = GetComponent<UIPanel> ();
		float _x = 0;
		float _y = 0;

		int cols = 4;

		int id = 0;

		foreach(Color c in colors)
		{
			UIButtonColor b = Instantiate (buttonColor);
			b.Init (this, c, id);
			b.transform.SetParent (container);
			float scale = 0.4f;
			b.transform.localScale = new Vector3 (scale, scale, scale);

			if(id>0)
				_x+=sizes;

			if (_x <= sizes * cols) {
				_x = 0;
				_y+=sizes;
			}
			b.transform.localPosition = new Vector3 (_x, _y, 0);
			b.transform.localEulerAngles = Vector3.zero;
			id++;
		}
	}
	public void PickColor(int id)
	{
		World.Instance.activeColor = colors[id];
		uiPanel.character.ChangeState (Character.states.COLOR_PAINT);
	}
}
