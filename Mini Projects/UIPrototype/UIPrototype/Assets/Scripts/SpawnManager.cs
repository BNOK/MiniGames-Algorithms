using System.Collections;
using UnityEngine;

/*pooling parameters 
     * using arrays since it has better performance than lists 
     * setting the poolsize to static since this class is one of a kind unlike other instances like weapons ammo
     * I am instantiating all the objects in the start method so i don't get performance issues later in the game 
     * when game speed is increased !
*/

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ballObjects;
    public float spawnRate = 1.5f;


    static private int poolSize = 100;
    private GameObject[] objectPool = new GameObject[poolSize];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePool();
        StartCoroutine(SpawnCoroutine());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int index = Random.Range(0, ballObjects.Length);
            objectPool[i] = Instantiate(ballObjects[index], Vector3.zero, Quaternion.identity);
            objectPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnObject();
        }
    }

    Vector3 SetSpawnLocation()
    {
        Vector3 result = Vector3.zero;

        float randomX = Random.Range(-5.5f, 5.5f);

        result.x = randomX;
        return result;
    }

    void SpawnObject()
    {
        Vector3 spawnLocation = SetSpawnLocation();
        int index = Random.Range(0, objectPool.Length);

        
    }
}
