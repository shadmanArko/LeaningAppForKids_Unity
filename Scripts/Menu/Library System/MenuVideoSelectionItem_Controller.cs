using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PreviewVideoType { Letter, Numarical, Story }
public class MenuVideoSelectionItem_Controller : MonoBehaviour
{
    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;
    public PreviewVideoType previewVideoType;

    public string videoURL;
    [SerializeField]
    private string title;
    // Start is called before the first frame update
    void Start()
    {
        titleText.text = title;
        switch (previewVideoType)
        {
            case PreviewVideoType.Letter:
                SpawnButton(VideoData.letterSound);
                break;
            case PreviewVideoType.Numarical:
                SpawnButton(VideoData.earlyCounting);
                break;
            case PreviewVideoType.Story:
                SpawnButton(VideoData.story);
                break;
            default:
                break;
        }
    }
    void SpawnButton(List<string> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
            g.GetComponentInChildren<TextMeshProUGUI>().text = "play";

            


            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item[i];
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                string t = g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
                PreviewVideoDetails previewVideoDetails = new PreviewVideoDetails(t, title);
                GameManager.instance.selectedpreviewVideoDetails = previewVideoDetails;
                FindObjectOfType<AudioManager>().Play("click");
                FindObjectOfType<AudioManager>().Stop("theme");
                SceneManager.LoadScene("VideoShow");
            });
        }
    }
}
