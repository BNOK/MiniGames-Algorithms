using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public float positionModifier = 2.0f;
    private float currentTime = 0.0f;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // time resets for the sine function
        //PowerUpFloat();
       
    }

    void PowerUpFloat()
    {
        if (currentTime < 180)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
        }
        float sinevalue = Mathf.Sin(currentTime);
        Debug.Log("sine value = " + sinevalue + " || currenttime = " + currentTime);

        // floating with jump effect 
        float finalvalue = Mathf.Lerp(0, 1, Mathf.Abs(sinevalue));
        transform.position = new Vector3(transform.position.x, finalvalue, transform.position.z);
    }
}
