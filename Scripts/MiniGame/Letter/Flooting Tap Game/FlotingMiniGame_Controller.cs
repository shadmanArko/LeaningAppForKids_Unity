using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FlotingMiniGame_Controller : MiniGame
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
    private List<AnswerItem_FlotingTapGame> _floatingTap = new List<AnswerItem_FlotingTapGame>();
    public override MiniGameSetting MiniGameSettings { get; set; }


    private void Start()
    {
        _levelCompleted = false;
        currentCorrentAnswer = 0;
    }

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

        foreach (var floating in _floatingTap)
        {
            floating.SmoothTweenKill();
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

        miniGameViewController = FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);

        gameObject.SetActive(true);
    }
    public override async void GenerateLetters()
    {
        List<string> generatedAlphabets = GetRandomizedLetters();
        VoiceController.instance.Speak($"Tap {MiniGameSettings.NoOfCorrectAnswers} {recognitionString} Ballon");


        foreach (string letter in generatedAlphabets)
        {
            if(_levelCompleted)
                return;
            GameObject g = Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerItem_FlotingTapGame answerItem = g.GetComponent<AnswerItem_FlotingTapGame>();
            
            _floatingTap.Add(answerItem);
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
    public override void EndGame()
    {

    }



    public void SetInstruction()
    {
        instruction+= "  \"" + recognitionString+"\"";
    }
}
