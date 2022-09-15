using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{
    public void SetHighTime()
    {
        if (GameManager.gameMode == 2)
        {
            if (PlayerPrefs.GetFloat("HighScoreFloat") > FindObjectOfType<Timer>().time)
            {
                PlayerPrefs.SetString("HighScore", FindObjectOfType<Timer>().textTimer.text);
                PlayerPrefs.SetFloat("HighScoreFloat", FindObjectOfType<Timer>().time);
                GameManager.bestTime = FindObjectOfType<Timer>().textTimer.text;
            }
            else if (PlayerPrefs.GetFloat("HighScoreFloat") == 0)
            {
                Debug.Log("Pene2");
                PlayerPrefs.SetString("HighScore", FindObjectOfType<Timer>().textTimer.text);
                PlayerPrefs.SetFloat("HighScoreFloat", FindObjectOfType<Timer>().time);
            }
        }
    }
}
