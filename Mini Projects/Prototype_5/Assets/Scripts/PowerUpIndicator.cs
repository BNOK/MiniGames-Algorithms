using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public GameObject parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, -0.5f, 0) + parent.transform.position;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
