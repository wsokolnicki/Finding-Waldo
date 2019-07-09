using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    Animator animator;
    float speed;
    int explosionForce = 15;
    int explosionDrag = 5;
    public bool isRunning = true;
    public bool standingStill = false;
    public bool explosion = false;
    Vector3 startPosition;
    GameObject mainPlayer;
    Camera cam;

    Rigidbody2D rigidBody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (explosion)
             return;
        PedestrianMovement();
    }

    private void PedestrianMovement()
    {
        if (standingStill)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        if (!isRunning && !standingStill)
        {
            animator.SetBool("isRunning", true);
            transform.position =
                Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            transform.localScale = new Vector3
                (Mathf.Sign((transform.position - startPosition).normalized.x), 1f, 1f);

            standingStill = (transform.position == startPosition) ? true : false;
        }
        else
        {
            animator.SetBool("isRunning", true);
            Vector2 moveDirection = transform.position - mainPlayer.transform.position;
            transform.localScale = new Vector3
                (-Mathf.Sign((moveDirection).normalized.x), 1f, 1f);
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }

    void Explosion(Vector3 waldoPosition)
    {
        if (transform.position.x < waldoPosition.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetBool("boomLeft", true);
        }
        else
            animator.SetBool("boomRight", true);

        Vector3 fallDirection = (transform.position - waldoPosition).normalized;
        rigidBody.AddForce(fallDirection * explosionForce, ForceMode2D.Impulse);
        rigidBody.drag = explosionDrag;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Waldo")
        {
            explosion = true;
            gameObject.transform.parent.GetComponent<PedestrianManager>().removed = true;
            Vector3 waldoPosition = player.transform.position;
            Explosion(waldoPosition);
        }

        else if (player.gameObject.tag == "Player")
        {
            mainPlayer = player.gameObject;
            speed = mainPlayer.GetComponent<Player>().movementSpeed * 1.5f;
            standingStill = false;
            isRunning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            isRunning = false;
            standingStill = true;
        }
        else
        {
            standingStill = false;
        }
    }
}
