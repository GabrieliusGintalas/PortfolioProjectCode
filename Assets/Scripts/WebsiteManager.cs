using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor.EditorTools;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class WebsiteManager : MonoBehaviour
{
    public static WebsiteManager Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI WebsiteURLText;
    [SerializeField] private TextMeshProUGUI BrowserWebsiteNameText;
    [SerializeField] private GameObject AboutMePage;
    [SerializeField] private GameObject ProjectsPage;
    [SerializeField] private GameObject ResumePage;

    [SerializeField] private TextMeshProUGUI AboutMeText;
    [SerializeField] private TextMeshProUGUI ProjectsText;
    [SerializeField] private TextMeshProUGUI ResumeText;

    [SerializeField] private Color highlighedTextColor;
    private Color originalTextColor;

    private GameObject currentActivePage;
    
    private void Awake() {
        if(Instance != this && Instance != null){
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        ProjectsPage.SetActive(false);
        ResumePage.SetActive(false);
        currentActivePage = AboutMePage;

        originalTextColor = AboutMeText.color;
    }

    public void TurnOnAboutMePage() {
    if (CheckIfCurrentPageTheSame(AboutMePage)) return;
        SwitchPage(AboutMePage);
        WebsiteURLText.text = "https://gabrieliusgintalas.com/home";
        BrowserWebsiteNameText.text = "Gabrielius Gintalas Portfolio | About Me | Windows - Internet Explorer";
    }

    public void TurnOnProjectsPage() {
        if (CheckIfCurrentPageTheSame(ProjectsPage)) return;
        SwitchPage(ProjectsPage);
        WebsiteURLText.text = "https://gabrieliusgintalas.com/projects";
        BrowserWebsiteNameText.text = "Gabrielius Gintalas Portfolio | Projects | Windows - Internet Explorer";
    }

    public void TurnOnResumePage() {
        if (CheckIfCurrentPageTheSame(ResumePage)) return;
        SwitchPage(ResumePage);
        WebsiteURLText.text = "https://gabrieliusgintalas.com/resume";
        BrowserWebsiteNameText.text = "Gabrielius Gintalas Portfolio | Resume | Windows - Internet Explorer";
    }

    private void SwitchPage(GameObject page) {
        currentActivePage.SetActive(false);
        page.SetActive(true);
        currentActivePage = page;
    }

    private bool CheckIfCurrentPageTheSame(GameObject pageToActivate){
        return currentActivePage == pageToActivate;
    }
}
