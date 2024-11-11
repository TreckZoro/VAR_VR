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

    public Toggle tunnelToggle;

    public Button spawnBanderaButton;
    public Button quitGameButton;  // Referencia al botón de salida

    public TMP_Dropdown interactionLayerDropd;
    
    public GameObject bandera; //De momento es un cilindro

    public GameObject[] tunnelObjects;  // Para los objetos con tag "Tunnel"
    public GameObject[] tunnelTPObjects;  // Para los objetos con tag "TunnelTP

    public Transform player;

    private void Start()
    {
        // Buscar los objetos al inicio de la escena
        tunnelObjects = GameObject.FindGameObjectsWithTag("Tunnel");
        tunnelTPObjects = GameObject.FindGameObjectsWithTag("TunnelTP");

        // Comprobamos que se encontraron los objetos
        if (tunnelObjects.Length == 0 || tunnelTPObjects.Length == 0)
        {
            Debug.LogError("No se encontraron objetos con los tags 'Tunnel' o 'TunnelTP'.");
        }

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

        // Asegurarnos de que el botón BanderaSpawn esté configurado correctamente
        if (spawnBanderaButton != null)
        {
            spawnBanderaButton.onClick.AddListener(SpawnCube);
        }
        else
        {
            Debug.LogError("El botón BanderaSpawn no está asignado.");
        }

        if (tunnelToggle != null)
        {
            tunnelToggle.onValueChanged.AddListener(OnTunnellingChecked);
        }
        else
        {
            Debug.LogError("El botón de tunel no está asignado.");
        }

        // Configuración del botón QuitGame
        if (quitGameButton != null)
        {
            //Este para cerrar el juego
            quitGameButton.onClick.AddListener(QuitGame);
            
            //Este para salir al menu principal
            //quitGameButton.onClick.AddListener(GoToMainMenu);
        }
        else
        {
            Debug.LogError("El botón QuitGame no está asignado.");
        }

        if (interactionLayerDropd != null) {
            interactionLayerDropd.onValueChanged.AddListener(OnInteractionLayerChanged);
        }
        else {
            Debug.LogError("El DropDown de iteraccion no está asignado.");
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
        // Buscar una bandera existente en la escena
        GameObject banderaExistente = GameObject.FindWithTag("Bandera");

        if (banderaExistente != null)
        {
            // Si ya existe una bandera, la destruimos
            Destroy(banderaExistente);
        }

        // Asegurarnos de que la bandera y el jugador estén asignados
        if (bandera != null && player != null)
        {
            // Posición del jugador + 1 metro hacia adelante
            Vector3 spawnPosition = player.position + player.forward + player.up;
            //Debug.Log("Posición de la nueva bandera: " + spawnPosition);

            // Instanciamos la nueva bandera en la posición calculada
            GameObject nuevaBandera = Instantiate(bandera, spawnPosition, Quaternion.identity);
            nuevaBandera.tag = "Bandera"; // Asignamos la etiqueta "Bandera" para identificarla
        }
        else
        {
            Debug.LogError("No se puede generar la bandera, asegúrese de que el prefab y el jugador estén asignados.");
        }
    }

    void OnInteractionLayerChanged(int i) {
        GameObject[] piedras = GameObject.FindGameObjectsWithTag("moonRock");

        foreach (GameObject piedra in piedras) { 
            var grabInteractable = piedra.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null) {
                // Cambiar la layer del interaction mask segun la opcion
                if (i == 0)
                {
                    grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Direct Grab");
                }
                else if (i == 1) {

                    grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Distance Grab");
                }
                
            }
        }
        Debug.Log(i == 0 ? "Piedras configuradas para Direct Grab" : "Piedras configuradas para Distance Grab");
    }

    void OnTunnellingChecked(bool activated)
    {
        Debug.Log("Toggle is now " + activated);

        // Si no se encontraron los objetos, no realizamos nada
        if (tunnelObjects.Length == 0 || tunnelTPObjects.Length == 0)
        {
            Debug.LogError("Faltan los objetos para manejar el tunneling.");
            return;
        }

        // Activamos o desactivamos los objetos de manera directa
        foreach (var obj in tunnelObjects)
        {
            obj.SetActive(activated);
        }

        foreach (var obj in tunnelTPObjects)
        {
            obj.SetActive(activated);
        }

        // Mensaje adicional para depurar
        Debug.Log(activated ? "Tunneling activado" : "Tunneling desactivado");


    }


    // Función para salir del juego
    void QuitGame()
    {
        UnityEngine.Application.Quit();

    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("1 Start Scene");
    }

}
