using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] GameObject pedestrianPrefab;
    [SerializeField] GameObject waldoPrefab;
    [HideInInspector] public bool isWaldo;

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
        Vector2 viewPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

        if (viewPosition.x < -0.15f || viewPosition.x > 1.15f || viewPosition.y < -0.15f || viewPosition.y > 1.15f)
        {
            if (transform.childCount == 0)
                return;
            else
                Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        else
        {
            if (gameObject.transform.childCount > 0)
                return;
            else
                InstantiatePedestrian(gameObject, gameObject.transform.position);
        }
    }
}
