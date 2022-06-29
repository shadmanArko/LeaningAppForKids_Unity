using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuManager : MonoBehaviour
{
    public static HomeMenuManager Instance;

    public GameObject ParentSignInPanel;
    public GameObject KidsCreatePanel;
    public GameObject KidsPage;
    public GameObject homePanel;
    public GameObject StartingPanel;

    public StoredData storedData;


    [SerializeField]private LoginState loginState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
            
        else
            Destroy(gameObject);

        storedData = new StoredData();
    }
    void Start()
    {
        loginState = (LoginState)PlayerPrefs.GetInt("loginState");
        if (GameManager.instance.ShowStartingPanel)
        {
            if (loginState != LoginState.ParentLogin)
            {
                loginState = LoginState.kidsCreate;
            }
            StartingPanel.SetActive(true);
            Invoke("LoginStateSet", 5f);
        }

        else
        {
            LoginStateSet();
        }

        //LoginStateSet();
    }

    private void LoginStateSet()
    {
        StartingPanel.SetActive(false);
        GameManager.instance.ShowStartingPanel = false;
        switch (loginState)
        {
            case LoginState.ParentLogin:
                ParentSignInPanel.SetActive(true);
                break;
            case LoginState.kidsCreate:
                KidsCreatePanel.SetActive(true);
                break;
            case LoginState.KidsLogin:
                homePanel.SetActive(true);
                break;
            case LoginState.KidsPage:
                KidsCreatePanel.SetActive(true);
                KidsPage.SetActive(true);
                break;
            default:
                ParentSignInPanel.SetActive(true);
                break;
        }
    }

    public void ActivateHomePanel()
    {
        homePanel.SetActive(true);
        KidsCreatePanel.SetActive(false);
        ParentSignInPanel.SetActive(false);
    }
}
