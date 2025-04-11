
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyGO;
    public GameObject[] powerUpGO;

    public float spawnBounds = 10.0f;

    private float delayEnemyTime = 1.5f;
    private float delayPowerUpTime = 3.0f;

    private float enemySpawnRate = 6.0f;
    private float powerUpSpawnRate = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", delayEnemyTime, enemySpawnRate);

        InvokeRepeating("SpawnPowerUp", delayPowerUpTime, powerUpSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 CreateSpawnPosition(float yvalue)
    {
        Vector3 result;
        float spawnX = Random.Range(-spawnBounds, spawnBounds);
        float spawnZ = Random.Range(-spawnBounds, spawnBounds);
        result = new Vector3(spawnX, yvalue, spawnZ);
        return result;
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyGO.Length);
        Instantiate(enemyGO[randomIndex], CreateSpawnPosition(6), transform.rotation);
    }

    void SpawnPowerUp()
    {
        Debug.Log("power up spawned !! ");
        int randomIndex = Random.Range(0, powerUpGO.Length);
        Instantiate(powerUpGO[randomIndex], CreateSpawnPosition(0), transform.rotation);
    }
}
