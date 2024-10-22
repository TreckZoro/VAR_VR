using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportActivatorç : MonoBehaviour
{
    public GameObject teleportationRay;
    public InputActionProperty teleportActivation;
    public InputActionProperty teleportDeactivation;
    void Update()
    {
        teleportationRay.SetActive(teleportDeactivation.action.ReadValue<float>() == 0 && teleportActivation.action.ReadValue<float>() > 0.2f);
    }
}
