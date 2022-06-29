using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using UnityEngine.SceneManagement;

public class LibraryItemSelection_Controller : LibraryItemSelection
{
    public override Grade grade { get; set; }

    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;
    public LetterType letterType;

    public List<string> itemlist = new List<string>();
    public string loadedScene;
    [SerializeField]
    private string title;

    public override void SpawnButton()
    {
        for (int i = 0; i < itemlist.Count; i++)
        {
            GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
            //g.GetComponent<ButtonManagerBasic>().buttonText = itemlist[i];
            //g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemlist[i];
            //if (isletter)
            //{
            //    g.GetComponent<LevelLoader>().videoURL = VideoData.letterSound[i];
            //}
            //g.GetComponent<LevelLoader>().videoURL = VideoData.letterSound[i];


            g.GetComponentInChildren<TextMeshProUGUI>().text = "play";
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemlist[i];
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                string t = g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
                PreviewVideoDetails previewVideoDetails = new PreviewVideoDetails(t, title);
                GameManager.instance.selectedpreviewVideoDetails = previewVideoDetails;
                FindObjectOfType<AudioManager>().Play("click");
                FindObjectOfType<AudioManager>().Stop("theme");
                print(loadedScene);
                SceneManager.LoadScene(loadedScene);
            });
        }
    }
}
