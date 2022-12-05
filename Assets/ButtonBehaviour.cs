using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject Cube;
    Transform newTransform;

    public void SummonNewCube()
    {
        Instantiate(Cube, new Vector3(0f, 4f, 0f), Quaternion.identity);
    }
}
