using UnityEngine;
using System.Collections;

public class ElementsManager : MonoBehaviour {

    public Element cubeElement;

	void Start () {
        Events.OnAddElement += OnAddElement;
	}
	void OnAddElement(Element.types type, Vector3 pos)
    {
        Element newElement = Instantiate(cubeElement);
        newElement.transform.localPosition = pos;
    }

}
