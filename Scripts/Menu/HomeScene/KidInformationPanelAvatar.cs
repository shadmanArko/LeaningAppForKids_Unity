using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class KidInformationPanelAvatar : MonoBehaviour
{
    public GameObject AvatarButtonPrefab;
    public RectTransform ButtonRect;
    public Button ConfirmButton;
    public GameObject NextPanelToLoad;
    public Sprite DefaultSprite;


    public Sprite selectedSprite;
    public Sprite OrginalSprite;

    public Sprite ButtonSelectedSprite;
    public Sprite ButtonOrginalSprite;

    private List<Sprite> _avatarSprite;
    private List<GameObject> _buttonReference = new List<GameObject>();
    private bool isClicked = false;

    void Start()
    {
        SetAvatar();
        SpawnAvatar();

        ConfirmButton.onClick.AddListener(SubmitButtonClickEvent);
    }

    public void SpawnAvatar()
    {


        object[] loadedAvatarSprite = Resources.LoadAll("Avatar", typeof(Sprite));
        _avatarSprite = new List<Sprite>(loadedAvatarSprite.Length);





        foreach (var avatar in loadedAvatarSprite)
        {
            _avatarSprite.Add((Sprite)avatar);
        }

        for (int i = 0; i < _avatarSprite.Count; i++)
        {
            GameObject g = Instantiate(AvatarButtonPrefab, ButtonRect);
            _buttonReference.Add(g);
            g.transform.GetChild(0).GetComponent<Image>().sprite = _avatarSprite[i];

            //string path = AssetDatabase.GetAssetPath(g.transform.GetChild(0).GetComponent<Image>().sprite);

            string path = "Avatar/" + g.transform.GetChild(0).GetComponent<Image>().sprite.name;

            Debug.Log("dipok avatar path "+ path);


            path = path.Replace(".png", "");
            //path = path.Replace("Assets/Resources/", "");
            


            g.GetComponent<Button>().onClick.AddListener((() =>
            {
                isClicked = true;
                ClearPreviousInput();
                PlayerPrefs.SetString("KidAvatar" + KidsManagement.Instance._kidCount, path);
                Debug.Log("dipok path " + path);
                Debug.Log("dipok asset path " + PlayerPrefs.GetString("KidAvatar" + KidsManagement.Instance._kidCount));

                g.GetComponent<Button>().image.sprite = selectedSprite;
                Debug.Log("dipok kidavatar : " + PlayerPrefs.GetString("KidAge" + KidsManagement.Instance._kidCount));
            }));



        }
        
    }


    void SetAvatar()
    {
        //string path = AssetDatabase.GetAssetPath(DefaultSprite);


        string path = "Avatar/" + DefaultSprite.name;


        Debug.Log("dipok path " + path);

        path = path.Replace(".png", "");
        path = path.Replace("Assets/Resources/", "");


        PlayerPrefs.SetString("KidAvatar" + KidsManagement.Instance._kidCount, path);

    }

    void ClearPreviousInput()
    {
        foreach (var button in _buttonReference)
        {
            button.GetComponent<Button>().image.sprite = OrginalSprite;
        }
    }

    void Reset()
    {
        isClicked = false;
        ClearPreviousInput();
        ConfirmButton.image.sprite = ButtonOrginalSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;


    }

    private async void SubmitButtonClickEvent()
    {
        if(!isClicked)
            SetAvatar();

        ConfirmButton.image.sprite = ButtonSelectedSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));

        

        NextPanelToLoad.SetActive(true);

        BackToChildCreation();


        Reset();
        this.gameObject.SetActive(false);

    }

    private void BackToChildCreation()
    {
        
        KidsManagement.Instance.SetKidCount();
        KidsManagement.Instance.SpawnSingleButton();
        KidsManagement.Instance.DisableKidInformationPanel();
    }
}
