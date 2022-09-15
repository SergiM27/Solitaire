using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] public AudioFile[] audioFiles;
    public AudioSource music;
    public AudioSource sfx;
    public AudioSource sfx2;
    [Range(0, 1)] public float OverallVolume_Music;
    [Range(0, 1)] public float OverallVolume_SFX;
    [Range(0, 1)] public float OverallVolume_SFX2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlayMusic("Music");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetVolume();
    }
    public void SetVolume()
    {
        music.volume = instance.OverallVolume_Music;
        sfx.volume = instance.OverallVolume_SFX;
        sfx2.volume = instance.OverallVolume_SFX;
    }



    public void PlayMusic(string audioName)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {
                music.clip = file.Clip;
                music.volume = instance.OverallVolume_Music;
                music.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that does not exist: " + audioName);
    }



    public void PlaySFX(string audioName)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {
                sfx.clip = file.Clip;
                sfx.volume = instance.OverallVolume_SFX;
                sfx.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that not exist: " + audioName);
    }

    public void PlaySFX2(string audioName)
    {
        var file = GetFileByName(audioName);

        if (file != null)
        {
            if (file.Clip != null)
            {
                sfx2.clip = file.Clip;
                sfx2.volume = instance.OverallVolume_SFX;
                sfx2.Play();
            }
            else Debug.LogError("This AudioFile does not have any AudioClip: " + audioName);
        }
        else Debug.LogError("Trying to play a sound that not exist: " + audioName);
    }

    private AudioFile GetFileByName(string soundName)
    {
        var file = audioFiles.First(x => x.Name == soundName);
        if (file != null)
        {
            return file;
        }
        else Debug.LogError("Trying to play a sound that not exist: " + soundName);

        return null;

    }

    public void VolumeDown()
    {
        StartCoroutine(MusicVolumeDown());
    }

    private IEnumerator MusicVolumeDown()
    {
        float volumeSpeed = 0.005f;
        for (float i = music.volume; i >= music.volume / 2.0f; i -= volumeSpeed)
        {
            music.volume = i;
            yield return null;
        }
        yield return null;
    }
}
