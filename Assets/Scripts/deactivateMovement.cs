using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableMovementAndRotationOnAnyGrab : MonoBehaviour
{
    public ContinuousMoveProviderBase continuousMoveProvider; // Reference to ContinuousMoveProvider
    public ContinuousTurnProviderBase continuousTurnProvider;      // Reference to ContinuousTurnProvider

    public XRBaseInteractor leftHandInteractor;  // Left hand XRBaseInteractor (Ray Interactor or Direct Interactor)
    public XRBaseInteractor rightHandInteractor; // Right hand XRBaseInteractor (Ray Interactor or Direct Interactor)

    private bool isGrabbing = false; // To track if something is grabbed

    private void OnEnable()
    {
        // Subscribe to the select events for the left and right hand interactors
        if (leftHandInteractor != null)
        {
            leftHandInteractor.selectEntered.AddListener(OnGrabStarted);
            leftHandInteractor.selectExited.AddListener(OnGrabEnded);
        }
        if (rightHandInteractor != null)
        {
            rightHandInteractor.selectEntered.AddListener(OnGrabStarted);
            rightHandInteractor.selectExited.AddListener(OnGrabEnded);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the events when the script is disabled
        if (leftHandInteractor != null)
        {
            leftHandInteractor.selectEntered.RemoveListener(OnGrabStarted);
            leftHandInteractor.selectExited.RemoveListener(OnGrabEnded);
        }
        if (rightHandInteractor != null)
        {
            rightHandInteractor.selectEntered.RemoveListener(OnGrabStarted);
            rightHandInteractor.selectExited.RemoveListener(OnGrabEnded);
        }
    }

    // Called when an object is grabbed
    private void OnGrabStarted(SelectEnterEventArgs args)
    {
        // Set the flag to true if either hand grabs an object
        isGrabbing = true;

        // Disable movement and rotation while the object is being grabbed
        DisableMovementAndRotation();
    }

    // Called when an object is released
    private void OnGrabEnded(SelectExitEventArgs args)
    {
        // Check if no hand is grabbing anything
        if (!leftHandInteractor.hasSelection && !rightHandInteractor.hasSelection)
        {
            isGrabbing = false;
        }

        // Re-enable movement and rotation if nothing is grabbed
        if (!isGrabbing)
        {
            EnableMovementAndRotation();
        }
    }

    // Disable movement and rotation
    private void DisableMovementAndRotation()
    {
        

        if (continuousTurnProvider != null)
        {
            continuousTurnProvider.enabled = false; // Disable rotation
        }
    }

    // Enable movement and rotation
    private void EnableMovementAndRotation()
    {
        if (continuousMoveProvider != null)
        {
            continuousMoveProvider.enabled = true; // Enable movement
        }

        if (continuousTurnProvider != null)
        {
            continuousTurnProvider.enabled = true; // Enable rotation
        }
    }
}
