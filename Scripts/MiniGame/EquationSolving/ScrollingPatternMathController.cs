using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingPatternMathController : MiniGame
{
    public RectTransform LetterContainerPanelRect;

    public GameObject LetterContainerPrefab;
    public GameObject ButtonPrefab;

    public TextMeshProUGUI wordText;

    public Button SubmitButton;

    public string word;
    public string instruction;

    private GameObject[] letterScroller;
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
    public override void InitializeGame()
    {
        recognitionString = MiniGameSettings.RecognitionString;
        MiniGameSettings = new MiniGameSetting(0, instruction, recognitionString, "", 0, 0);

        miniGameViewController = GameObject.FindObjectOfType<MiniGameViewController>();
        GenerateLetters();
        miniGameViewController.SetInstructionOnView(MiniGameSettings.Instruction);
        miniGameViewController.SetScoreOnView(0);
        gameObject.SetActive(true);
    }

    public override void StartGame()
    {
    }

    public override void EndGame()
    {
    }
    public override void GenerateLetters()
    {
        int length = word.Length;
        letterScroller = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            GameObject clone = Instantiate(LetterContainerPrefab, LetterContainerPanelRect);
            LetterContainerUI letterContainerUI = clone.GetComponent<LetterContainerUI>();
            letterContainerUI.Initialize(ButtonPrefab);
            letterScroller[i] = clone;
        }

        VoiceController.instance.Speak($"Scroll and match {word}");
    }
    public void ScrollUpdate()
    {
        wordText.text = "";
        for (int i = 0; i < letterScroller.Length; i++)
        {
            LetterContainerUI letterContainerUI = letterScroller[i].GetComponent<LetterContainerUI>();
            letterContainerUI.VerticalScroller.Update();
            string letterString = letterContainerUI.VerticalScroller.result;
            wordText.text += letterString;
        }
        UpdateSubmitButton();
    }
    private void UpdateSubmitButton()
    {
        SubmitButton.interactable = wordText.text == word;
        SubmitButton.onClick.AddListener(() =>
        {
            MiniGameSettings.Score = 100;
            CalcualteResult();
        });
    }
}