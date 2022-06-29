using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniGameSetting
{
    public int Score { get; set; }
    public string Instruction { get; private set; }
    public string RecognitionString { get; private set; }
    public string RecognitionLanguage { get; private set; }
    public int NoOfCorrectAnswers { get; private set; }
    public int NoOfPlaceholder { get; private set; }
    public string VideoUrl { get; private set; }
    public MiniGameSetting(string recognitionString)
    {
        this.RecognitionString = recognitionString;
    }
    public MiniGameSetting(string recognitionString, string videoURL)
    {
        this.RecognitionString = recognitionString;
        this.VideoUrl = videoURL;
    }
    public MiniGameSetting(int score, string instruction, string recognitionString, string recognitionLanguage, int noOfCorrectAnswers, int noOfPlaceholder)
    {
        this.Instruction = instruction;
        this.Score = score;
        this.RecognitionString = recognitionString;
        this.RecognitionLanguage = recognitionLanguage;
        this.NoOfCorrectAnswers = noOfCorrectAnswers;
        this.NoOfPlaceholder = noOfPlaceholder;
    }
}

public abstract class MiniGame : MonoBehaviour
{
    public abstract MiniGameSetting MiniGameSettings { get; set; }
    public abstract void InitializeGame();
    public abstract void StartGame();
    public abstract void EndGame();
    public abstract void CalcualteResult();
    public abstract void GenerateLetters();
    public virtual List<string> GetRandomizedLetters()
    {
        bool isNumber = int.TryParse(MiniGameSettings.RecognitionString, out _);
        List<string> selectedAlphabets = new List<string>();
        List<string> letters;
        for (int i = 0; i < MiniGameSettings.NoOfCorrectAnswers; i++)
        {
            selectedAlphabets.Add(MiniGameSettings.RecognitionString);
        }
        if (!isNumber)
        {
            if (PlayerPrefs.GetString("selectedLanguage") == "English")
            {
                bool isUppercase = MiniGameSettings.RecognitionString.Any(char.IsUpper);
                letters = CharacterData.letters;
                for (int i = 0; i < letters.Count; i++)
                {
                    if (isUppercase)
                        letters[i] = letters[i].ToUpper();
                }
                selectedAlphabets = Generate(letters.Count, letters, selectedAlphabets);
            }
            else
            {
                bool isBanjonborno = CharacterData.banglaBanjonborno.Contains(MiniGameSettings.RecognitionString);
                if (isBanjonborno)
                {
                    letters = CharacterData.banglaBanjonborno;
                    for (int i = 0; i < letters.Count; i++)
                    {
                        letters[i] = CharacterData.banglaBanjonborno[i];
                    }
                    selectedAlphabets = Generate(letters.Count, letters, selectedAlphabets);
                }
                else
                {
                    letters = CharacterData.banglaShorborno;
                    for (int i = 0; i < letters.Count; i++)
                    {
                        letters[i] = CharacterData.banglaShorborno[i];
                    }
                    selectedAlphabets = Generate(letters.Count, letters, selectedAlphabets);
                }
            }
        }
        else
        {
            letters = CharacterData.numericals;
            selectedAlphabets = Generate(10, letters, selectedAlphabets);
        }
        var random = new System.Random();
        var randomized = selectedAlphabets.OrderBy(item => random.Next());
        selectedAlphabets = randomized.ToList();
        return selectedAlphabets;
    }
    private List<string> Generate(int maxRange, List<string> letters, List<string> selectedAlphabets) {
        for (int i = MiniGameSettings.NoOfCorrectAnswers; i < MiniGameSettings.NoOfPlaceholder; i++)
        {
            int randomSelection = Random.Range(0, maxRange);
            string selectedCharacter = letters[randomSelection];
            selectedAlphabets.Add(selectedCharacter);
        }
        return selectedAlphabets;
    }
}