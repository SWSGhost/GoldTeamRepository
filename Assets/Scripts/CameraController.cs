using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    [Tooltip("The player body in the scene is placed here. Make sure the camera is parented TO the player body.")]
    public Transform playerBody;

    [Tooltip("how fast the player can look left and right.")]
    public float mouseSensitivityX = 300f;
    [Tooltip("how fast the player can look up and down.")]
    public float mouseSensitivityY = 120f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

	    playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
	

}