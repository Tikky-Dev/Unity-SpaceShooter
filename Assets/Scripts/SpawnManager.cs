using System.Collections;
using UnityEngine;

[System.Serializable]
public class Spawnable{
    public GameObject spawnablePrefab;
    [Range (0f, 100f)]
    public float chance = 100f;

    [HideInInspector]
    public double weight;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Spawnable[] prefabEnemy;
    [SerializeField]
    private Spawnable[] prefabPowerup;
    [SerializeField]
    private GameObject enemyContainer;
    private bool canSpawn = true;

    private double accumulatedWeight;
    private System.Random rand = new System.Random();

    void Start()
    {
        CalculateWeights(prefabEnemy);
        CalculateWeights(prefabPowerup);
    }

    public void OnPlayerDeath(){
        // stop spawning
        canSpawn = false;
    }

    public void StartSpawn(){
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    private void CalculateWeights(Spawnable[] prefab){
        accumulatedWeight = 0f;
        foreach (Spawnable item in prefab)
        {
            accumulatedWeight += item.chance;
            item.weight = accumulatedWeight;
        }
    }

    private int GetRandomIndex(Spawnable[] prefab){
        double r = rand.NextDouble() * accumulatedWeight;
        for (int i = 0; i < prefab.Length; i++)
        {
            if(prefab[i].weight >= r){
                return i;
            }
        }
        return 0;
    }

    // Spawn object every given time interval
    IEnumerator SpawnEnemyRoutine(){
        // while player alive spawn enemies
        while(canSpawn){
            Spawnable randomEnemy = prefabEnemy[GetRandomIndex(prefabEnemy)];

            // Instantiate enemy prefab
            Vector3 spawnPos = new Vector3(Random.Range(config.leftlimit,config.rightlimit), config.upperLimit, transform.position.z);
            GameObject enemy = Instantiate(randomEnemy.spawnablePrefab, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(enemyContainer.transform);

            // wait for given time
            yield return new WaitForSeconds(Random.Range(enemyConfig.spanRangeMin, enemyConfig.spanRangeMax)); 
        }
    }
    // Spawn object every given time interval
    IEnumerator SpawnPowerupRoutine(){
        // while player alive spawn enemies
        while(canSpawn){
            Spawnable randomPowerUp = prefabPowerup[GetRandomIndex(prefabPowerup)];

            // Instantiate enemy prefab
            Vector3 spawnPos = new Vector3(Random.Range(config.leftlimit,config.rightlimit), config.upperLimit, transform.position.z);
            Instantiate(randomPowerUp.spawnablePrefab, spawnPos, Quaternion.identity);

            // wait for given time
            yield return new WaitForSeconds(Random.Range(4f, 10f)); 
        }
    }




}
