using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float HorizontalInput;
    public float moveSpeed = 10;
    private Vector3 currentPosition;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(HorizontalInput * Vector3.right * Time.deltaTime * moveSpeed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -19, 19), transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position,transform.rotation);
        }
    }
}
