using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    public GameObject[] levelButtons;
    public GameObject[] LockPanel;
    public GameObject LevelsPanel;
    int Level = 0;

   

    void Start()
    {
        Time.timeScale = 1;
        LoadLevel();
        LockLevels();
    }
    void LoadLevel()
    {
        Level= 1+( PlayerPrefs.GetInt("Level"));
        Debug.Log(Level);
    }
   void LockLevels()
    {
        for (int Lock = + Level; Lock < 5; Lock++)
        {
            LockPanel[Lock].SetActive(true);
            levelButtons[Lock].GetComponent<Button>().interactable = false;
            Debug.Log("kilitlendi" + Lock);
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
    public void del()
    {
        PlayerPrefs.DeleteAll();
    }
}

