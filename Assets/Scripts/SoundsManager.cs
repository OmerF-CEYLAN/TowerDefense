using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    AudioSource audioSource;

    [SerializeField] List<AudioClip> musics;
    AudioClip currentClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        LoadVolumes();

        audioSource = GetComponent<AudioSource>();
        SelectMusic();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying == false)
        {
            SelectMusic();
        }
    }

    public void SetSoundValue(float value)
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Sounds", value);
    }

    public void SetMusicValue(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }

    void LoadVolumes()
    {
        float music = PlayerPrefs.GetFloat("Music", 1f);
        float sound = PlayerPrefs.GetFloat("Sounds", 1f);

        SetMusicValue(music);
        SetSoundValue(sound);
    }

    void SelectMusic()
    {
        AudioClip selectedClip = musics[Random.Range(0, musics.Count)];

        if(currentClip != null)
        {
            while(currentClip == selectedClip && musics.Count > 1)
            {
                selectedClip = musics[Random.Range(0, musics.Count)];
            }
            currentClip = selectedClip;
        }
        else
        {
            currentClip = selectedClip;
        }

        audioSource.clip = currentClip;
        audioSource.Play();
    }

}
