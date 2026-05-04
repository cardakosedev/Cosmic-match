using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created




    GameObject SelectedObject;
    GameObject FirstButon;
    public Sprite SpriteDefault;
    public AudioSource[] audioSources;
    public GameObject[] Buttons;
    public GameObject[] Panels;
    public Slider TimeSlider;
    public TMP_Text scoreText;
    public Sprite[] pairSprites;
    public GameObject Pool;
    public GameObject Grid;
    bool Create = true;

    int FirstSelect = 0;
    public int Level = 0;
    float[] TimesLevels = { 30f, 50f, 70f, 80f, 110f };
    float LevelTime ;
    bool LevelTimeStoper = false;
    int Point = 0;
    int[] GoalLevels = {4,6,6,6,6};//121518
    int Corrects = 0;
    int Object=0 ;
    int createdObject = 0;


    void Start()
    {
        TimeSlider.maxValue = TimesLevels[Level - 1];
        Object = Pool.transform.childCount;
        LevelTimeStoper=true;
        Time.timeScale = 1;
        LevelTime = TimesLevels[Level - 1];
        StartCoroutine(CreateButtons());

    }
    private void Update()
    {
        if (LevelTime >1 && LevelTimeStoper)
        {
            LevelTime -= Time.deltaTime;
            TimeSlider.value= LevelTime;
        }
        else
            Lose();
        if (GoalLevels[Level - 1] == Corrects){
            Win();
        }
       
    }

    public void GetObject(GameObject Object)
    {
        SelectedObject= Object;
        SelectedObject.GetComponent<Image>().sprite = SelectedObject.GetComponentInChildren<SpriteRenderer>().sprite;
        SelectedObject.GetComponent<Image>().raycastTarget = false;
        audioSources[0].Play();
    }
    
    
    
    
    public void ControlSelectedItems(int SelectedItem)
    {
        control(SelectedItem);
    }

    void ControlButtons(bool st)
    {
        foreach (var item in Buttons)
        {
            if (item == null) continue;

            else if (item != null)
            {
                item.GetComponent<Image>().raycastTarget = st;
            }
        }
    }
    void control(int SelectedNumber)
    {

        if (FirstSelect == 0)
        {
            FirstSelect = SelectedNumber;
            FirstButon = SelectedObject;
        }
        else
        {
            
            StartCoroutine(Controled(SelectedNumber));
        }
    }
    public void UpdateScore()
    {
        
    }

    void Win()
    {
        if (Level == 5)
        {
            Panels[2].SetActive(true); Debug.Log("bitirdin");
        }
        else
            Panels[0].SetActive(true);
        Time.timeScale = 0;
    }
    void Lose()
    {
        Panels[1].SetActive(true);
        Time.timeScale = 0;


    }
    public void Pause()
    {
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void MainMenu()
    {
        Debug.Log("MainMenu Clicked");
        SceneManager.LoadScene(5);
        Time.timeScale = 1;

    }
    public void Again()
    {
        SceneManager.LoadScene(Level-1);
    }
    public void Next()
    {
       
            SceneManager.LoadScene(Level);
    }
    public void BackToGame()
    {
        Time.timeScale = 1;
        Panels[2].SetActive(false);

    }
    void CalculatePoint()
    {
        Point += (int) (LevelTime * 7 + 900 );

        string scoreStr = Point.ToString();
        string result = "";

        foreach (char c in scoreStr)
        {
            int digit = c - '0';
            result += $"<sprite={digit}>";
        }
        Debug.Log(result);
        scoreText.text = result;

    }

    IEnumerator Controled(int SelectedNumber)
    {
        ControlButtons(false);
        yield return new WaitForSeconds((float)0.6);
        if (FirstSelect == SelectedNumber)
        {
            FirstButon.GetComponent<Image>().enabled = false;
            FirstButon.GetComponent<Button>().enabled = false;
            SelectedObject.GetComponent<Image>().enabled = false;
            SelectedObject.GetComponent<Button>().enabled = false;
            FirstSelect = 0;
            FirstButon = null;
            ControlButtons(true);
            SaveLevel(Level);
            Corrects += 1;
            Debug.Log("dogru"+Corrects);
            CalculatePoint();

        }
        else
        {

            FirstButon.GetComponent<Image>().sprite = SpriteDefault;
            SelectedObject.GetComponent<Image>().sprite = SpriteDefault;
            FirstSelect = 0;
            FirstButon = null;
            audioSources[1].Play();
            ControlButtons(true);

        }
    }
    IEnumerator CreateButtons()
    {
        Debug.Log("create calýstý");
        yield return new WaitForSeconds(0.1f);
        while (Create)
        {
            int randomNum = Random.Range(0, Pool.transform.childCount - 1);

            if (Pool.transform.GetChild(randomNum) != null)
            {
                createdObject++;
                Pool.transform.GetChild(randomNum).transform.SetParent(Grid.transform);

                if (Object == createdObject)
                {
                    Create = false;
                }
            }
        }
        



    }
    public void SaveLevel(int completedLevel)
    {
        PlayerPrefs.SetInt("Level", completedLevel);
    }

}
