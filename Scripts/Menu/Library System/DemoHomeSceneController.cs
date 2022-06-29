using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DemoHomeSceneController : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectedLanguage"))
        {
            PlayerPrefs.SetString("selectedLanguage", "English");
            languageDropdown.value = 0;
        }
        else
        {
            if (PlayerPrefs.GetString("selectedLanguage") == "English")
            {
                languageDropdown.value = 0;
            }
            else
            {
                languageDropdown.value = 1;
            }
        }
        languageDropdown.onValueChanged.AddListener((int i) =>
        {
            print(i);
            switch (i)
            {
                case 0:
                    PlayerPrefs.SetString("selectedLanguage", "English");
                    languageDropdown.value = 0;
                    break;
                case 1:
                    PlayerPrefs.SetString("selectedLanguage", "Bangla");
                    languageDropdown.value = 1;
                    break;
                default:
                    break;
            }
        });
    }
    public void LoadSceneMode(string sceneName)
    {
        //FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadSceneAsync(sceneName);
    }
}
