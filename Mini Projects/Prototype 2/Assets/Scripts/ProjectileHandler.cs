using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public float topBound = 50;
    public float downBound = -50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z > topBound || transform.position.z < downBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > topBound || transform.position.x < downBound)
        {
            Destroy(gameObject);
        }
    }

    

}
