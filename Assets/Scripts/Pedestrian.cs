using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class Pedestrian : LayerOrder
{
    Camera cam;
    Animator animator;
    Rigidbody2D rigidBody;
    Vector3 startPosition;
    GameObject mainPlayer;

    [Header("Sprites for pedestrian")]
    [SerializeField] Sprite[] hats;
    [SerializeField] Sprite[] bodies;
    [SerializeField] Sprite[] legs;

    [Header("Pedestrian bodyparts")]
    [SerializeField] SpriteRenderer hat;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer leg;
    [SerializeField] SpriteRenderer leg2;

    SpriteRenderer[] bodyParts;

    float walkAwaySpeed;
    int goBackSpeed = 5;
    int explosionDrag = 5;
    int explosionForce = 15;

    public bool isRunning = true;
    public bool explosion = false;
    public bool standingStill = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        bodyParts = new SpriteRenderer[] { hat, body, leg, leg2 };
        SetPedestrianLook();
    }

    void SetPedestrianLook()
    {
        int whichHat = Random.Range(0, hats.Length);
        int whichBody = Random.Range(0, bodies.Length);
        int whichLeg = Random.Range(0, legs.Length);

        hat.sprite = hats[whichHat];
        body.sprite = bodies[whichBody];
        leg.sprite = legs[whichLeg];
        leg2.sprite = legs[whichLeg];
    }

    private void Update()
    {
        LayerOrdering(bodyParts);

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
                Vector2.MoveTowards(transform.position, startPosition, goBackSpeed * Time.deltaTime);

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
            transform.Translate(moveDirection * walkAwaySpeed * Time.deltaTime);
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
            walkAwaySpeed = mainPlayer.GetComponent<Player>().movementSpeed * 1.25f;
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

#pragma warning restore 0649
