using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;
    private bool isCoroutineDone;
    private CameraMouseMovement camMovementScript;
    private void Start()
    {
        isCoroutineDone = true;
        camMovementScript = mainCamera.GetComponent<CameraMouseMovement>();
    }
    void Update()
    {
        if (isCoroutineDone)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                isCoroutineDone = false;
                camMovementScript.enabled = !camMovementScript.isActiveAndEnabled;
                if (camMovementScript.enabled)
                {
                    StartCoroutine(MoveOutCamera(1f));
                }
                else
                {
                    StartCoroutine(MoveInCamera(1f));
                }
            }
        }
    }
    private IEnumerator MoveInCamera(float duration)
    {
        Quaternion currRotationX = mainCamera.transform.rotation;
        Quaternion currRotationY = transform.rotation;
        Quaternion desiredAngleX = Quaternion.Euler(12.72f, 0f, 0f);
        Quaternion desiredAngleY = Quaternion.Euler(0f, 0f, 0f);
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime / duration;
            mainCamera.transform.position = new Vector3(0, Mathf.Lerp(1.5f, 1.215351f, Mathf.SmoothStep(0f, 1f, t)), Mathf.Lerp(-10f, -9.521122f, Mathf.SmoothStep(0f, 1f, t)));
            mainCamera.transform.rotation = Quaternion.Lerp(currRotationX, desiredAngleX, Mathf.SmoothStep(0f, 1f, t));
            transform.rotation = Quaternion.Lerp(currRotationY, desiredAngleY, Mathf.SmoothStep(0f, 1f, t));
            yield return null;

        }
        isCoroutineDone = true;
    }

    private IEnumerator MoveOutCamera(float duration)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime / duration;
            mainCamera.transform.position = new Vector3(0f, Mathf.Lerp(1.215351f, 1.5f, Mathf.SmoothStep(0f, 1f, t)), Mathf.Lerp(-9.521122f, -10f, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;

        }
        isCoroutineDone = true;
    }
}
