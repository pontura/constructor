using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChilds : MonoBehaviour {

	public List<Element> childs;
	Element element;
	void Start()
	{
		Events.StopCarrying += StopCarrying;
		element = GetComponent<Element> ();
	}
	void OnDestroy()
	{
		Events.StopCarrying -= StopCarrying;
	}
	void StopCarrying(Element e)
	{
		if (element == e) {
			ReactivateChilds ();
			Empty ();
		} else 
		{
			bool exists = false;
			foreach (Element el in childs)
				if (e == el)
					exists = true;
			if(exists)
				childs.Remove(e);
		}
	}
	private Element movedBy;
	public void ParentHasBeenMovedBy(Element _element) 
	{
		movedBy = _element;
		ReactivateChilds ();
		element.Repositionate ();
	}
	public void ReactivateChilds() 
	{
		foreach (Element e in childs) {
			if (e != movedBy)
				e.childs.ParentHasBeenMovedBy (element);
		}
		movedBy = null;
	}
	void OnCollisionEnter(Collision col)
	{
		Element element = col.gameObject.GetComponent<Element> ();

		if (element == null)
			return;	

		if (CheckIfIsChild (element))
			return;

		if(element.transform.position.y>transform.position.y)
			childs.Add (element);
	}
	public void Empty()
	{
		childs.Clear ();
	}
	void _______OnCollisionExit(Collision col)
	{
		Element element = col.gameObject.GetComponent<Element> ();

		if (element == null)
			return;	

		if (CheckIfIsChild (element))
			if(element.transform.position.y>transform.position.y)
				childs.Remove(element);
	}
	bool CheckIfIsChild(Element element)
	{
		foreach (Element e in childs)
			if (element == e) {
				return true;
			}
		return false;
	}
}
