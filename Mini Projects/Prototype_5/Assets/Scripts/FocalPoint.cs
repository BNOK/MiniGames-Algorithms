using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    public GameObject cameraObject;
    public Vector3 cameraOffset = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GetComponentInChildren<Camera>().gameObject;
        //cameraObject.transform.localPosition = cameraOffset;
        cameraObject.transform.LookAt(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * rotationSpeed, Space.Self);
    }
}
