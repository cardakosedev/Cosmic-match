using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    public GameObject[] levelButtons;
    public GameObject[] LockPanel;
    public GameObject LevelsPanel;
    public Slider SettingsSlider;
    int Level = 0;


    public AudioSource musicSource;

    public GameObject Settings_panel;


    void Start()
    {
        Time.timeScale = 1;

        LoadLevel();
        LockLevels();
        SettingsSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SettingsSlider.onValueChanged.AddListener(SoundControl.Instance.SetMusicVolume
        );
    }
    void LoadLevel()
    {
        Level= 1+( PlayerPrefs.GetInt("Level"));
    }
   void LockLevels()
    {
        for (int Lock = + Level; Lock < 5; Lock++)
        {
            LockPanel[Lock].SetActive(true);
            levelButtons[Lock].GetComponent<Button>().interactable = false;
        }
    }
    public void LevelPanel()
    {
        LevelsPanel.SetActive(true);
    }
    public void SelectLevel(int Level)
    {
        SceneManager.LoadScene(Level-1);
    }
    public void OpenSettingsPanel()
    {
        Settings_panel.SetActive(true);

        SettingsSlider.value =
            PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
    public void CloseSettingsPanel()
    {
        Settings_panel.SetActive(false);
    }
}

