using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    RectTransform planeTransform;
    
    public RectTransform rayCastOrigin;
    public float planeSpeed;
    public float maxRotation;
    public LayerMask ignoredLayer;
    public Vector3 target;
    public GameObject debugSphere;
    public int stateIndex;


    private Rigidbody rb;
    private RectTransform baseTransform;
    private RaycastHit hit;
    private Transform newTarget;
    private float planeRangeWithTarget;
    private float epochRange;
    private int childIndex;

    public enum PlaneState
    {
        wander,
        idleTakeOff,
        approachFirstEpoch,
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
        baseTransform = GameObject.FindGameObjectWithTag("Base").gameObject.GetComponent<RectTransform>();
        rb = gameObject.GetComponent<Rigidbody>();
        epochRange = 10f;
        setState(0);
        childIndex = 0;
    }

    void Update()
    {
        AIBehaviour();
        //Use LocalPosition to move things!!! 
        if(rb.velocity.sqrMagnitude < planeSpeed)
        {
            planeTransform.localPosition = new Vector3(planeTransform.localPosition.x, planeTransform.localPosition.y, 0f);
            rb.velocity = -planeTransform.up * planeSpeed * 0.001f;
        }
    }

    void AIBehaviour()
    {
        Vector3 forward = planeTransform.TransformDirection(-Vector3.up) * 10;
        Physics.Raycast(rayCastOrigin.position, forward, out hit, Mathf.Infinity, ~ignoredLayer);
        Debug.DrawRay(rayCastOrigin.position, forward, Color.green);

        //Wandering
        if (planeState == PlaneScript.PlaneState.wander)    
        {
            target = baseTransform.position;
            if (hit.collider)
            {   
                if (hit.collider.tag == "Base")
                {
                    rb.angularVelocity = Vector3.zero;
                }
            }else if(hit.collider == null)
                rotateTowardSomething(maxRotation, target, false);
        }
        //Approaching
        if(planeState == (PlaneState)stateIndex && stateIndex > 1 && stateIndex < 7)
        {
            Vector3 targetDistance = newTarget.GetChild(childIndex).position;
            target = newTarget.GetChild(childIndex).position;
            planeRangeWithTarget = Vector3.Distance(planeTransform.position, targetDistance) * 100f;
            Debug.Log(planeRangeWithTarget);
            rotateTowardSomething(maxRotation, target, false);
            if (epochRange <= 0) epochRange = 0.2f;
            if (planeRangeWithTarget <= epochRange)
            {
                childIndex++;
                epochRange -= 2f;
                setState(stateIndex + 1);
            }
        }

        if(planeState == PlaneState.approachFinalEpoch)
        {
            target = newTarget.position;
            Debug.Log("Running...");
            planeRangeWithTarget = Vector3.Distance(planeTransform.position, target) * 100f;
            Debug.Log(planeRangeWithTarget);
            rotateTowardSomething(maxRotation, target, false);
            if (reachedTarget(planeRangeWithTarget))
            {
                gameObject.SetActive(false);
            }
        }
    }

    void rotateTowardSomething(float maxRotation, Vector3 target, bool away)
    {
        Vector3 direction = target - (Vector3)planeTransform.position;
        direction.Normalize();

        float rotationAmmount = 0f;

        if (away)
            rotationAmmount = Vector3.Cross(direction, planeTransform.up).z;
        else
            rotationAmmount = Vector3.Cross(direction, -planeTransform.up).z;
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(-planeTransform.forward * rotationAmmount * maxRotation * 180f);
        rb.maxAngularVelocity = maxRotation;

        //Debug.Log(rotationAmmount * 180f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plane")
        {

            GameObject.Destroy(this);
        }
    }

    public void getTarget(Transform newTarget)
    {
        this.newTarget = newTarget;
    }

    private bool reachedTarget(float planeRangeWithTarget)
    {
        if (planeRangeWithTarget <= 0.2f)
        {
            return true;
        }
        return false;
    }

    public void setState(int i)
    {
        stateIndex = i;
        planeState = (PlaneState)i;
    }

}
