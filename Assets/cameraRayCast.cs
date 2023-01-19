using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cameraRayCast : MonoBehaviour
{
    RaycastHit[] hit;
    public LayerMask ignoredLayer;
    public Transform hitPoint;

    private bool coroutineDone;
    // Start is called before the first frame update
    void Start()
    {
        coroutineDone = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
