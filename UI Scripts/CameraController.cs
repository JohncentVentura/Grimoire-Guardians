using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public Transform targetObject;
    private Vector3 smoothDampVelocity = Vector3.zero;
    public float smoothTime = 0.2f;

    public Vector2 minPosition; //drag MainCamera from Unity Editor to bottom left of the scene and copy the x and y coordinates from Transform to this variable
    public Vector2 maxPosition; //drag MainCamera from Unity Editor to upper right of the scene and copy the x and y coordinates from Transform to this variable

    void FixedUpdate()
    {
        if(transform.position != targetObject.position && targetObject != null)
        {   
            Vector3 targetPosition = new(targetObject.position.x, targetObject.position.y, transform.position.z); 
            //targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            //targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothTime);
        }
    }
}
