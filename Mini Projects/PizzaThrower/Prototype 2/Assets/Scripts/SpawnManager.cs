using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Animals;
    int xOffset = 20;

    // spawn setting 
    public float startDelay = 2.0f;
    public float spawnRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, Animals.Length);
        Vector3 offset = new Vector3(Random.Range(-xOffset, xOffset), 0, 0);

        GameObject animalHolder = Instantiate(Animals[animalIndex], transform.position + offset, transform.rotation);
        animalHolder.AddComponent<ProjectileController>();
        animalHolder.AddComponent<ProjectileHandler>();
    }
}
