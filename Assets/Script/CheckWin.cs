using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckWin : MonoBehaviour
{
    public static CheckWin instance;

    public Camera defaultCamera;
    public Camera winCamera;
    public bool isWin = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public Transform playerRotation;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin) 
        {
            defaultCamera.enabled = false;
            winCamera.enabled = true;
        }
    }

    private void LateUpdate()
    {
         if(target != null && isWin) 
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z + 2.2f);
            Vector3 smoothedPosition = Vector3.Lerp(winCamera.transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
            winCamera.transform.position = smoothedPosition;

            playerRotation.LookAt(new Vector3(playerRotation.position.x,playerRotation.position.y,winCamera.transform.position.z));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.groundedPlayer)
        {
            isWin = true;
        }
    }
}
