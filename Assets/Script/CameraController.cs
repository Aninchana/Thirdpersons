using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSentitivity = 3.0f;

    private float RotationX;
    private float RotationY;

    [SerializeField] private Transform target;
    [SerializeField] private float distanceFromTarget = 3.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationxMinMax = new Vector2 (-20, 30);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSentitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSentitivity;

        RotationY += mouseX;
        RotationX += mouseY;

        RotationX = Mathf.Clamp(RotationX, rotationxMinMax.x, rotationxMinMax.y);

        Vector3 nextRotation = new Vector3(RotationX, RotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;

    }
}
