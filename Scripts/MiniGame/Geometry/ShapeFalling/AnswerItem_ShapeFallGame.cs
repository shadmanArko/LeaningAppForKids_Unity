using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class AnswerItem_ShapeFallGame : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public Image image;
    [HideInInspector] public UnityEvent OnRightAnswer;
    [HideInInspector] public UnityEvent OnWrongAnswer;

    [HideInInspector] public RectTransform parentToReturnTo = null;
    [HideInInspector] public RectTransform placeholderParent = null;

    private string correctAnswer;

    private Vector2 initialPosition;

    private Tween _mytween;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();

    }
    public void MoveBegin()
    {
        int interval = Random.Range(3, 8);
        float targetY = -(Screen.height + 100f);
        //_mytween = gameObject.GetComponent<RectTransform>().DOMoveY(targetY, Random.Range(5, 15)).SetLoops(-1, LoopType.Restart).SetDelay(interval);

        _mytween = gameObject.GetComponent<RectTransform>().DOMoveY(targetY, Random.Range(5, 15)).SetAutoKill(false)/*.SetLoops(-1, LoopType.Restart).SetDelay(interval)*/;


        _mytween.OnComplete((() => _mytween.Restart()));

    }

    public void Initialize(string answer, string rightAnswer)
    {
        answerText.text = answer;
        correctAnswer = rightAnswer;
        //image.sprite = Resources.Load<Sprite>($"Fruits/{answer}");

        image.sprite = Resources.Load<Sprite>($"Shapes/{answer}");


        float targetX = Random.Range(200f, Screen.width - 200f);
        float targetY = 400f;
        initialPosition = new Vector2(targetX, targetY);
        gameObject.GetComponent<RectTransform>().anchoredPosition = initialPosition;
        MoveBegin();
    }
    public void CheckAnswer()
    {
        bool isRightAnswer = false;
        isRightAnswer = answerText.text == correctAnswer ? true : false;
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
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        DOTween.Kill(this.gameObject);
        Destroy(this.gameObject);
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
        transform.GetChild(0).GetComponent<Image>().color = new Color(40, 142, 154, 0);
        GetComponent<Button>().interactable = true;
    }

    
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            CheckAnswer();
        }
        else { return; }
    }

    public void SmoothTweenKill()
    {
        //_mytween.SetLoops(0);
        _mytween.OnComplete(null);
        _mytween.SetAutoKill(true);
        //_mytween.Kill();
    }
}
