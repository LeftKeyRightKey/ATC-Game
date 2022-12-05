using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    RectTransform planeTransform;

    enum planeAIState
    {
        wander,
        idleTakeOff,
        approachFirstEpich,
        approachEpoch2,
        approachEpoch3,
        approachEpoch4,
        approachEpoch5,
        approachFinalEpoch
    };

    void Start()
    {
        //To move object in UI, use RectTransform instead of Transform.
        planeTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        AIBehaviour();
        //Use LocalPosition to move things!!! 
        planeTransform.localPosition += new Vector3(20f, 0f, 0f) * Time.deltaTime;
    }

    void AIBehaviour()
    {

    }
}
