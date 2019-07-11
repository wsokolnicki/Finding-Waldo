using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waldo : LayerOrder
{
    SpriteRenderer[] bodyParts;

    private void Start()
    {
        bodyParts =GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        LayerOrdering(bodyParts);
    }
    
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            gameObject.tag = "Waldo";
            gameObject.layer = 11;
            FindObjectOfType<Player>().transform.GetChild(4)
                .GetChild(0).gameObject.SetActive(false);
            StartCoroutine(DisableAndAcitvatePlayer(player));
            StartCoroutine(DisableWaldosCollider());
        }
    }

    IEnumerator DisableWaldosCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    IEnumerator DisableAndAcitvatePlayer(Collider2D player)
    {
        GameSetup gamesetup = FindObjectOfType<GameSetup>();

        player.enabled = false;
        player.gameObject.transform.GetChild(3).GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<CircleCollider2D>().radius = 5f;
        yield return new WaitForSeconds(2f);
        gamesetup.gameplay = false;
        gamesetup.GoBack();
        gamesetup.timeUI.SetActive(true);
    }
}
