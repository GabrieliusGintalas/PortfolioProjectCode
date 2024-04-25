using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for event interfaces
using TMPro;

public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
        if (text == null)
        {
            Debug.LogError("TextInteraction script requires a Text component on the same GameObject.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.white; 
        AudioManager.Instance.PlayHighlightOverButtonSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = originalColor; 
    }
}
