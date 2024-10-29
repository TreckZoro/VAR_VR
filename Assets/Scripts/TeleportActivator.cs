using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportActivatorç : MonoBehaviour
{
    public GameObject teleportationRay;

    public InputActionProperty teleportActivation;
    public InputActionProperty teleportDeactivation;

    public GameObject menu;

    public XRRayInteractor interactor;
    void Update()
    {
        if ( menu.activeSelf)
        {
            bool isRayHovering = interactor.TryGetHitInfo(out Vector3 pos, out Vector3 normal, out int number, out bool valid);
            //Explain of parameters
            //Pos -> direccion del rayo
            //Normal -> Respecto al objeto que toca el rayo
            // Number -> Identificador que devuelve cual objeto ha sido golpeado
            //Valid -> True si ha impactado un objeto

            teleportationRay.SetActive(!isRayHovering && teleportDeactivation.action.ReadValue<float>() == 0 && teleportActivation.action.ReadValue<float>() > 0.1f);
        }

        else
        {
            teleportationRay.SetActive(teleportDeactivation.action.ReadValue<float>() == 0 && teleportActivation.action.ReadValue<float>() > 0.2f);
        }
    }
}
