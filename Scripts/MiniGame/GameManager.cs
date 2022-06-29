using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MiniGameDetails selectedMiniGameDetails;
    public MathMiniGameDetails selectedMathMiniGameDetails;
    public BookReaderDetails selectedbookReaderDetails;
    public PreviewVideoDetails selectedpreviewVideoDetails;
    public LanguageDetails languageDetails;

    public LearningType LearningType;
    public bool ShowStartingPanel;
    public int KidId;
    public bool Return;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }

}
public class MiniGameDetails
{
    public string VideoToPlay;
    public string AlphabetToPlay;
    public string AlphabetToSpeach;
    public bool IsUpperCase;
    public bool IsRecognitionObject;
    public string RecognitionObject;
    
    public MiniGameDetails(string videoURL, string recognitionString, string speachText, bool isRecognitionObject)
    {
        VideoToPlay = videoURL;
        AlphabetToPlay = recognitionString;
        AlphabetToSpeach = speachText;
        this.IsRecognitionObject = isRecognitionObject;
    }
    
    public MiniGameDetails(string video, string alphabet, string speachAlphabet)
    {
        VideoToPlay = video;
        AlphabetToPlay = alphabet;
        AlphabetToSpeach = speachAlphabet;
    }
    public MiniGameDetails(string video, string alphabet,bool isUpperCase)
    {
        VideoToPlay = video;
        AlphabetToPlay = alphabet;
        this.IsUpperCase = isUpperCase;
    }

  
}
public class MathMiniGameDetails
{
    public int noOfSpawnObject;
    public int noOfSpawnRightObject;
    public string objectName;
    public MathMiniGameDetails(int spawnObject, int rightObject, string name)
    {
        noOfSpawnObject = spawnObject;
        noOfSpawnRightObject = rightObject;
        objectName = name;
    }
}
public class BookReaderDetails
{
    public string bookName;
    public string bookCategoryName;
    public BookReaderDetails(string name, string categoryName)
    {
        bookName = name;
        bookCategoryName = categoryName;
    }
}
public class PreviewVideoDetails
{
    public string videoToPlay;
    public string videoCategoryName;
    public PreviewVideoDetails(string url, string categoryName)
    {
        videoToPlay = url;
        videoCategoryName = categoryName;
    }
}

public class LanguageDetails
{
    public List<string> alphabets;
    public List<string> numaricAlphabet;
    public LanguageDetails(List<string> letter, List<string> number)
    {
        alphabets = letter;
        numaricAlphabet = number;
    }
}



