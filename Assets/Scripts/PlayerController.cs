using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject[] targetCamera;
    public int cameraIndex;
    private bool isCoroutineDone;
    private CameraMouseMovement camMovementScript;
    private void Start()
    {
        isCoroutineDone = true;
        cameraIndex = 0;
        camMovementScript = mainCamera.GetComponent<CameraMouseMovement>();
    }
    void Update()
    {
        if (isCoroutineDone)
        {
            if (!camMovementScript.enabled && Input.mousePosition.x >= Screen.width - (Screen.width * 0.001f))
            {
                if(cameraIndex + 1 <= targetCamera.Length - 1)
                {
                    cameraIndex++;
                    StartCoroutine(MoveInCamera(0.5f));
                }
            }
            if (!camMovementScript.enabled && Input.mousePosition.x <= (Screen.width * 0.001f))
            {
                if (cameraIndex - 1 >= 0)
                {
                    cameraIndex--;
                    StartCoroutine(MoveInCamera(0.5f));
                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                isCoroutineDone = false;
                camMovementScript.enabled = !camMovementScript.isActiveAndEnabled;
                if (camMovementScript.enabled)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    StartCoroutine(MoveOutCamera(1f));
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    StartCoroutine(MoveInCamera(1f));
                }
            }
        }
    }
    private IEnumerator MoveInCamera(float duration)
    {
        Quaternion currRotationX = mainCamera.transform.rotation;
        Quaternion currRotationY = mainCamera.transform.rotation;
        Quaternion desiredAngle = targetCamera[cameraIndex].transform.rotation;
        float TargetLocationX = targetCamera[cameraIndex].transform.position.x;
        float TargetLocationY = targetCamera[cameraIndex].transform.position.y;
        float TargetLocationZ = targetCamera[cameraIndex].transform.position.z;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime / duration;
            mainCamera.transform.position = 
                new Vector3(
                Mathf.Lerp(mainCamera.transform.position.x, TargetLocationX, Mathf.SmoothStep(0f, 1f, t)), 
                Mathf.Lerp(mainCamera.transform.position.y, TargetLocationY, Mathf.SmoothStep(0f, 1f, t)), 
                Mathf.Lerp(mainCamera.transform.position.z, TargetLocationZ, Mathf.SmoothStep(0f, 1f, t))
                );   
            mainCamera.transform.rotation = Quaternion.Lerp(currRotationX, desiredAngle, Mathf.SmoothStep(0f, 1f, t));
            yield return null;

        }
        isCoroutineDone = true;
    }

    private IEnumerator MoveOutCamera(float duration)
    {
        float currentPositionY = mainCamera.transform.localPosition.y;
        float targetPositionY = 1.5f;
        float currentPositionZ = mainCamera.transform.localPosition.z;
        float targetPositionZ = 0f;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime / duration;
            mainCamera.transform.localPosition = new Vector3(0f, Mathf.Lerp(currentPositionY, targetPositionY, Mathf.SmoothStep(0f, 1f, t)), 
                Mathf.Lerp(currentPositionZ, targetPositionZ, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;

        }
        isCoroutineDone = true;
    }
}
