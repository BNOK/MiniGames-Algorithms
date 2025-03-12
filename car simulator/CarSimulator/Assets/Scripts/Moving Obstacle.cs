using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float obstaclespeed =0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 deltaposition = transform.forward * Time.deltaTime * obstaclespeed;
        transform.position = Vector3.Lerp(transform.position, transform.position - deltaposition, Time.deltaTime);
    }
}
