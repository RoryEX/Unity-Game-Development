using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCamera3D : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField]
    private float maxVerticalTilt = 90;

    private float mouseSensitivity = 10f;

    private Vector3 camRotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TiltCamera();
    }

    void TiltCamera()
    {
        camRotation.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        camRotation.x = Mathf.Clamp(camRotation.x, -maxVerticalTilt, maxVerticalTilt);

        transform.localEulerAngles = camRotation;
    }
}
