using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AnswerItem_MathDragGame : DragItem
{
    public TextMeshProUGUI countText;
    public Image recognitionObjectImage;
    public string fruitName;
    private void Start()
    {
        countText.gameObject.SetActive(false);
        countText.text = answerText;
        recognitionObjectImage.sprite = Resources.Load<Sprite>($"Fruits/{answerText}");
    }
}
