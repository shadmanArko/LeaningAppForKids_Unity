using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class AnswerItem_SwipeTapGame : MonoBehaviour
{
    public TextMeshProUGUI answerText;

    [HideInInspector] public UnityEvent OnRightAnswer;
    [HideInInspector] public UnityEvent OnWrongAnswer;

    [HideInInspector] public RectTransform parentToReturnTo = null;
    [HideInInspector] public RectTransform placeholderParent = null;

    private string correctAnswer;

    private Vector2 initialPosition;

    private Tween _mytween;
    private bool _stopTweening = false;

    public void PointerEnter()
    {
        
    }
    public void PointerExit() => CheckAnswer();
    
    public void MoveBegin()
    {
        int interval = Random.Range(3, 8);
        float targetUP = Random.Range(550f, 650f);

        _mytween = gameObject.GetComponent<RectTransform>().DOMoveY(targetUP, Random.Range(4, 8)).SetEase(Ease.OutSine)
            .SetLoops(-1, LoopType.Yoyo).SetAutoKill(false).OnStepComplete(() =>
            {
                if (_stopTweening)
                {
                    gameObject.GetComponent<RectTransform>().DOMoveY(-targetUP, Random.Range(4, 8));
                    _mytween.Kill(true);
                }
                    
            });


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
        gameObject.transform.DOKill();
        //DestroyImmediate(this.gameObject);
        gameObject.SetActive(false);
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

    public void StopDoTween(bool state)
    {
        Debug.Log("dipok stopdotween");
        _stopTweening = state;
        //_mytween.Kill(true);


    }
}
