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
        InvokeRepeating("SpawnRandomAnimalX", startDelay, spawnRate);
        InvokeRepeating("SpawnRandomAnimalZ", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimalX()
    {
        // set upper or lower side to spawn from 
        float zValue = (Random.Range(0, 2) == 0) ? 20 : -20;

        // choose animal and set X offset to spawn from
        int animalIndex = Random.Range(0, Animals.Length);
        Vector3 offset = transform.position + new Vector3(Random.Range(-xOffset, xOffset), 0, zValue);


        //spawn and add necessary components
        GameObject animalHolder = Instantiate(Animals[animalIndex], offset, AnimalRotationZ(zValue),transform);

        animalHolder.AddComponent<ProjectileController>();
        animalHolder.AddComponent<ProjectileHandler>();
    }

    void SpawnRandomAnimalZ()
    {
        //set right or left side from where it will spawn
        float xValue = (Random.Range(0, 2) == 0) ? 20 : -20;

        //choose animal and set Z offset to spawn from
        int animalIndex = Random.Range(0, Animals.Length);
        Vector3 offset = transform.position + new Vector3(xValue, 0, Random.Range(-xOffset, xOffset));

        GameObject animalHolder = Instantiate(Animals[animalIndex], offset, AnimalRotationX(xValue));
        animalHolder.AddComponent<ProjectileController>();
        animalHolder.AddComponent<ProjectileHandler>();
    }

    Quaternion AnimalRotationZ(float value)
    {
        //set rotation accordingly
        Quaternion result = (value > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        return result;
    }

    Quaternion AnimalRotationX(float value)
    {
        //set rotation accordingly
        Quaternion result = (value > 0) ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
        return result;
    }
}
