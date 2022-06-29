using System;
using Cysharp.Threading.Tasks;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public enum LearningType { None, Course, Library}

public class MiniGameViewController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI instructionText;
    public RectTransform resultView;
    public RectTransform resultViewParent;
    public Button NextButton;
    public Button HomeButton;
   

    public void SetScoreOnView(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void SetInstructionOnView(string instruction)
    {
        instructionText.text = instruction;
    }

    public void SetGameoverView(int finalScore)
    {
        resultViewParent.gameObject.SetActive(true);
        resultView.GetComponentInChildren<TextMeshProUGUI>().text = "Final Score : " + finalScore.ToString();

        resultView.GetComponentInChildren<TextMeshProUGUI>().text = "Well done!!! ";

        if (GameManager.instance.LearningType == LearningType.Library)
        {
            NextButton.gameObject.SetActive(false);
            HomeButton.GetComponent<ButtonManagerBasic>().buttonText = "Library Page";

            HomeButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("Library_Scene");
                FindObjectOfType<AudioManager>().Play("click");
                FindObjectOfType<AudioManager>().Play("theme");
            });
        }

        else if (GameManager.instance.LearningType == LearningType.Course)
        {
            //CourseMenuController.Instance.GameIsPlaying = false;

            
            HomeButton.onClick.AddListener(() =>
            {
                HomeButton.GetComponent<ButtonManagerBasic>().buttonText = "Home Page";
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("Demo_Home_Scene");
                FindObjectOfType<AudioManager>().Play("click");
            });


            NextButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                FindObjectOfType<AudioManager>().Play("click");
                CourseMenuController.Instance.GameIsPlaying = false;
            });
        }
    }
}
