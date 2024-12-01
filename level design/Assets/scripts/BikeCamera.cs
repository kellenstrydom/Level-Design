using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeCamera : MonoBehaviour
{
    public Vector3 camOffset;
    public float camLookDownAngle;

    public Transform orientation;
    

    // Update is called once per frame
    void Update()
    {
            transform.position = orientation.position - orientation.forward * camOffset.z + Vector3.up * camOffset.y;
        transform.forward = orientation.forward;
        transform.Rotate(new Vector3(-camLookDownAngle,0,0));
    }
}
