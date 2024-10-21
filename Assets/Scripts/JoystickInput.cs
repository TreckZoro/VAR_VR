using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRJoystick : MonoBehaviour
{
    public InputActionProperty joystickAction;
    public XRRayInteractor xRayInteractor;
    // Start is called before the first frame update
    void Start()
    {
        if (xRayInteractor == null)
        {
            xRayInteractor = GetComponent<XRRayInteractor>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 joystickValue = joystickAction.action.ReadValue<Vector2>();



        Debug.Log("Joystick x :" + joystickValue.x);
        Debug.Log("Joystick y :" + joystickValue.y);

    }
}
