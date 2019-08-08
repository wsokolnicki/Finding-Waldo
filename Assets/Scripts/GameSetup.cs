using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma warning disable 0649

public class GameSetup : MonoBehaviour
{
    [Header("UI Cache")]
    [SerializeField] Slider crowdSizeSlider;
    [SerializeField] Slider crowdDensitySlider;
    [SerializeField] Slider playerSpeedSlider;

    [SerializeField] Text crowdSizeValue;
    [SerializeField] Text playerSpeedValue;

    [SerializeField] Canvas canvasWithButtons;

    [SerializeField] Text timeText;
 
    [Header("Scripts Cache")]
    [SerializeField] Player player;
    [SerializeField] SpawnPointsManager crowd;
    [SerializeField] public GameObject timeUI;

    [Header("")]
    [SerializeField] Transform pedestrians;

    public float time;
    int timeAfterCheatOn = 60;
    public bool gameplay = false;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameplay)
                GoBack();
            else
                Application.Quit();  
        }

        if (Input.GetKeyUp(KeyCode.F))
            Enable_DisableCheat(true);

        crowdSizeValue.text = crowdSizeSlider.value.ToString();
        playerSpeedValue.text = playerSpeedSlider.value.ToString();

        crowd.distanceBetweenPeople = crowdDensitySlider.value;
        crowd.area = (int)crowdSizeSlider.value;
        player.movementSpeed = playerSpeedSlider.value;

        if (gameplay)
        {
            time += Time.deltaTime;
            if (time >= timeAfterCheatOn)
                Enable_DisableCheat(true);
        }
        
        timeText.text = time.ToString("f2");
    }

    public void StartButton()
    {
        crowd.GenerateSpawnPoints();
        canvasWithButtons.gameObject.SetActive(false);
        player.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        player.transform.GetChild(3).GetComponent<CircleCollider2D>().enabled = true;
        gameplay = true;
        time = 0;
    }
    public void GoBack()
    {
        for (int i = 0; i < pedestrians.childCount; i++)
            Destroy(pedestrians.GetChild(i).gameObject);
        canvasWithButtons.gameObject.SetActive(true);
        gameplay = false;
        FindObjectOfType<Player>().gameObject.transform.position = Vector2.zero;
        Enable_DisableCheat(false);
    }

    void Enable_DisableCheat(bool activated)
    {
        FindObjectOfType<Player>().transform.GetChild(4)
            .GetChild(0).gameObject.SetActive(activated);
    }
}

#pragma warning restore 0649