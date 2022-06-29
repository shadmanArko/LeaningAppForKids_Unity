using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class KidInformationPanelAge : MonoBehaviour
{

    public GameObject AgeButtonPrefab;
    public RectTransform ButtonRect;
    public Button ConfirmButton;
    public GameObject NextPanelToLoad;

    public int StartingAge;
    public int EndingAge;
    public Sprite selectedSprite;
    public Sprite OrginalSprite;

    private bool isClicked = false;


    private List<GameObject> _buttonReference =  new List<GameObject>();

    void Start()
    {
        SetAge();
        SpawnButton();

        ConfirmButton.onClick.AddListener(SubmitButtonClickEvent);
    }

    public void SpawnButton()
    {
        for (int i = StartingAge; i <= EndingAge; i++)
        {
            GameObject g = Instantiate(AgeButtonPrefab, ButtonRect);
            _buttonReference.Add(g);
            g.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            g.GetComponent<Button>().onClick.AddListener((() =>
            {
                isClicked = true;
                ClearPreviousInput();
                PlayerPrefs.SetString("KidAge"+KidsManagement.Instance._kidCount, g.GetComponentInChildren<TextMeshProUGUI>().text);
                g.GetComponent<Button>().image.sprite = selectedSprite;
                g.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                Debug.Log("dipok kidage : "+ PlayerPrefs.GetString("KidAge" + KidsManagement.Instance._kidCount));
            }));

        }
    }

    void SetAge()
    {
        PlayerPrefs.SetString("KidAge" + KidsManagement.Instance._kidCount, StartingAge.ToString());
    }

    void ClearPreviousInput()
    {
        foreach (var button in _buttonReference)
        {
            button.GetComponent<Button>().image.sprite = OrginalSprite;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }


    void Reset()
    {
        isClicked = false;
        ClearPreviousInput();

        ConfirmButton.image.sprite = OrginalSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    private async void SubmitButtonClickEvent()
    {
        if(!isClicked)
            SetAge();
        ConfirmButton.image.sprite = selectedSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));

        NextPanelToLoad.SetActive(true);
        Reset();
        this.gameObject.SetActive(false);

    }




}
