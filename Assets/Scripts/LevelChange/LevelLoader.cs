using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void QuitGame()
    {
        Application.Quit();
    }


    public void PlayPressClassic()
    {
        StartCoroutine(LoadLevel_NormalTransition(1));
        AudioManager.instance.PlaySFX("Button");
        GameManager.gameMode = 1;
    }

    public void PlayPressTime()
    {
        GameManager.gameMode = 2;
        StartCoroutine(LoadLevel_NormalTransition(1));
        AudioManager.instance.PlaySFX("Button");
    }

    public void RePlayPress()
    {
        StartCoroutine(LoadLevel_NormalTransition(1));
        AudioManager.instance.PlaySFX("Button");
    }


    public void BackToMenuPress()
    {
        StartCoroutine(LoadLevel_NormalTransition(0));
        AudioManager.instance.PlaySFX("Button");
    }

    IEnumerator LoadLevel_NormalTransition(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
