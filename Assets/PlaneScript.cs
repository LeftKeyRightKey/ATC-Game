using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    RectTransform planeTransform;
    public RectTransform hitPoint;
    public RectTransform baseTransform;
    public RectTransform rayCastOrigin;
    public float planeSpeed;
    public float maxRotation;
    public LayerMask ignoredLayer;
    private Rigidbody rb;
    private RaycastHit hit;

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
    //public Transform[] points; 

    public PlaneState planeState = new PlaneState();

    void Start()
    {
        planeTransform = gameObject.GetComponent<RectTransform>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        AIBehaviour();
        //Use LocalPosition to move things!!! 
        if(rb.velocity.sqrMagnitude < planeSpeed)
        {
            rb.velocity = -planeTransform.up * planeSpeed * 0.001f;
        }
    }

    void AIBehaviour()
    {
        Vector2 target = baseTransform.position;

        float planeRangeWithBase = Vector2.Distance(planeTransform.position, target);
        Vector3 forward = transform.TransformDirection(-Vector3.up) * 10;
        Physics.Raycast(rayCastOrigin.position, forward, out hit, Mathf.Infinity, ~ignoredLayer);       

        Debug.DrawRay(rayCastOrigin.position, forward, Color.green);
            
        if (planeState == PlaneState.wander)
        {
            Debug.Log(hit.collider);
            if (hit.collider)
            {   
                if (hit.collider.tag == "Base")
                {
                    rb.angularVelocity = Vector3.zero;
                }
            }else if(hit.collider == null)
                rotateTowardBase(maxRotation, target, false);

        }
    }

    void rotateTowardBase(float maxRotation, Vector2 target, bool away)
    {
        Vector2 direction = target - (Vector2)planeTransform.position;
        direction.Normalize();

        float rotationAmmount = 0f;

        if (away)
            rotationAmmount = Vector3.Cross(direction, transform.up).z;
        else
            rotationAmmount = Vector3.Cross(direction, -transform.up).z;
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(-transform.forward * rotationAmmount * maxRotation);
        rb.maxAngularVelocity = maxRotation;

        //Debug.Log(rotationAmmount * 180f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plane")
        {

        }
    }

}
