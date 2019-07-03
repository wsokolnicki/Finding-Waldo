using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPointsManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] public GameObject pedestrianPrefab;
    [SerializeField] GameObject spawnPointPrefab;

    [Header("Pedestrian Density")]
    [SerializeField] [Range(0.5f, 3)] float distanceBetweenPeople = 0.7f;
    float minDistance = 0.5f;

    [Header("Pedestrian Area")]
    [SerializeField] public int area = 20;


    Vector3 playerPosition;
    [HideInInspector] public List<GameObject> pedestrianList;


    private void Awake()
    {
        playerPosition = FindObjectOfType<Player>().transform.position;
    }

    void Start()
    {
        GenerateSpawnPoints();
    }

    private void GenerateSpawnPoints()
    {
        for(int x = -area; x < area; x++)
        {
            for (int y = -area; y < area; y++)
            {
                Vector2 spawnPoint =  new Vector2
                    (x * Random.Range(minDistance, distanceBetweenPeople), 
                    y * Random.Range(-minDistance, -distanceBetweenPeople));
                GameObject spawnPointGO =
                    Instantiate(spawnPointPrefab, spawnPoint, Quaternion.identity);

                spawnPointGO.transform.SetParent(gameObject.transform);
            }
        }
    }
}
