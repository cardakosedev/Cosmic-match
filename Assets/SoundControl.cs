using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Instance;

    public AudioSource musicSource;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (volume == 1f) 
        {
            Debug.Log(1);
            SetMusicVolume(0.04f);

        }
        else
            SetMusicVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}