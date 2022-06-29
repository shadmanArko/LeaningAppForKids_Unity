using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CourseMenuController : MonoBehaviour
{

    public static CourseMenuController Instance;

    public List<GameObject> ALevelLoaders;
    public bool GameIsPlaying;


    public List<GameObject> PanelSerial;


    void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        else
            DestroyImmediate(this.gameObject);
        
    }
    void Start()
    {
        GameIsPlaying = false;

        Invoke("InitializeLevelLoaders", 5f);




        //InitializeLevelLoaders();
    }

    [ContextMenu("Do Something")]
    public void InitializeLevelLoaders()
    {
        foreach (var panel in PanelSerial)
        {
            List<LevelLoader> tempList = new List<LevelLoader>();
            tempList = panel.GetComponentsInChildren<LevelLoader>().ToList();

            List<GameObject> tempGameObjects = new List<GameObject>();

            foreach (var temp in tempList)
            {
                tempGameObjects.Add(temp.gameObject);
            }

            ALevelLoaders.AddRange(tempGameObjects);
        }

        foreach (var loader in ALevelLoaders)
        {
            loader.transform.parent = this.gameObject.transform;
        }

        Initialize();
    }

    async void  Initialize()
    {
        for (int i = GetCourseNumber(); i<ALevelLoaders.Count; i++)
        {
           
            ALevelLoaders[i].GetComponent<LevelLoader>().OnButtonClicked();
            GameIsPlaying = true;
            await UniTask.WaitUntil(() => GameIsPlaying == false);
            //await UniTask.Delay(TimeSpan.FromSeconds(5f));
            SetCourseNumber(i);


        }
        //Debug.Log("dipok recog " + ALevelLoaders[ALevelLoaders.Count - 1].GetComponentInChildren<TextMeshProUGUI>().text);
    }

    int GetCourseNumber()
    {

        return PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedKid") + "CourseCompletedId");
    }

    void SetCourseNumber(int value)
    {

        PlayerPrefs.SetInt(PlayerPrefs.GetInt("SelectedKid") + "CourseCompletedId", ++value);
    }

}
