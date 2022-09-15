using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuInteractions : MonoBehaviour
{

    public GameObject howToPlayMenu;
    public GameObject creditsMenu;
    public GameObject mainMenu;
    public GameObject soundSprite;
    public AudioSource backSound, clickSound;
    public Sprite soundOn, SoundOff;
    public AudioMixer audioMixer;

    private void Start()
    {
        if (GameManager.isSoundOn)
        {
            audioMixer.SetFloat("MasterVolume", -10);
            soundSprite.GetComponent<Image>().sprite = soundOn;
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", -80);
            soundSprite.GetComponent<Image>().sprite = SoundOff;
        }
    }
    public void HowToPlayPressed()
    {
        howToPlayMenu.gameObject.GetComponent<Animator>().SetBool("HowToPlay", true);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 2);
        clickSound.Play();
    }

    public void ReturnHowToPlayPressed()
    {
        howToPlayMenu.gameObject.GetComponent<Animator>().SetBool("HowToPlay", false);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 4);
        backSound.Play();
    }

    public void CreditsPressed()
    {
        creditsMenu.gameObject.GetComponent<Animator>().SetBool("Credits", true);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 1);
        clickSound.Play();
    }

    public void ReturnCreditsPressed()
    {
        creditsMenu.gameObject.GetComponent<Animator>().SetBool("Credits", false);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 3);
        backSound.Play();
    }

    public void SoundButtonPressed()
    {
        if (GameManager.isSoundOn)
        {
            backSound.Play();
            soundSprite.GetComponent<Image>().sprite = SoundOff;
            GameManager.isSoundOn = false;
            CancelInvoke("SoundOffFunction");
            Invoke("SoundOffFunction", 0.2f);
        }
        else
        {
            CancelInvoke("SoundOffFunction");
            audioMixer.SetFloat("MasterVolume", -10);
            GameManager.isSoundOn = true;
            soundSprite.GetComponent<Image>().sprite = soundOn;
            clickSound.Play();
        }
    }

    void SoundOffFunction()
    {
        audioMixer.SetFloat("MasterVolume", -80);
    }


}
