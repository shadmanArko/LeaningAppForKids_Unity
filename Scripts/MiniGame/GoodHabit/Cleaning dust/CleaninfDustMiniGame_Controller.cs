using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CleaninfDustMiniGame_Controller : MiniGame
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
        miniGameViewController.SetGameoverView(MiniGameSettings.Score);
    }
    public override void EndGame()
    {

    }
    public override void GenerateLetters()
    {
        List<string> generatedFruits = GetRandomizedObjects();
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
                VoiceController.instance.Speak("Opps Wrong Answer");
                CalcualteResult();
            });

        }
        VoiceController.instance.Speak($"select {MiniGameSettings.NoOfCorrectAnswers} {recognitionString}");
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

    public List<string> GetRandomizedObjects()
    {
        int randomFruit = Random.Range(0, FruitsData.fruitsName.Count);
        //recognitionString = FruitsData.fruitsName[randomFruit];
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
}
