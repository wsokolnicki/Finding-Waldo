using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class ArrowPointer : MonoBehaviour
{
    [HideInInspector] public Vector3 waldoPosition;
    [SerializeField] Transform arrow;

    void Update()
    {
        Vector2 waldoDirection = (waldoPosition - transform.parent.position).normalized;
        float angle = Vector2.Angle(transform.right, waldoDirection);

        int plusOrMinus = (transform.parent.position.y > waldoPosition.y) ? -1 : 1;

        arrow.eulerAngles = new Vector3(0, 0, plusOrMinus * angle);
    }
}

#pragma warning restore 0649