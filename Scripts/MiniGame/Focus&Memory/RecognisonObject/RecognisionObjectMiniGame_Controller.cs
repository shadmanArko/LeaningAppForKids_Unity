using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RecognisionObjectMiniGame_Controller : MiniGame
{
    public GameObject tapObjectPrefab;
    public RectTransform objectContentPanel;

    public string instruction;
    public string character;

    public int totalCorrectAnswer;
    public int totalAnswer;

    private int score;
    private string recognitionString;
    private int currentCorrentAnswer;

    private MiniGameViewController miniGameViewController;
    public override MiniGameSetting MiniGameSettings { get; set; }

    public enum ListofItemList
    {
        randomizeObject,
        cleaningDust,
        fruitData
    }
    [SerializeField] private ListofItemList listofItemList;
    private List<string> CurrentList()
    {
        switch (listofItemList)
        {
            case ListofItemList.randomizeObject:
                return ObjectsData.RecognitionObject;
                break;
            case ListofItemList.cleaningDust:
                return ObjectsData.CleaningObject;
                break;
            case ListofItemList.fruitData:
                return FruitsData.fruitsName;
                break;
            default:
                return FruitsData.fruitsName;
                break;
        }
    }
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
        miniGameViewController.SetGameoverView(MiniGameSettings.Score);
    }
    public override void EndGame()
    {

    }
    public override void GenerateLetters()
    {
        recognitionString = GameManager.instance.selectedMiniGameDetails.RecognitionObject;
        RandomizedObjectList randomizedObjectList = new RandomizedObjectList();
        List<string> generatedFruits = randomizedObjectList.GetRandomizedItem(CurrentList(), MiniGameSettings.NoOfCorrectAnswers, MiniGameSettings.NoOfPlaceholder, recognitionString);
        foreach (string fruit in generatedFruits)
        {
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerTapItem_ToResourceLoad answerItem = g.GetComponent<AnswerTapItem_ToResourceLoad>();
            answerItem.Initialize(fruit, recognitionString);
            answerItem.OnRightAnswer.AddListener(() =>
            {
                MiniGameSettings.Score += 10;
                currentCorrentAnswer++;
                miniGameViewController.SetScoreOnView(MiniGameSettings.Score);
                VoiceController.instance.Speak(recognitionString+" Right Answer");
                CalcualteResult();
            });
            answerItem.OnWrongAnswer.AddListener(() =>
            {
                //Voice
                VoiceController.instance.Speak(" Wrong Answer");
                CalcualteResult();
            });

        }
        VoiceController.instance.Speak($"select {MiniGameSettings.NoOfCorrectAnswers} {recognitionString}");
    }
    public override void InitializeGame()
    {
        recognitionString = GameManager.instance.selectedMiniGameDetails.RecognitionObject;
        MiniGameSettings = new MiniGameSetting(0, instruction, recognitionString, "", totalCorrectAnswer, totalAnswer);

        miniGameViewController = GameObject.FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);
        gameObject.SetActive(true);
    }
    public override void StartGame()
    {
        
    }
}
