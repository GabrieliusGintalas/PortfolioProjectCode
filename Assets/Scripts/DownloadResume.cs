using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for event interfaces
using TMPro;

public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        DownloadFile();
        AudioManager.Instance.PlayClickButtonSound();
    }

    private void DownloadFile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        DownloadFile_JS(); 
#else
        Debug.Log("Download functionality is currently supported only in WebGL builds.");
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void DownloadFile_JS();
#endif
}
