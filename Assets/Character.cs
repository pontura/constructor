using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public InputManager inputManager;

    void Start()
    {

    }
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        if (inputManager._h != 0) rot.y += inputManager._h;
        if (inputManager._v != 0) rot.x += inputManager._v;
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, rot, 0.5f);
    }

}
