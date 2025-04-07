using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody ballRb;
    private bool hasPowerUp = false;

    public GameObject focalPoint;
    public float ballSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(focalPoint == null)
        {
            focalPoint = GameObject.FindFirstObjectByType<FocalPoint>().gameObject;
        }
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        
        ballRb.AddForce(focalPoint.transform.forward * VerticalInput * ballSpeed * Time.deltaTime);
    }
}
