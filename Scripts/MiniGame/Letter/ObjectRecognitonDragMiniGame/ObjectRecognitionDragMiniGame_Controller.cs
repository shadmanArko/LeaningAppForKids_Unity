using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectRecognitionDragMiniGame_Controller : MiniGame
{
    public GameObject tapObjectPrefab;
    public GameObject placeholderPointImage;

    public RectTransform[] objectContentPanel;
    public RectTransform pointedObject;

    public string instruction;
    public string character;

    public int totalCorrectAnswer;
    public int totalAnswer;

    private int score;
    private string recognitionString;
    private int currentCorrentAnswer;

    private MiniGameViewController miniGameViewController;
    public override MiniGameSetting MiniGameSettings { get; set; }
    private void Start() => currentCorrentAnswer = 0;
    public override void CalcualteResult()
    {
        if (currentCorrentAnswer == MiniGameSettings.NoOfCorrectAnswers)
        {
            StartCoroutine(GameCompleteWait());
            return;
        }
    }
    IEnumerator GameCompleteWait()
    {
        yield return new WaitForSeconds(1f);
        miniGameViewController.SetGameoverView(score);
    }
    public override void InitializeGame()
    {
        recognitionString = MiniGameSettings.RecognitionString;
        ShowVideo_Manager.Instance.ShowVideo(MiniGameSettings.VideoUrl);
        ShowVideo_Manager.Instance.youtubePlayer.OnVideoFinished.RemoveAllListeners();
        ShowVideo_Manager.Instance.youtubePlayer.OnVideoFinished.AddListener(() =>
        {
            ShowVideo_Manager.Instance.ShowAnimation(recognitionString, instruction);
            ShowVideo_Manager.Instance.animatedCharacter.OnAnimationFinished.RemoveAllListeners();
            ShowVideo_Manager.Instance.animatedCharacter.OnAnimationFinished.AddListener(() =>
            {
                ShowVideo_Manager.Instance.StartGame();
                StartGame();
            });
        });
    }
    public override void StartGame()
    {
        SetInstruction();

        MiniGameSettings = new MiniGameSetting(0, instruction, recognitionString, "", totalCorrectAnswer, totalAnswer);

        miniGameViewController = GameObject.FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);

        gameObject.SetActive(true);
    }
    public override void EndGame()
    {
    }
    public override void GenerateLetters()
    {
        List<string> generatedFruits = GetRandomizedFruits();
        List<string> generatedAlphabets = GetRandomizedLetters();
        int i = 0;
        foreach (string letter in generatedAlphabets)
        {
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel[i]);
            AnswerItem_ObjectRecognitionDragGame answerItem = g.GetComponent<AnswerItem_ObjectRecognitionDragGame>();
            answerItem.Initialize(letter, recognitionString, placeholderPointImage);
            answerItem.fruitName = generatedFruits[i];
            answerItem.OnRightAnswer.AddListener(() =>
            {
                score += 10;
                currentCorrentAnswer++;
                miniGameViewController.SetScoreOnView(score);
                answerItem.countText.gameObject.SetActive(true);
                VoiceController.instance.Speak(answerItem.countText.text);
                CalcualteResult();
            });
            answerItem.OnWrongAnswer.AddListener(() =>
            {
               // VoiceController.instance.Speak("wrong");
                CalcualteResult();

            });
            i++;
        }
        VoiceController.instance.Speak($"Drag {MiniGameSettings.NoOfCorrectAnswers} {recognitionString} into the bucket");
    }  
    public List<string> GetRandomizedFruits()
    {
        int randomFruit = Random.Range(0, FruitsData.fruitsName.Count);
        string recognitionFruitString = FruitsData.fruitsName[randomFruit];
        List<string> selectedFruit = new List<string>();
        for (int i = 0; i < MiniGameSettings.NoOfPlaceholder; i++)
        { 
            selectedFruit.Add(recognitionFruitString);
        }
        return selectedFruit;
    }

    public void SetInstruction()
    {
        instruction = "Drag three " + recognitionString +  " into the basket";
    }
}
