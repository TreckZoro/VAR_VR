using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TeleportationProvider teleportationProvider; // Referencia al TeleportationProvider

    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    void Start()
    {
        Debug.Log($"La gravedad es: {gravity}");
    }

    void Update()
    {
        bool grounded = isGrounded();
        //Debug.Log($"IsGrounded: {grounded}");

        // Desactivar el teletransporte mientras el jugador está en el aire
        if (teleportationProvider != null)
        {
            teleportationProvider.enabled = grounded;
        }

        if (grounded && jumpButton.action.WasPressedThisFrame())
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;
        characterController.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity); // Para calcular la velocidad inicial
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayer);
    }
}
