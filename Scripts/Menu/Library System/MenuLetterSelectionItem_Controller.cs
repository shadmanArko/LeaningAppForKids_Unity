using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;


public class MenuLetterSelectionItem_Controller : MonoBehaviour
{
    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;
    public LetterType letterType;
    [SerializeField]
    private string title;
    // Start is called before the first frame update
    private void Awake()
    {
        titleText.text = title;
        List<string> letters = CharacterData.letters;
        switch (letterType)
        {
            case LetterType.UpperCase:
                if (PlayerPrefs.GetString("selectedLanguage") == "English")
                {
                    for (int i = 0; i < letters.Count; i++)
                    {
                        letters[i] = letters[i].ToUpper();
                    }
                    SpawnButton(letters, true);
                }
                else
                    SpawnButton(CharacterData.banglaShorborno, true);
                break;

            case LetterType.LowerCase:
                if (PlayerPrefs.GetString("selectedLanguage") == "English")
                    SpawnButton(letters, true);
                else
                    SpawnButton(CharacterData.banglaBanjonborno, true);
                break;

            case LetterType.Numarical:

                SpawnButton(CharacterData.numericals, false);
                break;
            default:
                break;
        }

    }
    private void SpawnButton(List<string> item, bool isletter)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject g = Instantiate(buttonPrefab, spawnRectTransform);
            g.GetComponent<ButtonManagerBasic>().buttonText = item[i];
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item[i];
            g.GetComponent<LevelLoader>().videoURL = isletter ? VideoData.letterSound[i] : VideoData.Numerical[i];
        }
        for (int i = 0; i < CharacterData.letters.Count; i++)
        {
            CharacterData.letters[i] = CharacterData.letters[i].ToLower();
        }
    }
}

