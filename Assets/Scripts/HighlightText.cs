using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using TMPro; 

public class HighlightText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI textMesh;
    private Color highlightColor = Color.white; 
    private Color originalColor;
    [SerializeField] private string WhatButton;

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>(); 
        }
        originalColor = textMesh.color; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color = highlightColor; 
        AudioManager.Instance.PlayHighlightOverButtonSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = originalColor; 
    }

    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("Buttonwasclicked!!!");
        AudioManager.Instance.PlayClickButtonSound();
        if(WhatButton == "AboutMe"){
            WebsiteManager.Instance.TurnOnAboutMePage();
        } else if (WhatButton == "Project"){
            WebsiteManager.Instance.TurnOnProjectsPage();
        } else if (WhatButton == "Resume"){
            WebsiteManager.Instance.TurnOnResumePage();
        }
    }
}
