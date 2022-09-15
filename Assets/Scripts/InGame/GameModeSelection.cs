using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject timer;
    void Start()
    {
        if (GameManager.gameMode == 1) //Classic
        {
            timer.gameObject.SetActive(false);
        }
        else if (GameManager.gameMode == 2) //Timer
        {
            Invoke("TimerStart",1.0f);
        }
    }

    public void TimerStart()
    {
        timer.GetComponent<Timer>().startTimer = true;
    }

}
