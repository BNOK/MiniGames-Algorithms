using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    private Vector3 Location;

    private float currentTime = 0.0f;

    public TextMeshProUGUI text;
    public GameObject gameCanvas;
    public GameObject playerCamera;
    public Vector3 offset;
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = Vector3.one;
        
        Material material = Renderer.material;
        
        material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);
    }
    
    void FixedUpdate()
    {
        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);
        SinMove();
    }

    private void LateUpdate()
    {
        //playerCamera.transform.position = this.transform.position + offset;
        //gameCanvas.transform.LookAt(playerCamera.transform, Vector3.up);
        
    }

    public void SinMove()
    {
        currentTime += Time.deltaTime % 90;
        float value = Mathf.Sin(currentTime) /10.0f;
        Debug.Log(String.Format("current Time = {0} || value = {1}",currentTime,value));

        //gameCanvas.transform.position = this.transform.position + Vector3.up * 3;
        text.text = String.Format("current Time = {0:F2} || value = {1:F2}", currentTime, value);


        Location = new Vector3(value, 0, 0);
        transform.Translate(Location);

        transform.localScale = Vector3.one + new Vector3(value,value,value) * 2;
    }
}
