using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 1f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovingPlayer();
    }

    void MovingPlayer()
    {
        float deltaX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float deltaY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        if (deltaX != 0 || deltaY != 0)
        {
            animator.SetBool("isRunning", true);
            transform.GetChild(0).localScale = new Vector3(-Mathf.Sign(deltaX), 1f, 1f);
        }
        else
            animator.SetBool("isRunning", false);

        transform.Translate(new Vector2(deltaX, deltaY));
    }
}
