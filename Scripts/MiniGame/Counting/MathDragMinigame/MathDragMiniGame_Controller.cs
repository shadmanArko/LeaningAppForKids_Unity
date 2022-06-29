using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MathDragMiniGame_Controller : MiniGame
{
    public GameObject tapObjectPrefab;
    public GameObject placeholderPointImage;
    public RectTransform objectContentPanel;
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
        recognitionString = character;
        RandomizedObjectList randomizedObjectList = new RandomizedObjectList();
        List<string> generatedFruits = randomizedObjectList.GetRandomizedItem(FruitsData.fruitsName, MiniGameSettings.NoOfCorrectAnswers, MiniGameSettings.NoOfPlaceholder, recognitionString);
        foreach (string fruit in generatedFruits)
        {
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerItem_MathDragGame answerItem = g.GetComponent<AnswerItem_MathDragGame>();
            answerItem.Initialize(fruit, recognitionString, placeholderPointImage);
            answerItem.OnRightAnswer.AddListener(() =>
            {
                MiniGameSettings.Score += 10;
                currentCorrentAnswer++;
                miniGameViewController.SetScoreOnView(MiniGameSettings.Score);
                answerItem.countText.gameObject.SetActive(false);
                answerItem.countText.text = currentCorrentAnswer.ToString();
                VoiceController.instance.Speak(currentCorrentAnswer.ToString());
                CalcualteResult();
            });
            answerItem.OnWrongAnswer.AddListener(() =>
            {
                //Voice
                CalcualteResult();
            });

        }
        VoiceController.instance.Speak($"Drag {MiniGameSettings.NoOfCorrectAnswers} {recognitionString} on the box");
    }
    public override void InitializeGame()
    {
        recognitionString = MiniGameSettings.RecognitionString;
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
    private void Start()
    {
        currentCorrentAnswer = 0;
    }
}
