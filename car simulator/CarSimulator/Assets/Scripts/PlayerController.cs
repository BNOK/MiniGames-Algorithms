using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vehicleSpeed = 10.0f;
    public float rotationSpeed = 60.0f;

    private Rigidbody vehicleRb;

    // Start is called before the first frame update
    void Start()
    {
        vehicleRb = GetComponent<Rigidbody>();
        if (vehicleRb == null)
        {
            gameObject.AddComponent<Rigidbody>();
            vehicleRb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void FixedUpdate()
    {
        
    }

    private void MovePlayer()
    {
        Vector3 forwardVector = transform.forward * Input.GetAxis("Vertical") * vehicleSpeed;
        Vector3 rotationVector = transform.up * Input.GetAxis("Horizontal") * rotationSpeed;
        
        vehicleRb.AddForce(forwardVector * Time.deltaTime);
        transform.Rotate(rotationVector * Time.deltaTime);
    }

}
