using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.125f; // the higher the value faster the camera will lock onto target

    void LateUpdate() // exact same thing as Update except it runs right after --> so the target will already
    //have done all its movement by the time it is called
    {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed); // Linear interpellation = smoothly going from point A to point B
            transform.position = smoothedPosition;

            transform.LookAt(target);
    }
}
