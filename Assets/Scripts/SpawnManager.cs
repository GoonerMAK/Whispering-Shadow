using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector2 spawnPos; //spawn position
    public GameObject[] goodVillagerPrefabs;
    public GameObject[] badVillagerPrefabs;
    public int badVillagerIndex;
    public int goodVillagerIndex;
    public int goodVillagerCount;
    public int badVillagerCount;
    public Vector2[] spawnCoordinates; 
    public int spawnCoordinatesIndex;

    // Start is called before the first frame update
    void Start()
    {
        goodVillagerCount = Random.Range(20, 25); //selects number of good villagers to spawn
        badVillagerCount = Random.Range(14, 16); //selects number of bad vilagers to spawn 

        for (int i = 0; i < goodVillagerCount; i++)
        {
            SpawnGoodVillager();
        }
        for (int i = 0; i < badVillagerCount; i++)
        {
            SpawnBadVillager();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGoodVillager()
    {
        goodVillagerIndex = Random.Range(0, goodVillagerPrefabs.Length);
        spawnCoordinatesIndex = Random.Range(0, spawnCoordinates.Length);
        spawnPos = spawnCoordinates[spawnCoordinatesIndex];

        //instantiate based on index of randomly generated index
        Instantiate(goodVillagerPrefabs[goodVillagerIndex], spawnPos, goodVillagerPrefabs[goodVillagerIndex].transform.rotation);
    }

    public void SpawnBadVillager()
    {
        badVillagerIndex = Random.Range(0, badVillagerPrefabs.Length);
        spawnCoordinatesIndex = Random.Range(0, spawnCoordinates.Length);
        spawnPos = spawnCoordinates[spawnCoordinatesIndex];

        //instantiate based on index of randomly generated index
        Instantiate(badVillagerPrefabs[badVillagerIndex], spawnPos, badVillagerPrefabs[badVillagerIndex].transform.rotation);
    }
}
