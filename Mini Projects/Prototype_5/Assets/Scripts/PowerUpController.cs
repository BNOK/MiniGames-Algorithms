using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public float floatingSpeed = 1.0f;
    private float currentTime = 0.0f;


    public int floatFunctionSelector = 1;
    

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

        if (currentTime < 180)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
        }
        // time resets for the sine function
        switch (floatFunctionSelector)
        {
            case 1: PowerUpFloat(); 
                return;

            case 2: Floating(); 
                return;
        }
        
        
        
    }

    // has a jump effect on it
    void PowerUpFloat()
    {
        float sinevalue = Mathf.Sin(currentTime * floatingSpeed);

        float finalvalue = Mathf.Lerp(0, 1, Mathf.Abs(sinevalue));
        transform.position = new Vector3(transform.position.x, finalvalue, transform.position.z);
    }

    //float example 2
    void Floating()
    {
        currentTime += Time.deltaTime;
        float yValue = Mathf.Sin(currentTime * floatingSpeed);

        transform.position = new Vector3(transform.position.x, yValue +1, transform.position.z);
    }
}
