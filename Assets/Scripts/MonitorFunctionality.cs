using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class MonitorFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private float blackScreenTime;
    [SerializeField] private string loadingVideo;
    [SerializeField] private string bootUpVideo;
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
        blackScreen.SetActive(false);
        if(videoPlayer){
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, loadingVideo);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }

    private void OnVideoFinished(VideoPlayer vp){
        if (isPlayingLoading) {
            StartCoroutine(WaitForBlackScreen());
        } else {
            AudioManager.Instance.TurnOnButtonClicks();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitForBlackScreen(){
        blackScreen.SetActive(true);

        // Wait for the specified black screen time
        yield return new WaitForSeconds(blackScreenTime);

        blackScreen.SetActive(false);

        // Proceed with playing the second video after the black screen is hidden
        isPlayingLoading = false;
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, bootUpVideo);
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
