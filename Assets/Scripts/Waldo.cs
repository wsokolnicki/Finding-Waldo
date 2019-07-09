using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waldo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            gameObject.tag = "Waldo";
            gameObject.layer = 11;
            player.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
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
        player.enabled = false;
        player.gameObject.transform.GetChild(4).GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<CircleCollider2D>().radius = 5f;
        yield return new WaitForSeconds(0.5f);
        //player.enabled = true;
        //player.gameObject.transform.GetChild(4).GetComponent<CircleCollider2D>().enabled = true;

    }
}
