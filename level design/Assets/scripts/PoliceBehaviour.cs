using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehaviour : MonoBehaviour
{
    public List<Vector3> patrolPoints;
    public Vector3 lookDir;
    public Transform orientation;

    public float sightDistance;
    public LayerMask whatIsPlayer;

    [Header("Movement")]
    public float walkSpeed;
    private Rigidbody rb;

    [Header("Looking")] 
    public float xAngleDeg;
    public float yAngleDeg;

    public float xAngleRad;
    public float yAngleRad;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        xAngleRad = (float)(xAngleDeg / 180 * Math.PI);
        yAngleRad = (float)(yAngleDeg / 180 * Math.PI);
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        //MoveBetweenPoints();
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 lookDir = Vector3.forward;
        for (int i = -10; i < 10; i++)
        {
            Vector3 yLookDir = Vector3.Normalize(
                new Vector3(lookDir.x,
                    (float)(lookDir.y * Math.Cos(yAngleDeg / 180 * Math.PI * i/20) - lookDir.z * Math.Sin(yAngleDeg / 180 * Math.PI * i/20)),
                    (float)(lookDir.y * Math.Sin(yAngleDeg / 180 * Math.PI * i/20) + lookDir.z * Math.Cos(yAngleDeg / 180 * Math.PI * i/20))));
            
                for (int j = -10; j < 10; j++)
                {
                    Vector3 scanDir = Vector3.Normalize(
                        new Vector3((float)(yLookDir.x * Math.Cos(xAngleDeg/ 180 * Math.PI * j/20) - yLookDir.z * Math.Sin(xAngleDeg/ 180 * Math.PI * j/20)),
                            yLookDir.y,
                            (float)(-1*yLookDir.x * Math.Sin(xAngleDeg/ 180 * Math.PI * j/20) + yLookDir.z * Math.Cos(xAngleDeg/ 180 * Math.PI * j/20))));
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(new Vector3(0,1,0),scanDir*sightDistance);
                }
        }
        
    }

    void LookForPlayer()
    {
        //https://stackoverflow.com/questions/14607640/rotating-a-vector-in-3d-space
         
        Vector3 lookDir = orientation.forward;
        for (int i = -10; i < 10; i++)
        {
            Vector3 yLookDir = Vector3.Normalize(
                new Vector3(lookDir.x,
                    (float)(lookDir.y * Math.Cos(yAngleDeg / 180 * Math.PI * i/20) - lookDir.z * Math.Sin(yAngleDeg / 180 * Math.PI * i/20)),
                    (float)(lookDir.y * Math.Sin(yAngleDeg / 180 * Math.PI * i/20) + lookDir.z * Math.Cos(yAngleDeg / 180 * Math.PI * i/20))));
            
            for (int j = -10; j < 10; j++)
            {
                Vector3 scanDir = Vector3.Normalize(
                    new Vector3((float)(yLookDir.x * Math.Cos(xAngleDeg/ 180 * Math.PI * j/20) - yLookDir.z * Math.Sin(xAngleDeg/ 180 * Math.PI * j/20)),
                        yLookDir.y,
                        (float)(-1*yLookDir.x * Math.Sin(xAngleDeg/ 180 * Math.PI * j/20) + yLookDir.z * Math.Cos(xAngleDeg/ 180 * Math.PI * j/20))));
                if (Physics.Raycast(transform.position, scanDir, sightDistance, whatIsPlayer))
                {
                    Debug.Log("CAUGHT");
                }
            }
        }
        
        if (Physics.Raycast(transform.position, lookDir, sightDistance, whatIsPlayer))
        {
            Debug.Log("CAUGHT");
        }
    }

    void MoveBetweenPoints()
    {
        if (patrolPoints.Count == 0) return;
        //Vector3 desPoint = patrolPoints[0];
        //Debug.Log(desPoint);
        orientation.forward = new Vector3(patrolPoints[0].x - orientation.position.x, 0,
            patrolPoints[0].z - orientation.position.z).normalized;
        rb.AddForce(orientation.forward * walkSpeed, ForceMode.Force);
        
        // limit velocity if needed
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > walkSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * walkSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if ((patrolPoints[0] - orientation.position).magnitude < 0.25)
        {
            Vector3 temp = patrolPoints[0];
            patrolPoints.RemoveAt(0);
            patrolPoints.Add(temp);
        }
    }
}
