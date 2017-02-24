using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public UIZoom.sizes size;
	public GameObject world;
	public float zoomMultiplier;
	static World mInstance = null;
	public GameObject cameraRig;
	public bool useGravity;
	public bool useSnapping;
	public Color activeColor = Color.red;
	public Character character;

	public static World Instance
	{
		get
		{
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;
	}
	void Start()
	{
		Events.OnResizeWorld += OnResizeWorld;
		OnResizeWorld (UIZoom.sizes.MEDIUM);
	}
	void OnResizeWorld(UIZoom.sizes _size)
	{
		float newZoom = 1;
		if (size == UIZoom.sizes.SMALL && _size == UIZoom.sizes.MEDIUM || size == UIZoom.sizes.MEDIUM && _size == UIZoom.sizes.BIG)
			newZoom = 10;
		else if (size == UIZoom.sizes.MEDIUM && _size == UIZoom.sizes.SMALL || size == UIZoom.sizes.BIG && _size == UIZoom.sizes.MEDIUM)
			newZoom = 0.1f;
		else if (size == UIZoom.sizes.SMALL && _size == UIZoom.sizes.BIG)
			newZoom = 100;
		else if (size == UIZoom.sizes.BIG && _size == UIZoom.sizes.SMALL)
			newZoom = 0.01f;



		Events.OnResizeWorldMultiplier (newZoom);

		cameraRig.transform.position /= newZoom;

		this.size = _size;

		float zoomValue= 1;
		switch (size) {
		case UIZoom.sizes.SMALL:
			zoomValue = 1;
			break;
		case UIZoom.sizes.MEDIUM:
			zoomValue = 0.1f;
			break;
		case UIZoom.sizes.BIG:
			zoomValue = 0.01f;
			break;
		}
		this.zoomMultiplier = zoomValue;
		world.transform.localScale = new Vector3(zoomValue,zoomValue,zoomValue);
	}
}
