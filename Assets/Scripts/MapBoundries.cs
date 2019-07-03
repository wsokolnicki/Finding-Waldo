using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundries : MonoBehaviour
{
    [SerializeField] BoxCollider2D top;
    [SerializeField] BoxCollider2D bottom;
    [SerializeField] BoxCollider2D left;
    [SerializeField] BoxCollider2D right;
    float multiplayer = 1.5f;
    float value;

    private void Start()
    {
        value = FindObjectOfType<SpawnPointsManager>().GetComponent<SpawnPointsManager>().area;

        top.size = new Vector2(3* value, 1f);
        bottom.size = top.size;
        left.size = new Vector2(1f, 3 * value);
        right.size = left.size;

        top.offset = new Vector2(0, value * multiplayer);
        bottom.offset = new Vector2(0, -value * multiplayer);
        left.offset = new Vector2(-value * multiplayer, 0);
        right.offset = new Vector2(value * multiplayer, 0);
    }
}
