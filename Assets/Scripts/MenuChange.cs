using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class MenuChange : MonoBehaviour
{
    public ContinuousMoveProviderBase ContinuousMoveProviderBase;
    public Slider speedSlider;
    public TextMeshProUGUI text;

    public Button spawnBanderaButton;
    public GameObject bandera; //De momento es un cubo

    public Transform player;

    private void Start()
    {
        if (ContinuousMoveProviderBase == null) {
            Debug.LogError("No hay ContinuousMoveProvider");
            return;
        }
        if (ContinuousMoveProviderBase == null)
        {
            Debug.LogError("No hay ContinuousMoveProvider");
            return;
        }

        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);

        speedSlider.value = ContinuousMoveProviderBase.moveSpeed;
        UpdateSpeedText(ContinuousMoveProviderBase.moveSpeed);

        // Asegurarnos de que el botón BanderaSpawn esté configurado correctamente
        if (spawnBanderaButton != null)
        {
            spawnBanderaButton.onClick.AddListener(SpawnCube);
        }
        else
        {
            Debug.LogError("El botón BanderaSpawn no está asignado.");
        }
    }

    void OnSliderValueChanged(float value) { 
        ContinuousMoveProviderBase.moveSpeed = value;
        UpdateSpeedText(value);
    }

    void UpdateSpeedText(float value)
    {
        text.text = $"{value:F1}";
    }

    void SpawnCube()
    {
        if (bandera != null && player != null)
        {
            // Posicion del jugador + 1 metro hacia adelante
            Vector3 spawnPosition = player.position + player.forward;
            Debug.Log(spawnPosition);

            // Instanciamos el cubo en la posición calculada
            Instantiate(bandera, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No se puede generar el cubo, asegúrese de que el prefab y el jugador estén asignados.");
        }
    }
}
