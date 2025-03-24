using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject healthBar;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }
    public void DecreaseHealthBar(float value)
    {
        healthBar.transform.localScale = new Vector3();
    }

    public void IncreaseHealthBar()
    {

    }
}
