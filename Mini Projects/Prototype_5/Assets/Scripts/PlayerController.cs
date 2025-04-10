using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody ballRb;
    private bool hasPowerUp = false;
    private GameObject powerUpIndicatorHolder;


    public GameObject PowerUpIndicator;
    public float powerUpStrength = 1000.0f;
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

        InitializePowerUpIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        
        ballRb.AddForce(focalPoint.transform.forward * VerticalInput * ballSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicatorHolder.SetActive(true);

            StartCoroutine(PowerUpCountDownRoutine());
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();
            enemyRB.AddForce(pushDirection * powerUpStrength, ForceMode.Impulse);
            TurnPowerUpOff();
        }
    }

    private IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        TurnPowerUpOff();
    }

    private void InitializePowerUpIndicator()
    {
        powerUpIndicatorHolder = Instantiate(PowerUpIndicator, transform.position, Quaternion.identity);
        //powerUpIndicatorHolder.transform.SetParent(transform, true);
        powerUpIndicatorHolder.transform.position =  transform.position;
        powerUpIndicatorHolder.SetActive(false);
        powerUpIndicatorHolder.GetComponent<PowerUpIndicator>().parent = transform.gameObject;
    }

    private void TurnPowerUpOff()
    {
        hasPowerUp = false;
        powerUpIndicatorHolder.SetActive(false);
    }
}
