using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;

    private void Update()
    {
        transform.position =
            new Vector3(mainPlayer.transform.position.x, mainPlayer.transform.position.y,
            -10);
    }
}
