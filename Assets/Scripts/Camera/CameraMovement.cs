using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed, rotationSpeed;

    private float xRotation = 0f, yRotation = 0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var forward = transform.forward;
            transform.Translate(new Vector3(forward.x, 0, forward.z).normalized *
                                (speed * Time.deltaTime), Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            var forward = transform.forward;
            transform.Translate(new Vector3(forward.x, 0, forward.z).normalized *
                                (-speed * Time.deltaTime), Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            var forward = transform.forward;
            transform.Translate(new Vector3(-forward.z, 0, forward.x).normalized * 
                                (speed * Time.deltaTime), Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            var forward = transform.forward;
            transform.Translate(new Vector3(-forward.z, 0, forward.x).normalized * 
                                (-speed * Time.deltaTime), Space.World);
        }

        if (Input.GetKey(KeyCode.Space))
            transform.Translate(new Vector3(0, speed * Time.deltaTime), Space.World);
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(new Vector3(0, -speed * Time.deltaTime), Space.World);

        if (Input.GetKey(KeyCode.F))
            xRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.R))
            xRotation -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            yRotation += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            yRotation -= rotationSpeed * Time.deltaTime;
        
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
