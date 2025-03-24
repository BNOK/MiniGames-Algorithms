using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public float delayRate = 1.5f;
    public float spawnRate = 2.0f;

    private Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", delayRate, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        offset = transform.position;
        offset.y = Random.Range(0,4);
        Instantiate(obstaclePrefabs[index], offset, transform.rotation);
    }
}
