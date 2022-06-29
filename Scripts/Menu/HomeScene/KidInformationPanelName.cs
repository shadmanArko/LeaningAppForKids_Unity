using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KidInformationPanelName : MonoBehaviour
{

    public TMP_InputField NameField;

    public Button ConfirmButton;

    public Sprite OrginalSprite;
    public Sprite SelectedSprite;

    public GameObject NextPanelToLoad;




    void Start()
    {
        SetName();
        ConfirmButton.onClick.AddListener(SubmitButtonClickEvent);
    }


    void SetName()
    {
        PlayerPrefs.SetString("KidName" + KidsManagement.Instance._kidCount, "Kid"+ KidsManagement.Instance._kidCount);
    }

    public void ChangeKidName()
    {

        if (NameField.text == string.Empty)
        {
            SetName();
        }
        else
        {
            PlayerPrefs.SetString("KidName" + KidsManagement.Instance._kidCount, NameField.text);
        }
    }

    void Reset()
    {

        NameField.text = "";
        ConfirmButton.image.sprite = OrginalSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    private async void SubmitButtonClickEvent()
    {
        ChangeKidName();
        ConfirmButton.image.sprite = SelectedSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));

        NextPanelToLoad.SetActive(true);

        Reset();
        this.gameObject.SetActive(false);

    }

}
