using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public int _v;
    public int _h;
    public UnityEngine.EventSystems.EventSystem _eventSystem;

    void Update()
    {
        if (_eventSystem.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        { 
            RaycastHit mouseHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out mouseHit, 100))
            {
                Events.OnAddElement(Element.types.CUBE, mouseHit.point);
            }
        }
    }
}
