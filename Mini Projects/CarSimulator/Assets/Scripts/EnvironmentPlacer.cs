using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPlacer : MonoBehaviour
{
    public GameObject[] roads;
    // Start is called before the first frame update
    void Start()
    {
        roads = GameObject.FindGameObjectsWithTag("Road");
        if (roads.Length > 0)
        {
            for (int i = 0; i < roads.Length; i++)
            {
                Debug.Log(roads[i].transform);
                roads[i].transform.position = new Vector3(i * 20, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
