using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("---Car Options--")]
    public GameObject[] Cars;
    int ActiveCarIndex;
    public int CarParkGoal;
    public int ParkedCarsCounter;

    [Header("---UI Options--")]
    public GameObject[] CarUIImages;
    public Sprite CarSuccessSprite;
    public TextMeshProUGUI[] UITexts;
    public GameObject[] Panels;
    public GameObject[] WatchReviveButtons;

    [Header("---Platform Options--")]
    public GameObject Platform1;
    public GameObject Platform2;
    public float[] RotateSpeeds;
    public bool TurnPlatform;
    public bool VerticalPlatform;


    [Header("---Level Options--")]
    public int DaimondCount;
    public ParticleSystem CrushEffect;
    public AudioSource[] auidioSources;
    bool TouchLock;

    void Start()
    {
        TouchLock= true;
        TurnPlatform = true;
        ParkedCarsCounter = 0;
        CheckDefaultValues();
        Cars[ActiveCarIndex].SetActive(true);
        ActiveteCarUIImages();
    }
    public void SendCar()
    {


        if (ActiveCarIndex < CarParkGoal)
        {
            Cars[ActiveCarIndex].SetActive(true);
        }
        else if (ParkedCarsCounter == CarParkGoal)
        {
            Win();
        }


    }
    public void ActiveteCarUIImages()
    {
        for (int i = 0; i < CarParkGoal; i++)
        {
            CarUIImages[i].SetActive(true);
        }
    }
    public void UpdateCarUIImages()
    {
        CarUIImages[ActiveCarIndex - 1].GetComponent<Image>().sprite = CarSuccessSprite;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount==1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Began)
            {
                if (TouchLock)
                {
                    Panels[0].SetActive(false);
                    Panels[3].SetActive(true);
                    TouchLock = false;
                }
                else
                {
                    if (Time.timeScale != 0)
                    {
                        if (Cars[ActiveCarIndex].GetComponent<car>().StartPointCheck)
                        {
                            if (Input.GetKeyDown(KeyCode.G))
                            {
                                Cars[ActiveCarIndex].GetComponent<car>().Foward = true;
                                ActiveCarIndex++;
                            }
                        }

                        if (TurnPlatform)
                        {
                            Platform1.transform.Rotate(new Vector3(0, 0, RotateSpeeds[0]), Space.Self);
                            if (Platform2 != null)
                                Platform2.transform.Rotate(new Vector3(0, 0, RotateSpeeds[1]), Space.Self);

                        }

                    }
                }
            }
        }

        
    }

    public void Lose()
    {
        PlayerPrefs.SetInt("Daimond", PlayerPrefs.GetInt("Daimond") + DaimondCount);
        UITexts[6].text = PlayerPrefs.GetInt("Daimond").ToString();
        UITexts[7].text = SceneManager.GetActiveScene().name;
        UITexts[8].text = (ParkedCarsCounter).ToString();
        UITexts[9].text = DaimondCount.ToString();
        auidioSources[1].Play();
        auidioSources[3].Play();
        Panels[3].SetActive(false);
        Panels[1].SetActive(true);

        Invoke("LoseActiveButton", 2f);
        TurnPlatform = false;

    }
    void Win()
    {
        PlayerPrefs.SetInt("Daimond", PlayerPrefs.GetInt("Daimond") + DaimondCount);
        UITexts[2].text = PlayerPrefs.GetInt("Daimond").ToString();
        UITexts[5].text = SceneManager.GetActiveScene().name;
        UITexts[3].text = (ParkedCarsCounter).ToString();
        UITexts[4].text = DaimondCount.ToString();
        auidioSources[2].Play();
        Panels[3].SetActive(false);
        Panels[2].SetActive(true);

        Invoke("WinActiveButton", 2f);

    }
    void LoseActiveButton()
    {
        WatchReviveButtons[0].SetActive(true);
    }
    void WinActiveButton()
    {
        WatchReviveButtons[1].SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        
    }
    void CheckDefaultValues()
    {
      
        UITexts[0].text = PlayerPrefs.GetInt("Daimond").ToString();
        UITexts[1].text = SceneManager.GetActiveScene().name;

    }
    public void MultipyDaimond(int multiply)
    {

    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
