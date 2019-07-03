using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    Animator animator;
    float speed = 3f;
    public bool isRunning = false;
    public bool standingStill = true;
    Vector3 startPosition;
    GameObject mainPlayer;
    Camera cam;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        cam = Camera.main;
    }

    private void Update()
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


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            mainPlayer = player.gameObject;
            standingStill = false;
            isRunning = true;
        }
        else if (player.gameObject.tag == "PedestrianController")
        {
            standingStill = true;
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
