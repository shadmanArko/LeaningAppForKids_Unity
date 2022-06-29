using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class TapMinigame_Controller : MiniGame
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
        MiniGameSettings = new MiniGameSetting(0, instruction, recognitionString, "", totalCorrectAnswer, totalAnswer);

        miniGameViewController = GameObject.FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);

        gameObject.SetActive(true);
    }
    public override void GenerateLetters()
    {
        List<string> generatedAlphabets = GetRandomizedLetters();
        foreach (string letter in generatedAlphabets)
        {
            GameObject g = GameObject.Instantiate(tapObjectPrefab, objectContentPanel);
            AnswerItem_TapGame answerItem = g.GetComponent<AnswerItem_TapGame>();
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
                //VoiceController.instance.Speak("Wrong");
                CalcualteResult();
            });

        }
    }
    public override void EndGame()
    {

    }
    private void Start() => currentCorrentAnswer = 0;
}
