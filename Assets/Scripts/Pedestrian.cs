using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    Animator animator;
    float speed;
    public bool isRunning = true;
    public bool standingStill = false;
    Vector3 startPosition;
    GameObject mainPlayer;
    Camera cam;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        startPosition = transform.position;
    }

    private void FixedUpdate()
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
            speed = mainPlayer.GetComponent<Player>().movementSpeed * 1.5f;
            standingStill = false;
            isRunning = true;
        }
        //else if (player.gameObject.tag == "PedestrianController")
        //{
        //    standingStill = true;
        //}
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
