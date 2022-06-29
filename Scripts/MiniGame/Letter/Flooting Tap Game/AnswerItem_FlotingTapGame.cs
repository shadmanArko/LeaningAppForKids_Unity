using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
public class AnswerItem_FlotingTapGame : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    private Image image;
    [HideInInspector] public UnityEvent OnRightAnswer;
    [HideInInspector] public UnityEvent OnWrongAnswer;

    [HideInInspector] public RectTransform parentToReturnTo = null;
    [HideInInspector] public RectTransform placeholderParent = null;

    private string correctAnswer;

    private Vector2 initialPosition;
    private Tween _mytween;
    public Sprite[] Ballons;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        transform.GetChild(0).GetComponent<Image>().sprite = Ballons[Random.Range(0, Ballons.Length)];
        GetComponent<Button>().onClick.AddListener(() =>
        {
            CheckAnswer();
        });
        
    }
    public void MoveBegin()
    {
        int interval = Random.Range(2,6);
        float targetY = Screen.height + 200f;


        //gameObject.GetComponent<RectTransform>().DOMoveY(targetY, Random.Range(10, 15)).SetLoops(-1, LoopType.Restart).SetDelay(interval);

        _mytween = gameObject.GetComponent<RectTransform>().DOMoveY(targetY, Random.Range(10, 15)).SetAutoKill(false)/*.SetLoops(-1, LoopType.Restart).SetDelay(interval)*/;


        _mytween.OnComplete((() => _mytween.Restart()));
    }

    public void Initialize(string answer, string rightAnswer)
    {
        answerText.text = answer;
        correctAnswer = rightAnswer;
        
        float targetX = Random.Range(220f, Screen.width - 220f);
        float targetY = -400f;
        initialPosition = new Vector2(targetX, targetY);
        gameObject.GetComponent<RectTransform>().anchoredPosition = initialPosition;
        MoveBegin();
    }
    public void CheckAnswer()
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
        DestroyImmediate(this.gameObject);
    }
    private void WrongAnswerBehaviour()
    {
        StartCoroutine(ResetColorOnInterval());
    }
    
    IEnumerator ResetColorOnInterval()
    {
        transform.GetChild(0).GetComponent<Image>().color = Color.red;
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(2);
        transform.GetChild(0).GetComponent<Image>().color = new Color(40, 142, 154, 255);
        GetComponent<Button>().interactable = true;
    }

    public void SmoothTweenKill()
    {
        _mytween.OnComplete(null);
        _mytween.SetAutoKill(true);
    }
}
