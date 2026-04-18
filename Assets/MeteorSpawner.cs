using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Префабы метеоритов")]
    public GameObject smallMeteor;
    public GameObject mediumMeteor;
    public GameObject largeMeteor;

    [Header("Настройки спавна")]
    public float spawnRadius = 60f;   
    public float spawnHeight = 40f;  
    public float spawnInterval = 1.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            SpawnRandomMeteor();
            timer = 0f;
        }
    }

    void SpawnRandomMeteor()
    {

        int randomTypeZone = Random.Range(0, 3);

        if (randomTypeZone < 1)
        {
            float randomAngle = Random.Range(0f, 360f);
            float radians = randomAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * spawnRadius;
            float z = Mathf.Sin(radians) * spawnRadius;

            Vector3 spawnPosition = new Vector3(x, spawnHeight, z);

            int randomType = Random.Range(0, 3);
            GameObject meteorToSpawn = smallMeteor;

            if (randomType == 1) meteorToSpawn = mediumMeteor;
            if (randomType == 2) meteorToSpawn = largeMeteor;

            Instantiate(meteorToSpawn, spawnPosition, Quaternion.identity);
        }
        
        if (randomTypeZone == 2)
        {
            float x = Random.Range(-2f, 2f);
            float z = Random.Range(-2f, 2f);

            Vector3 spawnPosition = new Vector3(x, spawnHeight, z); 
            int randomType = Random.Range(0, 3);
            GameObject meteorToSpawn = smallMeteor;

            if (randomType == 1) meteorToSpawn = mediumMeteor;
            if (randomType == 2) meteorToSpawn = largeMeteor;

            Instantiate(meteorToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}