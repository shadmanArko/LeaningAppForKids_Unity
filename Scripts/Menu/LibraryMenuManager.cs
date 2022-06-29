using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LibraryMenuManager : MonoBehaviour
{
    public Button[] allLibraryButtons;

    public Sprite OrginalSprite;

    public Sprite SelectedSprite; 
    // Start is called before the first frame update
    void Start()
    {
        ButtonClicked();
        allLibraryButtons[4].GetComponent<ToggleButton_Controller>().OnButtonClicked(true);
    }
    public void ButtonClicked()
    {
        

        foreach (Button button in allLibraryButtons)
        {
            ToggleButton_Controller toggleButton_Controller = button.GetComponent<ToggleButton_Controller>();
            toggleButton_Controller.OnButtonClicked(false);
            toggleButton_Controller.GetComponent<Image>().sprite = OrginalSprite;
        }
        
    }
    public void BackButton(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ChangeSprite()
    {

    }


}
