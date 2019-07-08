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
    [SerializeField] [Range(0.5f, 3)] public float distanceBetweenPeople = 0.7f;
    float minDistance = 0.5f;

    [Header("Pedestrian Area")]
    [SerializeField] public int area = 20;

    Vector3 playerPosition;
    int randomX;
    int randomY;

    private void Awake()
    {
        playerPosition = FindObjectOfType<Player>().transform.position;
    }

    public void GenerateSpawnPoints()
    {
        for(int x = -area; x < area; x++)
        {
            randomX = Random.Range(-area, area);
            randomY = Random.Range(-area, area);

            for (int y = -area; y < area; y++)
            {
                Vector2 spawnPoint =  new Vector2
                    (x * Random.Range(minDistance, distanceBetweenPeople), 
                    y * Random.Range(-minDistance, -distanceBetweenPeople));
                GameObject spawnPointGO =
                    Instantiate(spawnPointPrefab, spawnPoint, Quaternion.identity);

                spawnPointGO.transform.SetParent(gameObject.transform);

                if (x == randomX && y == randomY)
                {
                    spawnPointGO.GetComponent<PedestrianManager>().isWaldo = true;
                    FindObjectOfType<ArrowPointer>().GetComponent<ArrowPointer>().waldoPosition
                        = spawnPoint;
                }
            }
        }
    }
}
