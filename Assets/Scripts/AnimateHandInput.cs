using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    public Animator handanimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>(); //Comprueba que se ha pulsado el gatillo (boton trigger)
        handanimator.SetFloat("Trigger", triggerValue); // anima la cantidad del pulso hecha
        //Debug.Log(triggerValue);
    
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handanimator.SetFloat("Grip", gripValue);
    }
}
