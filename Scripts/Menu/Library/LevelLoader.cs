using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using UnityEngine.Serialization;

public class LevelLoader : MonoBehaviour
{
    public TextMeshProUGUI recognitionCharacter;
    public TextMeshProUGUI banglaRecognisionCharacter;
    public string videoURL;
    public bool isUpperCase;
    public string loadSceneName;
    //public string FruitName;
    public string ShapeName;
    public bool isText;
    public Image RecognitionImage;

    private void Start()
    {
        if (PlayerPrefs.GetString("selectedLanguage") == "English")
        {
            recognitionCharacter.gameObject.SetActive(true);
            banglaRecognisionCharacter.gameObject.SetActive(false);
        }
        else
        {
            recognitionCharacter.gameObject.SetActive(false);
            banglaRecognisionCharacter.gameObject.SetActive(true);
        }
    }
    public void OnButtonClicked()
    {
        MiniGameDetails miniGameDetails;
        if (PlayerPrefs.GetString("selectedLanguage") == "English")
        {
            miniGameDetails = new MiniGameDetails(videoURL, recognitionCharacter.text, recognitionCharacter.text);
            GameManager.instance.selectedMiniGameDetails = miniGameDetails;
        }
        else
        {
            miniGameDetails = new MiniGameDetails(videoURL, banglaRecognisionCharacter.text, VoiceData.banglaAlphabetDictionary[banglaRecognisionCharacter.text]);
            GameManager.instance.selectedMiniGameDetails = miniGameDetails;
        }

        //Fruit.SelectedFruit = FruitName;
        Shape.SelectedShape = ShapeName;

        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<AudioManager>().Stop("theme");
        SceneManager.LoadScene(loadSceneName);
    }

    public void Init(string itemName, string sceneName, string videoURL, string recognitionObject = null)
    {
        recognitionCharacter.gameObject.SetActive(isText);
        RecognitionImage.gameObject.SetActive(!isText);
        gameObject.GetComponent<ButtonManagerBasic>().buttonText = itemName;
        recognitionCharacter.text = itemName;
        this.videoURL = videoURL;
        loadSceneName = sceneName;
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            MiniGameDetails miniGameDetails;
            miniGameDetails = new MiniGameDetails(videoURL, itemName, itemName, recognitionObject== null);
            miniGameDetails.RecognitionObject = recognitionObject;
            GameManager.instance.selectedMiniGameDetails = miniGameDetails;
            FindObjectOfType<AudioManager>().Play("click");
            FindObjectOfType<AudioManager>().Stop("theme");
            SceneManager.LoadScene(loadSceneName);
        });
    }
}

