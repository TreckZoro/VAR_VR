using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{
    public ContinuousMoveProviderBase ContinuousMoveProviderBase;
    public ContinuousTurnProviderBase ContinuousTurnProviderBase; // Imagino que es este
    public Slider speedSlider; // Para movimiento personaje
    public Slider turnSpeedSlider;  // Slider para la velocidad de giro
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI turnText;

    public Button spawnBanderaButton;
    public Button quitGameButton;  // Referencia al bot�n de salida
    public GameObject bandera; //De momento es un cilindro

    public Transform player;

    private void Start()
    {
        if (ContinuousMoveProviderBase == null) {
            Debug.LogError("No hay ContinuousMoveProvider");
            return;
        }
        if (ContinuousTurnProviderBase == null)
        {
            Debug.LogError("No hay ContinuousTurnProvider");
            return;
        }


        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
        turnSpeedSlider.onValueChanged.AddListener(OnTurnSpeedSliderValueChanged);

        speedSlider.value = ContinuousMoveProviderBase.moveSpeed;
        UpdateSpeedText(ContinuousMoveProviderBase.moveSpeed);

        turnSpeedSlider.value = ContinuousTurnProviderBase.turnSpeed;
        UpdateTurnText(ContinuousTurnProviderBase.turnSpeed);

        // Asegurarnos de que el bot�n BanderaSpawn est� configurado correctamente
        if (spawnBanderaButton != null)
        {
            spawnBanderaButton.onClick.AddListener(SpawnCube);
        }
        else
        {
            Debug.LogError("El bot�n BanderaSpawn no est� asignado.");
        }

        // Configuraci�n del bot�n QuitGame
        if (quitGameButton != null)
        {
            //Este para cerrar el juego
            quitGameButton.onClick.AddListener(QuitGame);
            
            //Este para salir al menu principal
            //quitGameButton.onClick.AddListener(GoToMainMenu);
        }
        else
        {
            Debug.LogError("El bot�n QuitGame no est� asignado.");
        }
    }

    //Velocidad del player
    void OnSliderValueChanged(float value) { 
        ContinuousMoveProviderBase.moveSpeed = value;
        UpdateSpeedText(value);
    }

    //Velocidad de giro del jugador
    void OnTurnSpeedSliderValueChanged(float value){
        ContinuousTurnProviderBase.turnSpeed = value;
        UpdateTurnText(value);
    }


    void UpdateSpeedText(float value)
    {
        speedText.text = $"{value:F1}";
    }

    void UpdateTurnText(float value)
    {
        turnText.text = $"{value:F1}";
    }

    void SpawnCube()
    {
        if (bandera != null && player != null)
        {
            // Posicion del jugador + 1 metro hacia adelante
            Vector3 spawnPosition = player.position + player.forward;
            Debug.Log(spawnPosition);

            // Instanciamos el cubo en la posici�n calculada
            Instantiate(bandera, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No se puede generar el cubo, aseg�rese de que el prefab y el jugador est�n asignados.");
        }
    }

    // Funci�n para salir del juego
    void QuitGame()
    {
        #if UNITY_EDITOR
            // Si estamos en el editor, detenemos la simulaci�n
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Si estamos en una build del juego, cerramos la aplicaci�n
            Application.Quit();
        #endif
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("1 Start Scene");
    }

}
