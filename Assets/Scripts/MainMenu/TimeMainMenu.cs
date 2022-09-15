using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeMainMenu : MonoBehaviour
{

    public TextMeshProUGUI textTime;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("HighScore") == "")
        {
            PlayerPrefs.SetString("HighScore", "None");
        }
        textTime.text = "Best Time: " + PlayerPrefs.GetString("HighScore");
    }
}
