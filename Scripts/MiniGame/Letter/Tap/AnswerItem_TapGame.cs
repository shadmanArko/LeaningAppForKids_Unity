using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class AnswerItem_TapGame : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public Image answerImage;
    private string correctAnswer;

    [HideInInspector] public UnityEvent OnRightAnswer;
    [HideInInspector] public UnityEvent OnWrongAnswer;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            CheckAnswer();
        });
    }
    public void Initialize(string answer, string rightAnswer)
    {
        answerText.text = answer;
        correctAnswer = rightAnswer;
    }
    public void Initialize(Image image, string answer, string rightAnswer)
    {
        answerText.text = answer;
        correctAnswer = rightAnswer;
    }
    private void CheckAnswer()
    {
        bool isRightAnswer = answerText.text == correctAnswer;
        if (isRightAnswer)
        {
            RightAnswerBehaviour();
            OnRightAnswer?.Invoke();
        }
        else
        {
            WrongAnswerBehaviour();
            OnWrongAnswer?.Invoke();
        }
    }
    private void RightAnswerBehaviour()
    {
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
    }
    private void WrongAnswerBehaviour()
    {
        GetComponent<Image>().color = Color.red;
        GetComponent<Button>().interactable = false;
    }
}
