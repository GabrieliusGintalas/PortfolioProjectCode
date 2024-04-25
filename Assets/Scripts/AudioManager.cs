using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    private AudioSource audioSource;
    [SerializeField] private AudioClip fanSound;
    [SerializeField] private AudioClip highlightOverButtonSound;
    [SerializeField] private AudioClip clickButtonSound;
    private bool enableButtonClicks = false;

    void Awake(){
        if(Instance != this && Instance != null){
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHighlightOverButtonSound()
    {
        PlaySound(highlightOverButtonSound);
    }

    public void PlayClickButtonSound()
    {
        PlaySound(clickButtonSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if(!enableButtonClicks) return;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
    public AudioClip GetHighlightOverButtonSound()
    {
        return highlightOverButtonSound;
    }

    public AudioClip GetClickButtonSound()
    {
        return clickButtonSound;
    }

    public void TurnOnButtonClicks(){
        enableButtonClicks = true;
    }
}
