using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cysharp.Threading.Tasks;

public class ShapeCollectMIniGame_Controller : MiniGame
{
    public GameObject tapObjectPrefab;
    public RectTransform objectContentPanel;

    public string instruction;
    public string character;

    public int totalCorrectAnswer;
    public int totalAnswer;

    public Slider DiceMoveSlider;
    public RectTransform Dice;

    private int score;
    private string recognitionString;
    private int currentCorrentAnswer;
    private bool _levelCompleted = false;

    private MiniGameViewController miniGameViewController;
    public override MiniGameSetting MiniGameSettings { get; set; }

    private List<AnswerItem_ShapeFallGame> _shapesFall = new List<AnswerItem_ShapeFallGame>();

    private void Start()
    {
        _levelCompleted = false;
        currentCorrentAnswer = 0;
        DiceMoveSlider.value = 0;
        Dice.anchoredPosition = new Vector2(0, Dice.anchoredPosition.y); 
        DiceMoveSlider.onValueChanged.AddListener((val) =>
        {
            Dice.anchoredPosition = new Vector2(val * 960f, Dice.anchoredPosition.y);
        });
    }
    public override void CalcualteResult()
    {
        if (currentCorrentAnswer == MiniGameSettings.NoOfCorrectAnswers)
        {
            _levelCompleted = true;
            Dice.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(GameCompleteWait());
            return;
        }
    }
    IEnumerator GameCompleteWait()
    {

        DiceMoveSlider.onValueChanged.RemoveAllListeners();

        foreach (var shape in _shapesFall)
        {
            shape.SmoothTweenKill();
        }
        yield return new WaitForSeconds(1f);
        miniGameViewController.SetGameoverView(MiniGameSettings.Score);
    }
    public override void EndGame()
    {

    }
    public override void InitializeGame()
    {
        SetCharacter();

        recognitionString = MiniGameSettings.RecognitionString;
        MiniGameSettings = new MiniGameSetting(0, instruction, recognitionString, "", totalCorrectAnswer, totalAnswer);

        miniGameViewController = GameObject.FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);
        gameObject.SetActive(true);
    }

    public override async void GenerateLetters()
    {
        
        recognitionString = character;
        

        RandomizedObjectList randomizedObjectList = new RandomizedObjectList();
        //List<string> generatedFruits = randomizedObjectList.GetRandomizedItem(FruitsData.fruitsName, MiniGameSettings.NoOfCorrectAnswers, MiniGameSettings.NoOfPlaceholder, recognitionString);


        List<string> generatedShapes = randomizedObjectList.GetRandomizedItem(ShapesData.shapesName, MiniGameSettings.NoOfCorrectAnswers, MiniGameSettings.NoOfPlaceholder, recognitionString);


        VoiceController.instance.Speak($"Collect {MiniGameSettings.NoOfCorrectAnswers} {recognitionString}");


        foreach (string shape in generatedShapes)
        //foreach (string fruit in generatedFruits)
        {
            if(_levelCompleted)
                return;
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerItem_ShapeFallGame answerItem = g.GetComponent<AnswerItem_ShapeFallGame>();
            _shapesFall.Add(answerItem);
            //answerItem.Initialize(fruit, recognitionString);

            answerItem.Initialize(shape, recognitionString);


            answerItem.OnRightAnswer.AddListener(() =>
            {
                MiniGameSettings.Score += 10;
                currentCorrentAnswer++;
                miniGameViewController.SetScoreOnView(MiniGameSettings.Score);

                VoiceController.instance.Speak(recognitionString + " Right Answer");
                CalcualteResult();
            });
            answerItem.OnWrongAnswer.AddListener(() =>
            {
                //Voice
                VoiceController.instance.Speak("    Wrong Answer");
                CalcualteResult();
            });


            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
      

    }

    public override void StartGame()
    {

    }

    public void SetCharacter()
    {

        //if (Fruit.SelectedFruit == "")
        //    character = FruitsData.fruitsName[0];
        //character = Fruit.SelectedFruit;

        if (Shape.SelectedShape == "")
            character = ShapesData.shapesName[0];
        character = Shape.SelectedShape;


        SetInstruction();
    }


    public void SetInstruction()
    {
        //instruction += Fruit.SelectedFruit;

        instruction += Shape.SelectedShape;


    }

}
