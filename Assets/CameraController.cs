using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 0.2f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;

    Quaternion startRot;

    private void Start()
    {
        startRot = transform.localRotation;
        resetRotation();
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startRot, 10 * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            resetRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void resetRotation()
    {
        currentRotation = new Vector2(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.x);
    }
}