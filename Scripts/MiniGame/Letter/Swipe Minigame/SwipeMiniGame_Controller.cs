using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class SwipeMiniGame_Controller : MiniGame
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
    private bool _levelCompleted = false;

    private MiniGameViewController miniGameViewController;
    private List<AnswerItem_SwipeTapGame> _swipeTap = new List<AnswerItem_SwipeTapGame>();

    public override MiniGameSetting MiniGameSettings { get; set; }
    public override void CalcualteResult()
    {
        if (currentCorrentAnswer == MiniGameSettings.NoOfCorrectAnswers)
        {
            _levelCompleted = true;
            StartCoroutine(GameCompleteWait());
            return;
        }
    }
    IEnumerator GameCompleteWait()
    {
        foreach (var swipe in _swipeTap)
        {
            swipe.StopDoTween(true);
        }

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
    public override async void GenerateLetters()
    {
        List<string> generatedAlphabets = GetRandomizedLetters();
        
        VoiceController.instance.Speak($"Swipe {MiniGameSettings.NoOfCorrectAnswers} {recognitionString}");
        foreach (string letter in generatedAlphabets)
        {
            if(_levelCompleted)
                return;
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerItem_SwipeTapGame answerItem = g.GetComponent<AnswerItem_SwipeTapGame>();
            _swipeTap.Add(answerItem);
            answerItem.Initialize(letter, recognitionString);
            answerItem.OnRightAnswer.AddListener(() =>
            {
                score += 10;
                currentCorrentAnswer++;
                miniGameViewController.SetScoreOnView(score);
                VoiceController.instance.Speak(recognitionString);
                CalcualteResult();
            });
            answerItem.OnWrongAnswer.AddListener(() =>
            {
                //VoiceController.instance.Speak("    Wrong Answer");
                CalcualteResult();
            });

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
    }
    public List<string> GetRandomizedObjects()
    {
        int randomFruit = Random.Range(0, FruitsData.fruitsName.Count);
        recognitionString = FruitsData.fruitsName[randomFruit];
        List<string> selectedFruit = new List<string>();
        for (int i = 0; i < MiniGameSettings.NoOfCorrectAnswers; i++)
        {
            selectedFruit.Add(recognitionString);
        }
        for (int i = MiniGameSettings.NoOfCorrectAnswers; i < MiniGameSettings.NoOfPlaceholder; i++)
        {
            int randomSelection = UnityEngine.Random.Range(0, FruitsData.fruitsName.Count);
            string selectedCharacter = FruitsData.fruitsName[randomSelection];
            selectedFruit.Add(selectedCharacter);
        }
        var random = new System.Random();
        var randomized = selectedFruit.OrderBy(item => random.Next());
        selectedFruit = randomized.ToList();
        return selectedFruit;
    }
    public override void EndGame()
    {

    }
    private void Start() => currentCorrentAnswer = 0;


    public void SetInstruction()
    {
        instruction = "Swipe 3 times on the balloon containing learned letter \"" + recognitionString + "\"";
    }
}
