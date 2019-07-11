using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrder : MonoBehaviour
{
    public void LayerOrdering(SpriteRenderer[] bodyParts)
    {
        float onScreenPositionY =
            Camera.main.WorldToViewportPoint(transform.position).y;

        transform.position = new Vector3
                (transform.position.x, transform.position.y, onScreenPositionY);
    }
}
