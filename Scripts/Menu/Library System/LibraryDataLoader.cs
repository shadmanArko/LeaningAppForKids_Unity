using System.Collections.Generic;
using UnityEngine;
public class LibraryDataLoader : MonoBehaviour
{
    public GameObject loadedSectionPrefab;
    public RectTransform spawnRectTransform;
    enum Section { book, video, draw, reading, math, logic }
    [SerializeField] private Section section;
    private GameObject sectionPrefab;
    private string loadedScene;
    private void Awake()
    {
        LoadedData();
    }
    private void LoadedData()
    {
        switch (section)
        {
            case Section.book:
                Debug.Log("Book Data Loaded");
                break;
            case Section.video:
                Debug.Log("Video Data Loaded");
                sectionPrefab = loadedSectionPrefab;
                loadedScene = "VideoShow";
                GenerateSaction(VideoData.videoType);
                break;
            case Section.draw:
                Debug.Log("Draw Data Loaded");
                break;
            case Section.reading:
                Debug.Log("Reading Data Loaded");
                break;
            case Section.math:
                Debug.Log("Math Data Loaded");
                break;
            case Section.logic:
                Debug.Log("Logic Data Loaded");
                break;
            default:
                break;
        }
    }

    private void GenerateSaction(List<string> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject g = GameObject.Instantiate(sectionPrefab, spawnRectTransform);
            g.GetComponent<LibraryItemSelection_Controller>().itemlist = VideoData.letterSound;
            g.GetComponent<LibraryItemSelection_Controller>().loadedScene = loadedScene;
            g.GetComponent<LibraryItemSelection_Controller>().titleText.text = itemList[i];
            g.GetComponent<LibraryItemSelection>().SpawnButton();
        }
    }
}