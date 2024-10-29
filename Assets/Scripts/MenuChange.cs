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
    }

    void OnSliderValueChanged(float value) { 
        ContinuousMoveProviderBase.moveSpeed = value;
        UpdateSpeedText(value);
    }

    void UpdateSpeedText(float value)
    {
        text.text = $"{value:F1}";
    }
}
