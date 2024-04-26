using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EPOOutline;

public class MoveGymbalToDesk : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftClickText;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Vector3 newRotation;
    [SerializeField] private float moveSpeedToDesk;
    [SerializeField] private float rotationSpeedToDesk;
    [SerializeField] private float colorShiftSpeed;

    [SerializeField] private float delayBetweenMoveAndZoom;
    [SerializeField] private float newCameraFOV;
    [SerializeField] private float fovChangeSpeed;

    [SerializeField] private MonitorFunctionality monitorFunctionality;
    [SerializeField] private float delayToTurnOnMonitor;

    [SerializeField] private Outlinable outlinable;

    [SerializeField] private float outlineFadeDuration = 1f; // Duration to fade in/out
    private Coroutine fadeCoroutine;
    private BoxCollider boxCollider;
    private Camera mainCamera;

    void Awake(){
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider>();
    }
    
    void OnMouseDown()
    {
        StartCoroutine(MoveObject());
        boxCollider.enabled = false;
    }

    void OnMouseEnter()
    {
        Debug.Log("Highlighting desk!!");
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOutlineTo(1.0f)); 
    }

    void OnMouseExit()
    {
        Debug.Log("Not Highlighting desk!!");
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOutlineTo(0.0f)); 
    }

    public IEnumerator FadeOutlineTo(float targetAlpha)
    {
        float timeElapsed = 0;
        Color startColor = outlinable.OutlineParameters.Color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (timeElapsed < outlineFadeDuration)
        {
            outlinable.OutlineParameters.Color = Color.Lerp(startColor, targetColor, timeElapsed / outlineFadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        outlinable.OutlineParameters.Color = targetColor; // Ensure target color is set after interpolation
    }

    private IEnumerator MoveObject()
    {
        Vector3 startPosition = transform.position; // Store the start position
        Vector3 startRotation = transform.rotation.eulerAngles; // Store the start rotation
        float moveTime = 0; // Timing for movement
        float rotateTime = 0; // Timing for rotation
        float colorShift = 0;
        Color initialColor = leftClickText.color;
        Color initialAmbientLight = RenderSettings.ambientLight;
        Color initialEquatorColor = RenderSettings.ambientEquatorColor;
        Color initialGroundColor = RenderSettings.ambientGroundColor;

        while (moveTime < 1 || rotateTime < 1)
        {
            if (moveTime < 1) {
                moveTime += Time.deltaTime * moveSpeedToDesk;
                transform.position = Vector3.Lerp(startPosition, newPosition, moveTime);
                leftClickText.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(initialColor.a, 0, moveTime));
            }
            if(colorShift < 1) {
                mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.white, moveTime);
                RenderSettings.ambientLight = Color.Lerp(initialAmbientLight, Color.white, moveTime);
                RenderSettings.ambientEquatorColor = Color.Lerp(initialEquatorColor, Color.white, moveTime);
                RenderSettings.ambientGroundColor = Color.Lerp(initialGroundColor, Color.white, moveTime);
            }
            if (rotateTime < 1) {
                rotateTime += Time.deltaTime * rotationSpeedToDesk;
                transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, newRotation, rotateTime));
            }

            yield return null;
        }

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation);

        yield return new WaitForSeconds(delayBetweenMoveAndZoom);

        StartCoroutine(ZoomOntoMonitor());
    }

    private IEnumerator ZoomOntoMonitor(){
        float startFOV = mainCamera.fieldOfView;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * fovChangeSpeed;
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, newCameraFOV, time);
            yield return null;
        }

        mainCamera.fieldOfView = newCameraFOV;

        yield return new WaitForSeconds(delayToTurnOnMonitor);
        
        monitorFunctionality.TurnOnMonitor();
    }
}
