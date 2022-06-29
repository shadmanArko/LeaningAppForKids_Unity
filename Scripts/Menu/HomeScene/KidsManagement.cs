using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KidsManagement : MonoBehaviour
{

    public static KidsManagement Instance;


    public List<RectTransform> kidsButtonRect;

    public GameObject ButtonPrefab;
    public GameObject KidsAddButton;
    public GameObject KidInformationPanel;

    private List<GameObject> kidsList = new List<GameObject>();

    private bool KidAssociatedWithButton = false;

    public int _kidCount = 0;

    public int i = 0;




    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        _kidCount = GetKidCount();
        
        SpawnButton();
    }
    public void KidsAdd()
    {
        if (_kidCount >= kidsButtonRect.Count)
        {
            KidsAddButton.GetComponent<Button>().interactable = false;
            Debug.Log("Maximum Child Reached");
            return;
        }
        _kidCount = Mathf.Clamp(++_kidCount, 1, kidsButtonRect.Count);

        ActivateKidInformationPanel();

    }

    private void KidsRemove()
    {
        _kidCount = Mathf.Clamp(--_kidCount, 1, kidsButtonRect.Count);

    }


    public void SetKidButton(bool state)
    {
        KidAssociatedWithButton = state;
    }

    void SpawnButton()
    {
        for (int i = 1; i <= _kidCount; i++)
        {
            
            kidsButtonRect[i-1].gameObject.SetActive(true);
            GameObject g = Instantiate(ButtonPrefab, kidsButtonRect[i-1]);
            g.GetComponent<KidsInformation>().SetInformation(i, GetKidName(i), GetKidAge(i), GetKidAvatar(i), PlayerPrefs.GetString("parentsName"),GetCourse(i));
            g.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("KidName" + i);
            g.transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(PlayerPrefs.GetString("KidAvatar" + i));


            g.GetComponent<Button>().onClick.AddListener((() =>
            {
                //HomeMenuManager.Instance.homePanel.GetComponent<HomePanelController>().Initialize(g.GetComponent<KidsInformation>().KidId,
                //    GetKidName(g.GetComponent<KidsInformation>().KidId), GetKidAge(g.GetComponent<KidsInformation>().KidId), GetKidAvatar(g.GetComponent<KidsInformation>().KidId), GetCourse(g.GetComponent<KidsInformation>().KidId));
                SetKidButton(g.GetComponent<KidsInformation>().KidId);

                HomeMenuManager.Instance.ActivateHomePanel();
            }));

            


        }
    }

    public void SpawnSingleButton()
    {
        kidsButtonRect[_kidCount-1].gameObject.SetActive(true);
        GameObject g = Instantiate(ButtonPrefab, kidsButtonRect[_kidCount-1]);

        g.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("KidName" + _kidCount);
        g.transform.GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>(PlayerPrefs.GetString("KidAvatar" + _kidCount));
        g.GetComponent<KidsInformation>().SetInformation(_kidCount, GetKidName(_kidCount), GetKidAge(_kidCount), GetKidAvatar(_kidCount), PlayerPrefs.GetString("parentsName"), GetCourse(_kidCount));
        int id = g.GetComponent<KidsInformation>().KidId;

        g.GetComponent<Button>().onClick.AddListener((() =>
        {
            
            //HomeMenuManager.Instance.homePanel.GetComponent<HomePanelController>().Initialize(id,
            //    GetKidName(id), GetKidAge(id), GetKidAvatar(id), GetCourse(id));
            SetKidButton(id);

            HomeMenuManager.Instance.ActivateHomePanel();
        }));
        //g.GetComponent<Button>().onClick.AddListener(HomeMenuManager.Instance.ActivateHomePanel);


    }

    public void SetKidButton(int id)
    {
        HomeMenuManager.Instance.homePanel.GetComponent<HomePanelController>().Initialize(id,
            GetKidName(id), GetKidAge(id), GetKidAvatar(id), GetCourse(id));
    }

    int GetKidCount()
    {
        if (!PlayerPrefs.HasKey("KidCount"))
        {
            PlayerPrefs.SetInt("KidCount" , 0);
        }

        return PlayerPrefs.GetInt("KidCount");
    }


    public void SetKidCount()
    {
        PlayerPrefs.SetInt("KidCount", _kidCount);
    }

    [ContextMenu("Clear Child")]
    public void ClearChild()
    {
        PlayerPrefs.DeleteKey("KidCount");
        
    }


    public void ActivateKidInformationPanel()
    {
        KidInformationPanel.SetActive(true);
    }

    public void DisableKidInformationPanel()
    {
        KidInformationPanel.SetActive(false);
    }


    public void SetKidNameInformation()
    {
        /*PlayerPrefs.SetString("KidName" + _kidCount, KidInformationPanel.GetComponent<KidInformationPanel>().KidName.text);
        Debug.Log("dipok " + PlayerPrefs.GetString("KidName" + _kidCount));
        DisableKidInformationPanel();
        SpawnSingleButton();*/
    }


    public string GetKidName(int kidId)
    {
        return PlayerPrefs.GetString("KidName" + kidId);
    }

    public string GetKidAge(int kidId)
    {
        return PlayerPrefs.GetString("KidAge" + kidId);
    }

    public string GetKidAvatar(int kidId)
    {
        return PlayerPrefs.GetString("KidAvatar" + kidId);
    }

    int GetCourse(int kidId)
    {
        if (!PlayerPrefs.HasKey(kidId + "CourseCompletedId"))
        {
            PlayerPrefs.SetInt(kidId + "CourseCompletedId", 0);
        }
        return PlayerPrefs.GetInt(kidId + "CourseCompletedId");

    }

}
