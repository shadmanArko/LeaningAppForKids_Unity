using UnityEngine;
using UnityEngine.UI;

public class ToggleButton_Controller : MonoBehaviour
{

    public UIPage relatedPage;
    
    public string sfxName;
    public Sprite SelectedSprite;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnButtonClicked(true);
        });
    }
    public void OnButtonClicked(bool isPressed)
    {
        if (isPressed)
        {
            relatedPage.ShowPage();
            GetComponent<Image>().sprite = SelectedSprite;
        }
        else
        {
            relatedPage.HidePage();
        }
        FindObjectOfType<AudioManager>().Play(sfxName);


    }
}
