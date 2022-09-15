using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    public bool startTimer;
    public float time;

    private void Start()
    {
        textTimer.text = "00:00";
        startTimer = false;
    }

    void Update()
    {
        if (startTimer)
        {
            float t = time = time + Time.deltaTime;
            string minutes = Mathf.Floor(t / 60).ToString("00");
            string seconds = ((int)t % 60).ToString("00");
            textTimer.text = minutes + ":" + seconds;
        }

    }
}