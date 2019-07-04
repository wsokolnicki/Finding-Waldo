using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    [Header("UI Cache")]
    [SerializeField] Slider crowdSizeSlider;
    [SerializeField] Slider crowdDensitySlider;
    [SerializeField] Slider playerSpeedSlider;

    [SerializeField] Text crowdSizeValue;
    [SerializeField] Text playerSpeedValue;

    [SerializeField] Canvas canvasWithButtons;
 
    [Header("Scripts Cache")]
    [SerializeField] Player player;
    [SerializeField] SpawnPointsManager crowd;

    [Header("")]
    [SerializeField] Transform pedestrians;

    bool gameplay = false;

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
            EnableCheat();

        crowdSizeValue.text = crowdSizeSlider.value.ToString();
        playerSpeedValue.text = playerSpeedSlider.value.ToString();

        crowd.distanceBetweenPeople = crowdDensitySlider.value;
        crowd.area = (int)crowdSizeSlider.value;
        player.movementSpeed = playerSpeedSlider.value;
    }

    public void StartButton()
    {
        crowd.GenerateSpawnPoints();
        canvasWithButtons.gameObject.SetActive(false);
        gameplay = true;
    }

    void GoBack()
    {
        for (int i = 0; i < pedestrians.childCount; i++)
            Destroy(pedestrians.GetChild(i).gameObject);
        canvasWithButtons.gameObject.SetActive(true);
        gameplay = false;
        FindObjectOfType<Player>().gameObject.transform.position = Vector2.zero;
    }

    void EnableCheat()
    {
        FindObjectOfType<Player>().transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
    }
}
