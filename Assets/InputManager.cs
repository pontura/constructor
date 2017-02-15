using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public int _v;
    public int _h;

    void Update()
    {
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
