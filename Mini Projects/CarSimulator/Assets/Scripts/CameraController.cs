using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject parentPlayer;
    Rigidbody parentRb;

    [Range(10,50)]
    public int minDistance = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        parentPlayer = transform.parent.gameObject;
        parentRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
