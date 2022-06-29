using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class MenuReadingSelection_Controller : MonoBehaviour
{
    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;
    [SerializeField]
    private string title;
    // Start is called before the first frame update
    void Start()
    {
        titleText.text = title;

        SpawnButton(CharacterData.numericals, false);

    }
    void SpawnButton(List<string> item, bool isletter)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
            g.GetComponent<ButtonManagerBasic>().buttonText = item[i];
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item[i];
            if (isletter)
            {
                g.GetComponent<LevelLoader>().videoURL = VideoData.letterSound[i];
            }
        }

    }
}
