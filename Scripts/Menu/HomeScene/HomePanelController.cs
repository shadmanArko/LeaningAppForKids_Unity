using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePanelController : MonoBehaviour
{
    public RectTransform AvatarPos;
    public GameObject ButtonPrefab;
    public GameObject PanelToLoad;
    public Button SoundButton;
    public Button CourseButton;
    public Button LibraryButton;


    public bool SoundOff;
    public Sprite MusicOn;
    public Sprite MusicOff;


    void Start()
    {
       SoundIcon();

       if (GameManager.instance.Return)
       {
           KidsManagement.Instance.SetKidButton(GameManager.instance.KidId);
       }

       LibraryButton.onClick.AddListener((() =>
       {
           GameManager.instance.LearningType = LearningType.Library;
           GameManager.instance.Return = true;
           PlayerPrefs.SetInt("loginState", (int)LoginState.KidsPage);
       }));


        CourseButton.onClick.AddListener((() =>
       {
           GameManager.instance.LearningType = LearningType.Course;
           GameManager.instance.Return = true;
           PlayerPrefs.SetInt("loginState", (int)LoginState.KidsPage);
       }));
    }


    public void Initialize(int id, string name, string age, string avatar, int courseId)
    {
        this.GetComponent<KidsInformation>().SetInformation(id,name,age,avatar, PlayerPrefs.GetString("parentsName"),  courseId);
        SpawnButton();
        GameManager.instance.KidId = id;

    }

    public void SpawnButton()
    {

        GameObject g = Instantiate(ButtonPrefab, AvatarPos);


        g.GetComponentInChildren<TextMeshProUGUI>().text = this.GetComponent<KidsInformation>().KidName;
        g.transform.GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>(this.GetComponent<KidsInformation>().KidAvatar);

        PlayerPrefs.SetInt("SelectedKid", this.GetComponent<KidsInformation>().KidId);

        g.GetComponent<Button>().onClick.AddListener((() =>
        {
            PanelToLoad.SetActive(true);
            Destroy(g);
            GameManager.instance.KidId = 0;
            gameObject.SetActive(false);
        }));

    }



    void SoundIcon()
    {
        SoundButton.transform.GetChild(0).GetComponent<Image>().sprite = AudioManager.Instance.audioState ? MusicOff : MusicOn;
        
    }

    public void SoundToggle()
    {

        SoundOff = !SoundOff;
        SoundButton.transform.GetChild(0).GetComponent<Image>().sprite = SoundOff ? MusicOff : MusicOn;
        AudioManager.Instance.ChangeAudioState();

    }


    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneMode(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
