using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("Daimond"))
        {
            PlayerPrefs.SetInt("Daimond", 0);

            PlayerPrefs.SetInt("Level", 1);
        }
        
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
