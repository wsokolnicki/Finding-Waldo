using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] GameObject pedestrianPrefab;
    [SerializeField] GameObject waldoPrefab;
    [HideInInspector] public bool isWaldo;
    [HideInInspector] public bool removed = false;

    Vector2 viewPosition;
    Vector2 viewPositionPedestrian;
    float minBoundary = -0.15f;
    float maxBoundary = 1.15f;

    public void InstantiatePedestrian(GameObject spawnPointGO, Vector2 spawnPoint)
    {
        if (isWaldo)
            pedestrianPrefab = waldoPrefab;

            GameObject pedestrian =
          Instantiate(pedestrianPrefab, spawnPoint, Quaternion.identity);
            pedestrian.transform.SetParent(spawnPointGO.transform);
    }

    private void Update()
    {
        PedetriansVisibility();
    }

    private void PedetriansVisibility()
    {
        viewPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

        if ((viewPosition.x < minBoundary || viewPosition.x > maxBoundary
            || viewPosition.y < minBoundary || viewPosition.y > maxBoundary))
        {
            if (transform.childCount == 0)
                return;
            else
                Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        else
        {
            if (gameObject.transform.childCount > 0 || removed)
                return;
            else 
                InstantiatePedestrian(gameObject, gameObject.transform.position);
        }
    }
}

#pragma warning restore 0649