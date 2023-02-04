using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonSelected : MonoBehaviour
{

    [SerializeField] private GameObject[] origin;
    public GameObject selectedOrigin;
    public GameObject[] target;
    public GameObject sphere;

    public scoreScript score;
    private PlaneScript planeScript;

    // Start is called before the first frame update
    void Awake()
    {
        int i = 0;
        if(origin == null || origin.Length == 0)
        {
            Debug.Log("Running");
            origin = GameObject.FindGameObjectsWithTag("Plane");
        }
        score.amountOfPlane = origin.Length;
        foreach(GameObject originTemp in origin)
        {
            origin[i] = originTemp.transform.GetChild(1).gameObject;
            i++;
        }
    }

    public bool isWaiting;
    // Update is called once per frame
    void Update()   
    {
        int i = 0;

        foreach (GameObject go in origin) 
        {
            if(go != null) 
            {
                if(EventSystem.current.currentSelectedGameObject == origin[i] && !isWaiting)
                {
                    isWaiting = true;
                    selectedOrigin = origin[i];
                }
                if (isWaiting)
                {
                    Debug.Log("Waiting...");
                }
                if (Input.GetMouseButtonDown(0) && isWaiting)
                {
                    int k = 0;
                    foreach(GameObject targets in target)
                    {
                        if(EventSystem.current.currentSelectedGameObject != target[k] && k == target.Length - 1) 
                        {
                            Debug.Log("Failed! " + EventSystem.current.currentSelectedGameObject + " vs " + target[k]);
                            isWaiting = false;
                            break;
                        }
                        if (EventSystem.current.currentSelectedGameObject == target[k] && isWaiting)
                        {
                            isWaiting = false;
                            Debug.Log("Success! " + EventSystem.current.currentSelectedGameObject);
                            planeScript = selectedOrigin.gameObject.transform.parent.gameObject.GetComponent<PlaneScript>();
                            Debug.Log(planeScript);
                            changePlaneState(EventSystem.current.currentSelectedGameObject);
                            break;
                        }
                        k++;
                    }

                }
            }
            i++;
        }

    }

    public void changePlaneState(GameObject target)
    {
        planeScript.setState(2);
        planeScript.getTarget(target.transform);
        Debug.Log(target.transform.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
