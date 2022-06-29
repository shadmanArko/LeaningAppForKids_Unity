using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AnswerItem_ObjectRecognitionDragGame : DragItem
{
    public TextMeshProUGUI countText;
    public Image recognitionObjectImage;
    public string fruitName;
    private void Start()
    {
        countText.gameObject.SetActive(true);
        countText.text = answerText;
        recognitionObjectImage.sprite = Resources.Load<Sprite>($"Fruits/Strawberry");
    }

    protected override void RightAnswerBehaviour()
    {
        allowBeginDrag = false;
        Destroy(gameObject, 1);

    }

}
