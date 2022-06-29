using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;


public class MenuCountingSelection_Controller : MonoBehaviour
{
    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;
    public Logic logic;
    public GameType Game = GameType.None;
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
        if (Game == GameType.ShapeFall)
        {
            ButtonForShapesFall(item);
        }

        else if (Game == GameType.None)
        {
            ButtonForOther(item, isletter);
        }
    }

    public void ButtonForShapesFall(List<string> item)
    {

        //for (int i = 0; i < FruitsData.fruitsName.Count; i++)
        //{
        //    GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
        //    g.GetComponent<ButtonManagerBasic>().buttonText = FruitsData.fruitsName[i];
        //    g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FruitsData.fruitsName[i];
        //    g.GetComponent<LevelLoader>().FruitName = FruitsData.fruitsName[i];
        //}


        for (int i = 0; i < ShapesData.shapesName.Count; i++)
        {
            GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
            g.GetComponent<ButtonManagerBasic>().buttonText = ShapesData.shapesName[i];
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ShapesData.shapesName[i];
            g.GetComponent<LevelLoader>().ShapeName = ShapesData.shapesName[i];
            g.GetComponent<LevelLoader>().videoURL = VideoData.GeometryShape[0];
        }

    }

    public void ButtonForOther(List<string> item, bool isLetter)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject g = GameObject.Instantiate(buttonPrefab, spawnRectTransform);
            g.GetComponent<ButtonManagerBasic>().buttonText = item[i];
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item[i];

            if (isLetter)
            {
                g.GetComponent<LevelLoader>().videoURL = VideoData.letterSound[i];
            }
        }

        
    }
}
