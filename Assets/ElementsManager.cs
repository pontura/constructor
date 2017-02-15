using UnityEngine;
using System.Collections;

public class ElementsManager : MonoBehaviour {

	public Transform world;
    public Element cubeElement;

	private float lastTimeElementCreated;
	private float delay_to_create = 0.3f;

	void Start () {
        Events.OnAddElement += OnAddElement;
		lastTimeElementCreated = Time.time;
	}
	void OnAddElement(Element.types type, Vector3 pos)
    {
		if (!CanCreate ())
			return;
		
        Element newElement = Instantiate(cubeElement);        
		newElement.transform.SetParent (world);
		newElement.transform.localScale = Vector3.one;
		newElement.transform.position = pos;
    }
	bool CanCreate()
	{
		if (lastTimeElementCreated + delay_to_create > Time.time)
			return false;
		lastTimeElementCreated = Time.time;
		return true;
	}

}
