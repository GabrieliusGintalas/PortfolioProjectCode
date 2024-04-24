using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveGymbalToDesk : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftClickText;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Vector3 newRotation;
    [SerializeField] private float moveSpeedToDesk;
    [SerializeField] private float rotationSpeedToDesk;

    [SerializeField] private float delayBetweenMoveAndZoom;
    [SerializeField] private float newCameraFOV;
    [SerializeField] private float fovChangeSpeed;

    [SerializeField] private MonitorFunctionality monitorFunctionality;
    [SerializeField] private float delayToTurnOnMonitor;

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

    private IEnumerator MoveObject()
    {
        Vector3 startPosition = transform.position; // Store the start position
        Vector3 startRotation = transform.rotation.eulerAngles; // Store the start rotation
        float moveTime = 0; // Timing for movement
        float rotateTime = 0; // Timing for rotation
        Color initialColor = leftClickText.color;

        while (moveTime < 1 || rotateTime < 1)
        {
            if (moveTime < 1) {
                moveTime += Time.deltaTime * moveSpeedToDesk;
                transform.position = Vector3.Lerp(startPosition, newPosition, moveTime);
                leftClickText.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(initialColor.a, 0, moveTime));
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
