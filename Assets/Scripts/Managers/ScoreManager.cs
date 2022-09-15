using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Selectable[] topStacks;
    public ParticleSystem[] particlesWin;


    private void Start()
    {

    }
    public bool HasWon()
    {
        int i = 0;
        foreach(Selectable topstack in topStacks)
        {
            i += topstack.value; 
        }
        Debug.Log(i);
        if (i >= 52)
        {
            //Victoria
            Debug.Log("Victoria");
            Win();
            return true;
        }
        else
        {
            return false;
        }
    }


    void Win()
    {
        foreach (Selectable topstack in topStacks)
        {
            topstack.enabled = false;
        }
        InvokeRepeating("WinParticles", 0.0f, 4.0f);
        AudioManager.instance.music.Stop();
        AudioManager.instance.PlaySFX2("Victory");
        Invoke("MusicBack", 6.0f);
        if (GameManager.gameMode == 2)
        {
            FindObjectOfType<SetHighScore>().SetHighTime();
            FindObjectOfType<Timer>().startTimer = false;
        }
        GameManager.gameOver = true;
    }

    void MusicBack()
    {
        AudioManager.instance.music.Play();
    }

    void WinParticles()
    {
        if (GameManager.gameMode == 1) //Classic
        {
            foreach (ParticleSystem particle in particlesWin)
            {
                particle.Play();
            }
        }
        else if (GameManager.gameMode == 2) //Timer
        {
            foreach (ParticleSystem particle in particlesWin)
            {
                particle.Play();
            }
        }
    }

}
