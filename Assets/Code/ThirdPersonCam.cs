using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform lookAt, camTransform;
    Camera currentCam;
    public float offset = 20f;
    float currentX, currentY, sensivityX, sensivityY, minCamDown, maxCamUp;

    // Start is called before the first frame update
    void Start()
    {
        currentX = 0f;
        currentY = 0;
        sensivityX = 2f;
        sensivityY = 2f;

        camTransform = this.transform;
        currentCam = Camera.main;
        minCamDown = 0f;
        maxCamUp = 80f;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse Y") * sensivityY;
        currentY += Input.GetAxis("Mouse X") * sensivityX;

        //currentX = Mathf.Clamp(currentX, minCamDown, maxCamUp);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 5, -offset);
        Quaternion camRot = Quaternion.Euler(currentX, currentY, 0);

        camTransform.position = lookAt.position + camRot * direction;
        camTransform.LookAt(lookAt.position);
    }
}
