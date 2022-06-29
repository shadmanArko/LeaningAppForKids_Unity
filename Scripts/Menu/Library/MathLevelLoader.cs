using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MathLevelLoader : MonoBehaviour
{
    public TextMeshProUGUI englishRecognisionCharacter;
    public TextMeshProUGUI banglaRecognisionCharacter;
    public string videoURL;
    public bool isUpperCase;
    public string loadSceneName;
    public int noOfSpawnObject;
    public int noOfSpawnRightObject;
    public string objectName;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnButtonClicked();
        });
    }
    public void OnButtonClicked()
    {
        MathMiniGameDetails miniGameDetails;
        miniGameDetails = new MathMiniGameDetails(noOfSpawnObject, noOfSpawnRightObject, "");
        GameManager.instance.selectedMathMiniGameDetails = miniGameDetails;
        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<AudioManager>().Stop("theme");
        SceneManager.LoadScene(loadSceneName);
    }
}
