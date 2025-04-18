﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    private float currentTime = 0.0f;

    public float speed = 500;

    public Canvas PlayerUI;
    private Slider powerUpSlider;
    public GameObject smokeEffect;
    
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        if(playerRb == null)
        {
            playerRb = gameObject.AddComponent<Rigidbody>();
        }
        focalPoint = GameObject.Find("FocalPoint");
        powerupIndicator.SetActive(false);

        smokeEffect = Instantiate(smokeEffect, transform.position, transform.rotation);

        PlayerUI = GameObject.FindAnyObjectByType<Canvas>();
        powerUpSlider = PlayerUI.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        BoostMovement(verticalInput);
        focalPoint.transform.position = transform.position;

        UpdatePowerUpSlider();
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        smokeEffect.transform.position = transform.position + new Vector3(0, 10.0f, 0);
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            //StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    //IEnumerator PowerupCooldown()
    //{
    //    yield return new WaitForSeconds(powerUpDuration);
    //    hasPowerup = false;
    //    powerupIndicator.SetActive(false);
    //}

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

    void BoostMovement(float verticalInput)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("space pressed");
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * 10.0f * Time.deltaTime);
            smokeEffect.SetActive(true);
            smokeEffect.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            smokeEffect.SetActive(false);
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        }
    }

    void UpdatePowerUpSlider()
    {
        if (hasPowerup)
        {
            powerUpSlider.value = currentTime - Time.deltaTime;
            if(currentTime <= 0)
            {
                hasPowerup = false;
                powerUpSlider.gameObject.SetActive(false);
                powerupIndicator.SetActive(false);
                currentTime = 0;
            }
        }
    }
}
