using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : NetworkBehaviour
{
    [SerializeField]
    private const float y_angle_min = 0f;
    [SerializeField]
    private const float y_angle_max = 70f;

    [SerializeField]
    private Transform lookAt;
    [SerializeField]
    private Transform camTransform;

    [SerializeField]
    private float currentX = 0f;
    [SerializeField]
    private float currentY = 0f;
    [SerializeField]
    private float sensitivityX = 4f;
    [SerializeField]
    private float sensitivityY = 1f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensitivityY;

        currentY = Mathf.Clamp(currentY, y_angle_min, y_angle_max);
        
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        camTransform.LookAt(lookAt);
        lookAt.rotation = Quaternion.Euler(currentY, currentX, 0);
    }
}
