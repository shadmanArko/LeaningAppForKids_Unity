using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class AnswerTapItem_ToResourceLoad : MonoBehaviour
{
    public TextMeshProUGUI count;
    public Image image;
    [HideInInspector]public UnityEvent OnRightAnswer;
    [HideInInspector]public UnityEvent OnWrongAnswer;
    private string correctAnswer;
    private string answerText;

    [SerializeField] private string path;

    private void Start()
    {
        count.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(() =>
        {
            CheckAnswer();
        });
    }
    public void Initialize(string answer, string rightAnswer)
    {
        answerText = answer;
        correctAnswer = rightAnswer;
        image.sprite = Resources.Load<Sprite>($"{path}{answer}");
        Debug.Log($"{answer} {rightAnswer}");
    }
    private void CheckAnswer()
    {
        bool isRightAnswer = false;
        isRightAnswer = answerText == correctAnswer ? true : false;
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
