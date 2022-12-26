using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    RectTransform planeTransform;
    public float planeSpeed;

    public enum PlaneState
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
    public float speed;
    public int MaxIndex;
    int Pindex;
    Transform movepoint;
    public Transform[] points; 

    public PlaneState planeState = new PlaneState();

    void Start()
    {
        //To move object in UI, use RectTransform instead of Transform.
        planeTransform = gameObject.GetComponent<RectTransform>();
        Pindex = 0;
        movepoint = points[Pindex];
    }

    void Update()
    {
        AIBehaviour();
        //Use LocalPosition to move things!!! 
        planeTransform.localPosition += new Vector3(planeSpeed, 0f, 0f) * Time.deltaTime;
    }

    void AIBehaviour()
    {
        Transform.position = Vector3.MoveTowards(Transform.position,movepoint.position, speed * deltaTime)
        if(Vector3.distance(Transform.position,movepoint.position)<=0){
            if(Pindex>MaxIndex){
                Pindex = 0;
            }
        }
        Pindex++;
        movepoint = points[Pindex];
    }
    Vector3 pos = movepoint.position - Transform.position;
    float angle = MathF.Atan2(float y, float x) * MathF.Rad2Deg;
    Transform.rotation = Quaternion.Euler(0,0,angle -90);
}
