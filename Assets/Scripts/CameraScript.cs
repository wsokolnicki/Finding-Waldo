using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;

    private void Update()
    {
        transform.position =
            new Vector3(mainPlayer.transform.position.x,
            mainPlayer.transform.position.y,-10);
    }
}

#pragma warning restore 0649