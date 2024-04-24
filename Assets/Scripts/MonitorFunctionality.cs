using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MonitorFunctionality : MonoBehaviour
{
    [SerializeField] private Material monitorMaterial;
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void TurnOnMonitor()
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
