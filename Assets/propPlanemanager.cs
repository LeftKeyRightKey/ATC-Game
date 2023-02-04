using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propPlanemanager : MonoBehaviour
{
    public Transform prop;

    // Update is called once per frame
    void Update()
    {
        prop.Rotate(200f, 0.0f, 0.0f);
    }
}
