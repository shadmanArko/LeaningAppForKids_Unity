using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Michsky.UI.ModernUIPack;
public class MemoryLevelLoader : MonoBehaviour
{
    public TextMeshProUGUI title;
    public ButtonManagerBasic buttonManager;
    public string videoURL;
    public string ObjectName;
    public string loadSceneName;
    private void Start()
    {
        title.text = ObjectName;
        buttonManager.buttonText = ObjectName;
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnButtonClicked();
        });
    }
    public void OnButtonClicked()
    {
        MiniGameDetails miniGameDetails;
        miniGameDetails = new MiniGameDetails(videoURL, ObjectName, ObjectName);
        GameManager.instance.selectedMiniGameDetails = miniGameDetails;
        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<AudioManager>().Stop("theme");
        SceneManager.LoadScene(loadSceneName);
    }
   
}
