using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class MonitorFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private VideoClip loadingVideo;
    [SerializeField] private VideoClip bootUpVideo;
    private VideoPlayer videoPlayer;
    private bool isPlayingLoading = false;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        blackScreen.SetActive(true);

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void TurnOnMonitor()
    {
        isPlayingLoading = true;
        videoPlayer.clip = loadingVideo;
        Destroy(blackScreen);
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer videoPlayer){
        if(isPlayingLoading){
            isPlayingLoading = false;
            videoPlayer.clip = bootUpVideo;
            videoPlayer.Play();
        } else {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
