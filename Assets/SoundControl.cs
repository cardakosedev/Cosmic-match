using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    private static GameObject instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Slider SettingsSlider;
    float soundLevel = 0;

    public GameObject Settings_panel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);

    }
    void Start()
    {
        soundLevel = PlayerPrefs.GetFloat("MusicVolume", 1f);

        SettingsSlider.value = soundLevel;
        SetMusicVolume(soundLevel);

        SettingsSlider.onValueChanged.AddListener(SetMusicVolume);
    }
    public void SetMusicVolume(float volume)
    {
        soundLevel = volume;

        musicSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void OpenSettingsPanel()
    {
        Settings_panel.SetActive(true);

        soundLevel = PlayerPrefs.GetFloat("MusicVolume", 1f);

        SettingsSlider.value = soundLevel;
    }

}


