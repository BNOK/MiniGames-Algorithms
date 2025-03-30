using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyGO;

    public float spawnBounds = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyGO, CreateSpawnPosition(), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 CreateSpawnPosition()
    {
        Vector3 result;
        float spawnX = Random.Range(-spawnBounds, spawnBounds);
        float spawnZ = Random.Range(-spawnBounds, spawnBounds);
        result = new Vector3(spawnX, 0, spawnZ);
        return result;
    }
}
