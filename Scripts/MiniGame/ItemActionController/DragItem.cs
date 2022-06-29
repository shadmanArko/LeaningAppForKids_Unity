using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DragItem : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnRightAnswer;
    [HideInInspector] public UnityEvent OnWrongAnswer;

    [HideInInspector] public RectTransform parentToReturnTo = null;
    [HideInInspector] public RectTransform placeholderParent = null;
    [HideInInspector] public string answerText;

    private GameObject pointPlaceholder;
    protected GameObject placeholder = null;
    private string correctAnswer;
    protected bool allowBeginDrag = true;
    private bool isDroped = false;
    protected Vector3 initialPosition;
    protected int newSiblingIndex;
    public void DragBegin()
    {
        if (allowBeginDrag)
        {
            isDroped = false;
            placeholder = new GameObject();
            placeholder.AddComponent<RectTransform>();
            placeholder.transform.SetParent(this.transform.parent.parent);
            placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            parentToReturnTo = (RectTransform)gameObject.transform.parent;
            placeholderParent = (RectTransform)gameObject.transform.parent;
            initialPosition = gameObject.transform.position;

            this.transform.SetParent(this.transform.parent.parent);

            PositionRearranged();
        }
    }
    public void Drag() => this.transform.position = allowBeginDrag ? Input.mousePosition : gameObject.transform.position;
    public void Drop()
    {
        isDroped = true;
        if (allowBeginDrag)
        {
            float distance = Vector3.Distance(gameObject.transform.position, pointPlaceholder.transform.position);
            if (distance < 400)
                CheckAnswer();
            else
            {
                gameObject.transform.SetParent(parentToReturnTo);
                gameObject.transform.SetSiblingIndex(newSiblingIndex);
                gameObject.transform.position = initialPosition;
                Destroy(placeholder);
            }
        }
    }
    public void EndDrag()
    {
        if (!isDroped)
        {
            gameObject.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(newSiblingIndex);
            gameObject.transform.position = initialPosition;

            Destroy(placeholder);
        }
    }

    private void PositionRearranged()
    {
        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent);

        newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void Initialize(string answer, string rightAnswer, GameObject placeholder)
    {
        answerText = answer;
        correctAnswer = rightAnswer;
        pointPlaceholder = placeholder;
    }
    public void CheckAnswer()
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
    protected virtual void RightAnswerBehaviour()
    {
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
        gameObject.transform.position = pointPlaceholder.transform.position;
        Destroy(gameObject, 1);

        allowBeginDrag = false;
    }
    protected virtual void WrongAnswerBehaviour()
    {
        GetComponent<Image>().color = Color.red;
        GetComponent<Button>().interactable = false;
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(newSiblingIndex);
        this.transform.position = initialPosition;
        Destroy(placeholder);
        allowBeginDrag = false;
    }
}
