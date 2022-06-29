using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class AnswerItem_DragGame : MonoBehaviour
{
    public TextMeshProUGUI answerText;

    public UnityEvent OnRightAnswer;
    public UnityEvent OnWrongAnswer;

    public RectTransform parentToReturnTo = null;
    public RectTransform placeholderParent = null;

    private GameObject pointPlaceholder;
    GameObject placeholder = null;
    private string correctAnswer;
    private bool allowBeginDrag = true;
    private bool isDroped = false;
    private Vector3 initialPosition;
    private int newSiblingIndex;
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
            if (distance < 200)
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
    public void EndDrag() {
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
        answerText.text = answer;
        correctAnswer = rightAnswer;
        pointPlaceholder = placeholder;
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
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
        gameObject.transform.position = pointPlaceholder.transform.position;
        Destroy(gameObject, 1);

        allowBeginDrag = false;
    }
    private void WrongAnswerBehaviour()
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
